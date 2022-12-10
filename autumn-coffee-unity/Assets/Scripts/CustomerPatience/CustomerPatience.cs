using UnityEngine;
using Ruinum.Core;
using System.Collections;
using TMPro;

public class CustomerPatience : MonoBehaviour
{
    [SerializeField] private float waitingTime = 30;
    private int amount = 100;

    private void Start() => StartCoroutine(StartWaiting());

    private IEnumerator StartWaiting()
    {
        yield return new WaitForSeconds(waitingTime);
        
        NotificationSystem.Singleton.Notify("You take too long to place an order. The client's patience is not eternal! Hurry up!");

        yield return new WaitForSeconds(waitingTime);

        NotificationSystem.Singleton.Notify("The client is tired of waiting! He's gone...");
        MoneySystem.Singleton.SubtractAmount(amount);
    }
}
