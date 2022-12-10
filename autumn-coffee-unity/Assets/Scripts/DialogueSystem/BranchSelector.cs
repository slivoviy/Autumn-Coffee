using Articy.Unity;
using Articy.Unity.Interfaces;
using TMPro;
using UnityEngine;

namespace DialogueSystem {
    public class BranchSelector : MonoBehaviour {
        private Branch branch;
        private ArticyFlowPlayer flowPlayer;
        [SerializeField] TextMeshProUGUI buttonText;

        public void AssignBranch(ArticyFlowPlayer aFlowPlayer, Branch aBranch) {
            branch = aBranch;
            flowPlayer = aFlowPlayer;
            var target = aBranch.Target;
            buttonText.text = string.Empty;

            if (target is IObjectWithMenuText objectWithMenuText) buttonText.text = objectWithMenuText.MenuText;

            if (string.IsNullOrEmpty(buttonText.text)) buttonText.text = "Continue";
        }

        public void OnBranchSelected() {
            flowPlayer.Play(branch);
        }
    }
}