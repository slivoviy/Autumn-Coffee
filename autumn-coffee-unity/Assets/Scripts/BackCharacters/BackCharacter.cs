using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackCharacter : MonoBehaviour
{
    [SerializeField] private float minExistTime = 3;
    [SerializeField] private float maxExistTime = 5;

    private float existTime = 5;
    
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();

        existTime = Random.Range(minExistTime, maxExistTime);

        animator.SetTrigger("in");
        StartCoroutine(ExistBC());
    }

    private IEnumerator ExistBC()
    {
        yield return new WaitForSeconds(existTime);
        animator.SetTrigger("out");
    }

    public void DeleteBC()
    {
        Destroy(gameObject);
    }
}
