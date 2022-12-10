using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ruinum.Core;
using TMPro;

public class NotificationSystem : BaseSingleton<NotificationSystem>
{
    //[SerializeField] private TextMeshProUGUI notificationText;

    private void Start() 
    { 
        //notificationText.text = "";
    }

    public void Notify(string text)
    {
        //notificationText.text = text;
        //StartCoroutine(ShowNotification());

        GameObject not = Instantiate(Resources.Load<GameObject>("Prefabs/Notification"), null);
        not.transform.SetParent(gameObject.transform, false);

        not.GetComponentInChildren<TextMeshProUGUI>().text = text;
    }
}
