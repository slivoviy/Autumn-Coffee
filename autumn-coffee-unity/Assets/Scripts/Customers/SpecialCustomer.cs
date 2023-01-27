using Articy.Unity;
using Customers;
using DialogueSystem;
using Gameplay.ScriptableObjects;
using Ruinum.Core;
using UnityEngine;

public class SpecialCustomer : Customer {
    
    [SerializeField] public SpecialCustomerSO data;

    private bool dialogueStarted;

    protected override void AddTask() {
        if (isTaskCreated) return;
        
        if(data.taskNumber >= data.tasks.Count) base.AddTask();
        else {
            task = data.tasks[data.taskNumber];
            
            isTaskCreated = true;

            ReloadUI();
        }
    }

    public override void TryLeave() {
        if(!task.CheckComplete()) return;

        if (data.hasSecondDialogue) {
            
            if (task.CheckCorrect()) {
                DialogueManager.Singleton.OrderDoneCorrectly();
            }

            Time.timeScale = 0;
            
            DialogueManager.Singleton.StartDialogue(GetDialogue(), this);
            data.hasSecondDialogue = false;
        }
        
        base.TryLeave();
    }

    private ArticyObject GetDialogue() {
        return data.taskNumber < data.dialogues.Count
            ? data.dialogues[data.taskNumber++].GetObject()
            : null;
    }

    private void OnMouseDown() {
        if (dialogueStarted) return;
        dialogueStarted = true;
        
        Time.timeScale = 0;

        DialogueManager.Singleton.StartDialogue(GetDialogue(), this);
    }

    public void SetTask() {
        AddTask();
    }
}