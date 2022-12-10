using System.Collections;
using System.Collections.Generic;
using Articy.Unity;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Customers/SpecialCustomerData")]
public class SpecialCustomerSO : ScriptableObject {
    public string customerName;
    public List<Task> tasks;
    public List<ArticyRef> dialogues;
    [HideInInspector] public int ordersNumber;
    public bool hasSecondDialogue;
}