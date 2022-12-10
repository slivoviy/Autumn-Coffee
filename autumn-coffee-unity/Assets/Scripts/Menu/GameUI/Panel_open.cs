using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel_open : MonoBehaviour
{
    public GameObject Panel;

    public void OpenPanel()
    {
        if (Panel != null)
        {
            Panel.SetActive(true);
        }

    }
    public void ClosePanel()
    {
        if (Panel != null)
        {
            StartCoroutine(waiter());

        }

    }
    IEnumerator waiter()
    {
        yield return new WaitForSeconds(1);
        Panel.SetActive(false);
    }
}
