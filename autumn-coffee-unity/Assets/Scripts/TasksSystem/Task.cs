using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class Task {
    private Customer _customer;

    public List<ItemSO> Order = new List<ItemSO>();
    private List<ItemSO> _orderInProgress = new List<ItemSO>();

    public void AddItem(ItemSO item) {
        _orderInProgress.Add(item);
    }

    public bool CheckComplete() {
        if (Order.All(t => t.type != ItemType.Coffee)) return false;
        
        var completed = _orderInProgress.Count >= Order.Count;

        if (completed) CheckCorrect();

        return completed;
    }

    public bool CheckCorrect() {
        return Order.Count == _orderInProgress.Count && Order.All(_orderInProgress.Contains);

    }

    public bool IsItemInOrder(ItemSO item) {
        return _orderInProgress.Contains(item);
    }

    public Customer Owner(Customer c = null) {
        if (c) _customer = c;
        return _customer;
    }
}