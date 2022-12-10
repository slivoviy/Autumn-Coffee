using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Notification : MonoBehaviour
{
    private Animator animator;

    private int existTime = 15;

    private void Start()
    {
        animator = GetComponent<Animator>();

        animator.SetTrigger("in");

        StartCoroutine(ShowNotification());
    }

    private IEnumerator ShowNotification()
    {
        yield return new WaitForSeconds(existTime);
        animator.SetTrigger("out");
    }

    public void DeleteNotification()
    {
        Destroy(gameObject);
    }
}
