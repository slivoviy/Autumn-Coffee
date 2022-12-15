using UnityEngine;

[CreateAssetMenu(fileName = nameof(RecipeSO), menuName = EditorConstants.Data + nameof(RecipeSO))]
public class RecipeSO : ScriptableObject
{
    public ItemSO[] RequestItems;
    public ItemSO ResultItem;

    public bool CheckEquality(ItemSO[] items)
    {
        if (RequestItems.Length > items.Length) return false;

        int correctItems = 0;

        for (int j = 0; j < items.Length; j++)
        {
            for (int i = 0; i < RequestItems.Length; i++)
            {
                if (items[j] == RequestItems[i]) { correctItems++; break; }
            }
        }
        if (correctItems != RequestItems.Length) return false;
        return true;
    }
}