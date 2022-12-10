using UnityEngine;

[CreateAssetMenu(fileName = nameof(ItemSO), menuName = EditorConstants.Data + nameof(ItemSO))]
public class ItemSO : ScriptableObject
{
    [Header("Main Settings")]
    public Sprite Icon;
    public string Name;
    [TextArea(5,5)]
    public string Description;
    public int Cost;

    [Header("Tag Settings")]
    public bool IsSyrup;
    public bool IsTopping;
    public bool IsDessert;
}
