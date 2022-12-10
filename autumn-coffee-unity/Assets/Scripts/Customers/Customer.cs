using System.Collections.Generic;
using Articy.Unity;
using UnityEngine;
using Ruinum.Core;
using TMPro;

public class Customer : Executable
{
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


    public override void Execute()
    {
        if (clicked) {
            patienceMeter.sizeDelta = new Vector2(_timerToLeave.GetCurrentTime() / TimeToWait * 3, 0.5f);
        }
    }

    protected virtual void AddTask()
    {
        if (_isTaskCreated) return;

        task = TaskManager.Singleton.CreateTask(this);
        _isTaskCreated = true;

        InitializeTaskUI();
    }

    protected void InitializeTaskUI()
    {
        _orderBubble.SetActive(true);
        //0 - coffee; 1 - syrup; 2 - topping; 3-4 - dessert
        bool hasDessert = false;
        foreach (var dish in task.Dish)
        {
            if (dish.Item.IsSyrup)
            {
                _bubbleTextComponents[1].gameObject.SetActive(true);
                _bubbleTextComponents[1].text = "- " + dish.Item.Name;
            }
            else if (dish.Item.IsTopping)
            {
                _bubbleTextComponents[2].gameObject.SetActive(true);
                _bubbleTextComponents[2].text = "- " + dish.Item.Name;
            }
            else if (dish.Item.IsDessert)
            {
                if (!hasDessert)
                {
                    _bubbleTextComponents[3].gameObject.SetActive(true);
                    _bubbleTextComponents[3].text = dish.Item.Name;
                    hasDessert = true;
                }
                else
                {
                    _bubbleTextComponents[4].gameObject.SetActive(true);
                    _bubbleTextComponents[4].text = dish.Item.Name;
                }
            }
            else
            {
                _bubbleTextComponents[0].gameObject.SetActive(true);
                _bubbleTextComponents[0].text = dish.Item.Name;
            }
        }
    }

    public virtual void Leave()
    {
        ReviewsSystem.Singletone.ChangeRating(task.completed ? 2 : -2);
        CustomersSystem.Singleton.CustomerLeave(gameObject);

        int randomMoney = (int)(Random.Range(_minRandom, _maxRandom) + _constantMoney);
        
        if (task.completed) MoneySystem.Singleton.AddAmount(randomMoney);
        else MoneySystem.Singleton.SubtractAmount(randomMoney);

        GameManager.Singleton.RemoveExecuteObject(this);

        task = null;
    }

    private void OnMouseDown() {
        clicked = true;
        
        AddTask();

        TimeToWait += Random.Range(MinChangeTime, MaxChangeTime);
        _timerToLeave = TimerManager.Singleton.StartTimer(TimeToWait, Leave);
    }
}