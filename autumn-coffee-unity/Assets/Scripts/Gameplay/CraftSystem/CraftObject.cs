using Ruinum.Utils;
using System.Collections.Generic;
using UnityEngine;


public class CraftObject : AnimationObject
{
    [SerializeField] private List<ReceptSO> _recepts;

    private List<ItemSO> _items = new List<ItemSO>();
    private List<ItemSO> _syrups = new List<ItemSO>();
    private List<ItemSO> _poddings = new List<ItemSO>();

    private ItemSO _currentCofee;

    public void AddItem(ItemSO itemSO)
    {
        AnimationPunch();

        if (itemSO.IsSyrup) { _syrups.Add(itemSO); return; }
        if (itemSO.IsTopping) { _poddings.Add(itemSO); return; }

        _items.Add(itemSO);
    }

    public void CraftCofee()
    {       
        for (int i = 0; i < _recepts.Count; i++)
        {
            var recept = _recepts[i];
            if (recept.RequestItems.Length != _items.Count) continue;
            int correctItems = 0;
            for (int j = 0; j < recept.RequestItems.Length; j++)
            {
                if (recept.RequestItems[j] == _items[j]) correctItems++;
            }

            if (correctItems == recept.RequestItems.Length)
            {
                _currentCofee = recept.ResultItem;
                break;
            }
        }
    }

    private void OnMouseDown()
    {
        gameObject.layer = 2;
    }

    private void OnMouseDrag()
    {
        transform.position = new Vector3(MouseUtils.GetMouseWorld2DPosition().x, MouseUtils.GetMouseWorld2DPosition().y, transform.position.z);
    }

    private void OnMouseExit()
    {
        gameObject.layer = 0;
    }

    private void OnMouseUp()
    {
        gameObject.layer = 2;

        if (!MouseUtils.TryRaycast2DToMousePosition(out var raycastHit2D)) { gameObject.layer = 0; return; }

        if (raycastHit2D.collider.TryGetComponent<TransformCraftObject>(out var transformCraftObject))
        {
            transformCraftObject.TransformObject(gameObject);
            gameObject.layer = 0;
        }

        if (raycastHit2D.collider.TryGetComponent<Customer>(out var customer))
        {
            CraftCofee();

            customer.task.AddItem(_currentCofee);

            for (int i = 0; i < _syrups.Count; i++)
            {
                customer.task.AddItem(_syrups[i]);
            }

            for (int i = 0; i < _poddings.Count; i++)
            {
                customer.task.AddItem(_poddings[i]);
            }

            gameObject.layer = 0;
            customer.Leave();
            
            Destroy(gameObject);
        }
        gameObject.layer = 0;
    }

    public List<ItemSO> GetItems()
    {
        return _items;
    }

    public List<ItemSO> GetSyrups()
    {
        return _syrups;
    }

    public List<ItemSO> GetPoddings()
    {
        return _poddings;
    }
}
