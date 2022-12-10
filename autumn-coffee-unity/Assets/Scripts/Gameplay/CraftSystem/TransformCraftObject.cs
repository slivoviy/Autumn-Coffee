using UnityEngine;


public class TransformCraftObject : MonoBehaviour
{
    [SerializeField] private Transform _transformPosition;

    public void TransformObject(GameObject gameObject)
    {
        gameObject.transform.position = _transformPosition.position;
    }
}
