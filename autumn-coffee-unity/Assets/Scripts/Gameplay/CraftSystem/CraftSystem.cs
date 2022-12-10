using Ruinum.Core;
using UnityEngine;

public class CraftSystem : BaseSingleton<CraftSystem>
{
    [SerializeField] private GameObject _craftObjectPrefab;
    [SerializeField] private Transform _craftObjectTransorm;

    private CraftObject _craftObject;

    public bool TryCreateCraftObject(out CraftObject craftObject)
    {
        craftObject = null;
        if (_craftObject != null) return false;

        var craftObjectPrefab = Instantiate(_craftObjectPrefab, _craftObjectTransorm);
        _craftObject = craftObjectPrefab.GetComponent<CraftObject>();

        return true;
    }

    public bool TryAddItem(ItemSO itemSO)
    {
        if (_craftObject == null) return false;

        _craftObject.AddItem(itemSO);
        
        return true;
    }
}