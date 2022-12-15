using System.Collections.Generic;

using TMPro;

using UnityEngine;
using UnityEngine.UI;


public class CookBookUI : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private TMP_Text _name;
    [SerializeField] private TMP_Text _description;
    [SerializeField] private Transform _layoutGroup;

    [SerializeField] private GameObject _itemUIPrefab;

    [SerializeField] private RecipeSO[] _receptSOs;

    private List<GameObject> _createdRecepts = new List<GameObject>();

    private int _index = 0;

    public void Open()
    {
        gameObject.SetActive(!gameObject.activeSelf);
        SetInformation(_receptSOs[Mathf.Min(_index, _receptSOs.Length)]);
    }

    public void NextRecept()
    {
        if (_index + 1 > _receptSOs.Length) return;

        _index++;

        SetInformation(_receptSOs[_index]);
    }

    public void PreviousRecept()
    {
        if (_index - 1 < 0) return;

        _index--;

        SetInformation(_receptSOs[_index]);
    }

    private void SetInformation(RecipeSO recipeSo)
    {
        _image.sprite = recipeSo.ResultItem.icon;
        _name.text = recipeSo.ResultItem.itemName;
        _description.text = recipeSo.ResultItem.description;

        for (int i = 0; i < _createdRecepts.Count; i++)
        {
            Destroy(_createdRecepts[i]);
        }

        for (int i = 0; i < recipeSo.RequestItems.Length; i++)
        {
            var createdItemUI = Instantiate(_itemUIPrefab, _layoutGroup);
            createdItemUI.GetComponent<Image>().sprite = recipeSo.RequestItems[i].icon;
            _createdRecepts.Add(createdItemUI);
        }
    }
}
