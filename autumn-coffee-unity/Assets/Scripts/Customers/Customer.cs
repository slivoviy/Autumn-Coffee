using System.Collections.Generic;
using UnityEngine;
using Ruinum.Core;
using TMPro;
using Random = UnityEngine.Random;

public class Customer : Executable {
    public Task task;
    public float TimeToWait;
    public float MinChangeTime;
    public float MaxChangeTime;

    [SerializeField] private GameObject _orderBubble;
    [SerializeField] private List<TextMeshProUGUI> _bubbleTextComponents;
    [SerializeField] private RectTransform patienceMeter;

    [Header("Money Reward Settigns")]
    [SerializeField] private float _minRandom;
    [SerializeField] private float _maxRandom;
    [SerializeField] private float _constantMoney;

    [HideInInspector] public int _Pos;

    private Timer _timerToLeave;
    public bool _isTaskCreated;
    private bool clicked;


    public override void Execute() {
        if (clicked) {
            patienceMeter.sizeDelta = new Vector2(_timerToLeave.GetCurrentTime() / TimeToWait * 3, 0.5f);
        }
    }

    protected virtual void AddTask() {
        if (_isTaskCreated) return;

        task = TaskManager.Singleton.CreateTask(this);
        _isTaskCreated = true;

        ReloadUI();
    }

    public void ReloadUI() {
        _orderBubble.SetActive(true);

        //0 - coffee; 1 - syrup; 2 - topping; 3-4 - dessert
        var hasDessert = false;
        foreach (var item in task.Order) {
            switch (item.type) {
                case ItemType.Coffee:
                    _bubbleTextComponents[0].gameObject.SetActive(!task.IsItemInOrder(item));
                    _bubbleTextComponents[0].text = item.itemName;
                    break;
                case ItemType.Syrup:
                    _bubbleTextComponents[1].gameObject.SetActive(!task.IsItemInOrder(item));
                    _bubbleTextComponents[1].text = "- " + item.itemName;
                    break;
                case ItemType.Topping:
                    _bubbleTextComponents[2].gameObject.SetActive(!task.IsItemInOrder(item));
                    _bubbleTextComponents[2].text = "- " + item.itemName;
                    break;
                case ItemType.Dessert:
                    if (!hasDessert) {
                        _bubbleTextComponents[3].gameObject.SetActive(!task.IsItemInOrder(item));
                        _bubbleTextComponents[3].text = item.itemName;
                        hasDessert = true;
                    }
                    else {
                        _bubbleTextComponents[4].gameObject.SetActive(!task.IsItemInOrder(item));
                        _bubbleTextComponents[4].text = item.itemName;
                    }
                    break;
                default:
                    Debug.Log("ItemType not found");
                    break;
            }
        }
    }

    public virtual void TryLeave() {
        if (!task.completed && _timerToLeave.GetCurrentTime() > 0) return;

        CustomersSystem.Singleton.CustomerLeave(gameObject);

        var randomMoney = (int)(Random.Range(_minRandom, _maxRandom) + _constantMoney);

        if (task.correct) MoneySystem.Singleton.AddAmount(randomMoney);

        GameManager.Singleton.RemoveExecuteObject(this);

        task = null;
    }

    private void OnMouseDown() {
        clicked = true;

        AddTask();

        TimeToWait += Random.Range(MinChangeTime, MaxChangeTime);
        _timerToLeave = TimerManager.Singleton.StartTimer(TimeToWait, TryLeave);
    }
}