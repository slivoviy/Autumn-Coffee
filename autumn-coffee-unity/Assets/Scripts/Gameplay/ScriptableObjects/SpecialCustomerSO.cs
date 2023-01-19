using System.Collections.Generic;
using Articy.Unity;
using UnityEngine;

namespace Gameplay.ScriptableObjects {
    [CreateAssetMenu(fileName = "Data", menuName = "Customers/SpecialCustomerData")]
    public class SpecialCustomerSO : ScriptableObject {
        public string customerName;
        public List<Task> tasks;
        public List<ArticyRef> dialogues;
        [HideInInspector] public int taskNumber;
        public bool hasSecondDialogue;
    }
}