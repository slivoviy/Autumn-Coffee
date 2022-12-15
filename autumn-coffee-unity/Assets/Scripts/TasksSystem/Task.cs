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
    private HashSet<ItemSO> OrderInProgress = new HashSet<ItemSO>();

    public void AddItem(ItemSO item) {
        OrderInProgress.Add(item);

        CheckComplete();
    }

    private void CheckComplete() {
        if (Order.All(t => t.type != ItemType.Coffee)) return;
        
        completed = OrderInProgress.Count >= Order.Count;

        if (completed) CheckCorrect();
    }

    private void CheckCorrect() {
        if (Order.Count != OrderInProgress.Count) return;

        correct = Order.SetEquals(OrderInProgress);
    }



    public Customer Owner(Customer c = null) {
        if (c) _customer = c;
        return _customer;
    }
}