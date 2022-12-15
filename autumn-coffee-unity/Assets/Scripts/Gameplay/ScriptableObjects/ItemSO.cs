using System;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(ItemSO), menuName = EditorConstants.Data + nameof(ItemSO)), Serializable]
public class ItemSO : ScriptableObject {
    [Header("Main Settings")]
    public Sprite icon;
    public string itemName;
    [TextArea(5, 5)]
    public string description;
    public int cost;

    [Header("Tag Settings")]
    public ItemType type;
    
}