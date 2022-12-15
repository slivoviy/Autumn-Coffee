using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CraftObject))]
public class CraftObjectUI : MonoBehaviour
{
    [SerializeField] private Transform _canvasTransform;
    [SerializeField] private GameObject _layoutPrefab;
    [SerializeField] private GameObject _itemIcon;

    private GameObject _createdLayout;
    private CraftObject _currentObject;

    private List<GameObject> _createdItemIcons = new List<GameObject>();

    private void Start()
    {
        _currentObject = GetComponent<CraftObject>();
        _createdLayout = Instantiate(_layoutPrefab, _canvasTransform);
        _createdLayout.SetActive(false);
    }

    private void OnMouseEnter()
    {
        Show();
    }

    private void OnMouseExit()
    {
        Hide();
    }

    private void Show()
    {
        _createdLayout.SetActive(true);

        CreateIcons(_currentObject.GetItems());
        CreateIcons(_currentObject.GetSyrups());
        CreateIcons(_currentObject.GetToppings());
    }

    private void Hide()
    {
        _createdLayout.SetActive(false);

        for (int i = 0; i < _createdItemIcons.Count; i++)
        {
            Destroy(_createdItemIcons[i]);
        }

        _createdItemIcons.Clear();
    }

    private void CreateIcons(List<ItemSO> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            var createdIcon = Instantiate(_itemIcon, _createdLayout.transform);
            createdIcon.GetComponent<Image>().sprite = list[i].icon;

            _createdItemIcons.Add(createdIcon);
        }
    }
}
