using UnityEngine;
using DG.Tweening;


public class RotateCameraLogic : MonoBehaviour
{
    [SerializeField] private Transform _camera;
    [SerializeField] private float _duration;

    public void RotateLogic(int direction)
    {
        _camera.transform.DOBlendableRotateBy(new Vector3(0, 180 * direction, 0), _duration);
    }
}
