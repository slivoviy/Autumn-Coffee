using UnityEngine;
using DG.Tweening;

public class AnimationObject : MonoBehaviour
{
    [Header("Animation Settings")]
    [SerializeField] private float _addedScale = 0.05f;
    [SerializeField] private float _duration = 0.2f;
    [SerializeField] private int _vibratio = 2;
    [SerializeField] private float _elastic = 0.2f;

    public void AnimationPunch()
    {
        transform.DOPunchScale(new Vector3(transform.localScale.x + _addedScale, transform.localScale.y + _addedScale, transform.localScale.z + _addedScale), _duration, _vibratio, _elastic);
    }
}
