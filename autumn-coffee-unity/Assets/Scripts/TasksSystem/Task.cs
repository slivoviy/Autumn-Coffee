using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Customers;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class Task {
    private Customer _customer;

    public List<ItemSO> order = new List<ItemSO>();
    private List<ItemSO> _orderInProgress = new List<ItemSO>();

    public void AddItem(ItemSO item) {
        _orderInProgress.Add(item);
    }

    public bool CheckComplete() {
        if (order.All(t => t.type != ItemType.Coffee)) return false;
        
        var completed = _orderInProgress.Count >= order.Count;

        if (completed) CheckCorrect();

        return completed;
    }

    public bool CheckCorrect() {
        order.Sort((lhs, rhs) => string.CompareOrdinal(lhs.itemName, rhs.itemName));
        _orderInProgress.Sort((lhs, rhs) => string.CompareOrdinal(lhs.itemName, rhs.itemName));
        
        var log = _orderInProgress.Aggregate("", (current, item) => current + $"{item.itemName} ");
        Debug.Log($"Checking equality of list: {log}");
        Debug.Log($"Equality is: {order.SequenceEqual(_orderInProgress)}");
        
        return order.SequenceEqual(_orderInProgress);

    }

    public bool IsItemInOrder(ItemSO item) {
        return _orderInProgress.Contains(item);
    }

    public Customer Owner(Customer c = null) {
        if (c) _customer = c;
        return _customer;
    }
}