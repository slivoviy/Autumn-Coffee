using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class Task {
    private Customer _customer;

    public bool completed;
    public bool correct;

    public HashSet<ItemSO> Order = new HashSet<ItemSO>();
    private HashSet<ItemSO> _orderInProgress = new HashSet<ItemSO>();

    public void AddItem(ItemSO item) {
        _orderInProgress.Add(item);

        CheckComplete();
    }

    private void CheckComplete() {
        if (Order.All(t => t.type != ItemType.Coffee)) return;
        
        completed = _orderInProgress.Count >= Order.Count;

        if (completed) CheckCorrect();
    }

    private void CheckCorrect() {
        if (Order.Count != _orderInProgress.Count) return;

        correct = Order.SetEquals(_orderInProgress);
    }

    public bool IsItemInOrder(ItemSO item) {
        return _orderInProgress.Contains(item);
    }

    public Customer Owner(Customer c = null) {
        if (c) _customer = c;
        return _customer;
    }
}