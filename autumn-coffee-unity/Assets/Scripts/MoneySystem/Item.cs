using Customers;
using Ruinum.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;


public class Item : AnimationObject {
    [SerializeField] private ItemSO item;
    [SerializeField] private GameObject hintCanvas;

    private Vector3 _startPosition;
    private SpriteRenderer _sprite;

    private void Start() {
        _startPosition = transform.position;

        hintCanvas.SetActive(false);
        hintCanvas.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>().text = item.itemName;

        _sprite = transform.Find("Square").GetComponent<SpriteRenderer>();
    }

    private void OnMouseDown() {
        gameObject.layer = 2;
        _sprite.sortingOrder = item.type != ItemType.Dessert ? 3 : 11;
    }

    private void OnMouseDrag() {
        transform.position = new Vector3(MouseUtils.GetMouseWorld2DPosition().x, MouseUtils.GetMouseWorld2DPosition().y, transform.position.z);
    }

    private void OnMouseUp() {
        if (!MouseUtils.TryRaycast2DToMousePosition(out var raycastHit2D)) {
            RefreshSettings();
            return;
        }

        if (raycastHit2D.collider.TryGetComponent<CraftObject>(out var craftObject) && item.type != ItemType.Dessert && craftObject.GetItems().Count < 4) {
            craftObject.AddItem(item);

            AnimationPunch();

            if (item.cost > 0) MoneySystem.Singleton.SubtractAmount(item);

            RefreshSettings();
        }

        if (raycastHit2D.collider.TryGetComponent<Customer>(out var customer)) {
            customer.task.AddItem(item);
            customer.ReloadUI();

            AnimationPunch();

            if (item.type != ItemType.Coffee && item.cost > 0) MoneySystem.Singleton.SubtractAmount(item);

            RefreshSettings();

            customer.TryLeave();
        }

        if (raycastHit2D.collider.TryGetComponent<TrashCan>(out var trash)) {
            Destroy(this);
        }

        RefreshSettings();
    }

    private void OnMouseEnter() {
        hintCanvas.SetActive(true);
    }

    private void OnMouseExit() {
        hintCanvas.SetActive(false);
    }

    private void RefreshSettings() {
        transform.position = _startPosition;
        gameObject.layer = 0;
        _sprite.sortingOrder = item.type != ItemType.Dessert ? 2 : 10;
    }
}