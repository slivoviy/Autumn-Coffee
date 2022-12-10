using UnityEngine;
using UnityEngine.UI;

using DG.Tweening;
using TMPro;


[RequireComponent(typeof(TMP_Text))]
public class PopupTextUI : MonoBehaviour
{
    [SerializeField] private float _addedScale = 0.5f;
    [SerializeField] private float _duration = 1f;
    [SerializeField] private float _destroyTime = 1.2f;
    [SerializeField] private float _fadeDuration = 1.4f;

    private TMP_Text _tmpText => GetComponent<TMP_Text>();

    private void Start()
    {
        transform.DOScale(new Vector3(transform.localScale.x + _addedScale, transform.localScale.y + _addedScale, transform.localScale.z), _duration);
        _tmpText.DOFade(0, _fadeDuration);

        Destroy(gameObject, 2f);
    }
}
