using UnityEngine;
using DG.Tweening;


public class TransformObject : MonoBehaviour
{
    [SerializeField] private Transform _object;
    [SerializeField] private Transform _position;

    [Header("Move Settings")]
    [SerializeField] private float _duration;

    private Vector3 _startObjectPosition;

    private void OnMouseEnter()
    {
        _startObjectPosition = _object.transform.position;

        _object.DOMoveY(_position.position.y, _duration, false);        
    }

    private void OnMouseExit()
    {
        _object.DOMoveY(_startObjectPosition.y, _duration, false);
    }
}