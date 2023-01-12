using System;
using Ruinum.Utils;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class CraftObject : AnimationObject {
    [SerializeField] private List<RecipeSO> recipes;

    private readonly List<ItemSO> _items = new List<ItemSO>();
    private readonly List<ItemSO> _syrups = new List<ItemSO>();
    private readonly List<ItemSO> _toppings = new List<ItemSO>();

    private ItemSO _currentCoffee;

    private void Start() {
        _currentCoffee = ScriptableObject.CreateInstance<ItemSO>();
        
        // _currentCoffee = true;
    }

    public void AddItem(ItemSO itemSO) {
        AnimationPunch();

        if (itemSO.type == ItemType.Syrup) {
            _syrups.Add(itemSO);
            return;
        }
        if (itemSO.type == ItemType.Topping) {
            _toppings.Add(itemSO);
            return;
        }

        _items.Add(itemSO);
    }

    private void CraftCoffee() {
        foreach (var recipe in recipes) {
            _currentCoffee = recipe.CheckEquality(_items) ? recipe.ResultItem : null;

            if (_currentCoffee != null) break;
        }
    }

    private void OnMouseDown() {
        gameObject.layer = 2;
    }

    private void OnMouseDrag() {
        transform.position = new Vector3(MouseUtils.GetMouseWorld2DPosition().x, MouseUtils.GetMouseWorld2DPosition().y, transform.position.z);
    }

    private void OnMouseExit() {
        gameObject.layer = 0;
    }

    private void OnMouseUp() {
        gameObject.layer = 2;

        if (!MouseUtils.TryRaycast2DToMousePosition(out var raycastHit2D)) {
            gameObject.layer = 0;
            return;
        }

        if (raycastHit2D.collider.TryGetComponent<TransformCraftObject>(out var transformCraftObject)) {
            transformCraftObject.TransformObject(gameObject);
            gameObject.layer = 0;
        }

        if (raycastHit2D.collider.TryGetComponent<Customer>(out var customer)) {
            CraftCoffee();

            customer.task.AddItem(_currentCoffee);

            foreach (var syrup in _syrups) {
                customer.task.AddItem(syrup);
            }

            foreach (var topping in _toppings) {
                customer.task.AddItem(topping);
            }
            
            customer.ReloadUI();

            gameObject.layer = 0;
            customer.TryLeave();

            Destroy(gameObject);
        }
        gameObject.layer = 0;
    }

    public List<ItemSO> GetItems() {
        return _items;
    }

    public List<ItemSO> GetSyrups() {
        return _syrups;
    }

    public List<ItemSO> GetToppings() {
        return _toppings;
    }
}