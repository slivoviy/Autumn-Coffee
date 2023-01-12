using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(RecipeSO), menuName = EditorConstants.Data + nameof(RecipeSO))]
public class RecipeSO : ScriptableObject
{
    public ItemSO[] RequestItems;
    public ItemSO ResultItem;

    public bool CheckEquality(List<ItemSO> items)
    {
        if (RequestItems.Length > items.Count) return false;

        var correctItems = items.Count(item => RequestItems.Any(t => item == t));

        return correctItems == RequestItems.Length;
    }
}