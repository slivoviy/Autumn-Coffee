using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Ruinum.Utils;
using Ruinum.Core;


public class TaskManager : BaseSingleton<TaskManager> {
    private readonly List<ScriptableObject> _coffee = new List<ScriptableObject>();
    private readonly List<ScriptableObject> _items = new List<ScriptableObject>();

    private void Start() {
        _coffee.AddRange(Resources.LoadAll<ScriptableObject>("ScriptableObjects/Items/Drinks"));

        _items.AddRange(Resources.LoadAll<ScriptableObject>("ScriptableObjects/Items/Desserts"));
        _items.AddRange(Resources.LoadAll<ScriptableObject>("ScriptableObjects/Items/Syrups"));
        _items.AddRange(Resources.LoadAll<ScriptableObject>("ScriptableObjects/Items/Toppings"));
    }
    
    public Task CreateTask(Customer customer) {
        var task = new Task();
        task.Owner(customer);
        var difficulty = GetDifficulty();

        task.order.Add(AddItem(_coffee));

        for (var i = 1; i < difficulty; i++) {
            ItemSO item;
            do {
                item = AddItem(_items);
            } while(task.order.Any(it => it.type == item.type));
            
            task.order.Add(item);
        }

        return task;
    }

    private static ItemSO AddItem(List<ScriptableObject> items) {
        return (ItemSO)RandomExtentions.RandomList(items);
    }

    private static int GetDifficulty() {
        if (RandomExtentions.RandomLess(0.8f)) return 1;

        return RandomExtentions.RandomLess(0.5f) ? 2 : 3;
    }
}