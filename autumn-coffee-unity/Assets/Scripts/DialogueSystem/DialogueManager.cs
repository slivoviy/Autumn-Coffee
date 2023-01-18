using System.Collections.Generic;
using Articy.Cozy_Cafe_atricy;
using Articy.Unity;
using Articy.Unity.Interfaces;
using Ruinum.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace DialogueSystem {
    public class DialogueManager : BaseSingleton<DialogueManager>, IArticyFlowPlayerCallbacks {
        [SerializeField] private GameObject dialogueBox;
        [SerializeField] private TextMeshProUGUI dialogueText;
        [SerializeField] private TextMeshProUGUI personName;
        [SerializeField] private RectTransform branchingPanel;

        private ArticyFlowPlayer flowPlayer;
        private SpecialCustomer currentCustomer;

        private void Start() {
            flowPlayer = GetComponent<ArticyFlowPlayer>();
        }

        public void StartDialogue(IArticyObject aObject, SpecialCustomer customer) {
            if (aObject == null) return;
            dialogueBox.SetActive(true);

            currentCustomer = customer;
            OrderDoneCorrectly();

            flowPlayer.StartOn = aObject;
        }

        public void CloseDialogueBox() {
            Time.timeScale = 1;

            dialogueBox.SetActive(false);

            if (Singleton.currentCustomer.isTaskCreated) return;
            Singleton.currentCustomer.SetTask();
        }

        public void OnFlowPlayerPaused(IFlowObject aObject) {
            dialogueText.text = string.Empty;
            personName.text = string.Empty;


            if (aObject is IObjectWithText objectWithText) dialogueText.text = objectWithText.Text;

            if (!(aObject is IObjectWithSpeaker objectWithSpeaker)) return;
            if (objectWithSpeaker.Speaker is Entity speaker) personName.text = speaker.DisplayName;
        }

        public void OnBranchesUpdated(IList<Branch> aBranches) {
            ClearBranches();

            var dialogueIsFinished = true;
            foreach (var branch in aBranches) {
                if (branch.Target is IDialogueFragment) dialogueIsFinished = false;
            }

            if (!dialogueIsFinished) {
                foreach (var branch in aBranches) {
                    var button = Instantiate(Resources.Load<GameObject>("Prefabs/Branch Button"), branchingPanel);
                    button.GetComponent<BranchSelector>().AssignBranch(flowPlayer, branch);
                }
            }
            else {
                var button = Instantiate(Resources.Load<GameObject>("Prefabs/Close Button"), branchingPanel);
                button.GetComponent<Button>().onClick.AddListener(CloseDialogueBox);
            }
        }

        private void ClearBranches() {
            foreach (Transform child in branchingPanel) {
                Destroy(child.gameObject);
            }
        }

        public void OrderDoneCorrectly() {
            if (Singleton.currentCustomer.data.customerName is "Reviewer" or "BaristaFriend") {
                flowPlayer.GlobalVariables.SetVariableByString(
                    Singleton.currentCustomer.data.customerName + ".OrderDoneCorrectly", true);
            }
        }
    }
}