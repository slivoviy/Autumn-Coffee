using Ruinum.Utils;
using UnityEngine;


public class CreateObject : MonoBehaviour
{
    [SerializeField] private GameObject _object;
    [SerializeField] GameObject _hintCanvas;

    private GameObject _createdObject;

    private void Start()
    {
        _hintCanvas.SetActive(false);
    }

    private void OnMouseDown()
    {
        _createdObject = Instantiate(_object);
        _createdObject.transform.position = transform.position;
        _createdObject.transform.position = new Vector3(MouseUtils.GetMouseWorld2DPosition().x, MouseUtils.GetMouseWorld2DPosition().y, _createdObject.transform.position.z);
    }

    private void OnMouseDrag()
    {
        _createdObject.transform.position = new Vector3(MouseUtils.GetMouseWorld2DPosition().x, MouseUtils.GetMouseWorld2DPosition().y, _createdObject.transform.position.z);
    }

    private void OnMouseUp()
    {
        _createdObject = null;
    }

    private void OnMouseEnter()
    {
        _hintCanvas.SetActive(true);
    }

    private void OnMouseExit()
    {
        _hintCanvas.SetActive(false);
    }
}
