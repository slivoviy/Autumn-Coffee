using System;
using UnityEngine;


public class TrashCan : MonoBehaviour
{
    [SerializeField] private GameObject _canvas;

    private void Start()
    {
        _canvas.SetActive(false);
    }

    private void OnMouseEnter()
    {
        _canvas.SetActive(true);
    }

    private void OnMouseExit()
    {
        _canvas.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Destroy(other.gameObject);
    }
}