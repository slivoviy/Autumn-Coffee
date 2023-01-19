using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(RecipeSO), menuName = EditorConstants.Data + nameof(RecipeSO))]
public class RecipeSO : ScriptableObject {
    public ItemSO[] RequestItems;
    public ItemSO ResultItem;

    public bool CheckEquality(List<ItemSO> items) {
        var log = items.Aggregate("", (current, item) => current + $"{item.itemName} ");
        Debug.Log($"Checking equality of list: {log}");

        Array.Sort(RequestItems, (lhs, rhs) => string.CompareOrdinal(lhs.itemName, rhs.itemName));
        items.Sort((lhs, rhs) => string.CompareOrdinal(lhs.itemName, rhs.itemName));
        
        return RequestItems.SequenceEqual(items);
    }
}