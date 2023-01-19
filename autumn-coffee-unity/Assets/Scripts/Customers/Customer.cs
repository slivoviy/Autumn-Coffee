using System.Collections.Generic;
using Ruinum.Core;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Customers {
    public class Customer : Executable {
        [Header("Waiting Time Settings")]
        [SerializeField] protected float timeToWait;
        [SerializeField] protected float minChangeTime;
        [SerializeField] protected float maxChangeTime;
        
        [Header("UI Settings")]
        [SerializeField] private GameObject orderBubble;
        [SerializeField] private List<TextMeshProUGUI> bubbleTextComponents;
        [SerializeField] private RectTransform patienceMeter;
        
        [Header("Money Reward Settings")]
        [SerializeField] private float minRandom;
        [SerializeField] private float maxRandom;
        [SerializeField] private float constantMoney;

        [HideInInspector] public Task task;
        [HideInInspector] public int pos;
        [HideInInspector] public bool isTaskCreated;

        protected Timer _timerToLeave;
        private bool _clicked;

        private new void Start() {
            timeToWait += Random.Range(minChangeTime, maxChangeTime);
            _timerToLeave = TimerManager.Singleton.StartTimer(timeToWait * 3, TryLeave);
            
            base.Start();
        }

        public override void Execute() {
            patienceMeter.sizeDelta = _clicked ? new Vector2(_timerToLeave.GetCurrentTime() / timeToWait * 3, 0.5f) : new Vector2(_timerToLeave.GetCurrentTime() / (timeToWait * 3) * 3, 0.5f);
        }

        protected virtual void AddTask() {
            if (isTaskCreated) return;

            task = TaskManager.Singleton.CreateTask(this);
            isTaskCreated = true;

            ReloadUI();
        }

        public void ReloadUI() {
            orderBubble.SetActive(true);

            //0 - coffee; 1 - syrup; 2 - topping; 3-4 - dessert
            var hasDessert = false;
            foreach (var item in task.order) {
                switch (item.type) {
                    case ItemType.Coffee:
                        bubbleTextComponents[0].gameObject.SetActive(!task.IsItemInOrder(item));
                        bubbleTextComponents[0].text = item.itemName;
                        break;
                    case ItemType.Syrup:
                        bubbleTextComponents[1].gameObject.SetActive(!task.IsItemInOrder(item));
                        bubbleTextComponents[1].text = "- " + item.itemName;
                        break;
                    case ItemType.Topping:
                        bubbleTextComponents[2].gameObject.SetActive(!task.IsItemInOrder(item));
                        bubbleTextComponents[2].text = "- " + item.itemName;
                        break;
                    case ItemType.Dessert:
                        if (!hasDessert) {
                            bubbleTextComponents[3].gameObject.SetActive(!task.IsItemInOrder(item));
                            bubbleTextComponents[3].text = item.itemName;
                            hasDessert = true;
                        }
                        else {
                            bubbleTextComponents[4].gameObject.SetActive(!task.IsItemInOrder(item));
                            bubbleTextComponents[4].text = item.itemName;
                        }
                        break;
                    default:
                        Debug.Log("ItemType not found");
                        break;
                }
            }
        }

        public virtual void TryLeave() {
            if (!task.CheckComplete() && _timerToLeave.GetCurrentTime() > 0) return;

            CustomersSystem.Singleton.CustomerLeave(gameObject);

            var randomMoney = (int)(Random.Range(minRandom, maxRandom) + constantMoney);

            if (task.CheckCorrect()) MoneySystem.Singleton.AddAmount(randomMoney);

            GameManager.Singleton.RemoveExecuteObject(this);

            task = null;
        }

        private void OnMouseDown() {
            if (_clicked) return;

            _clicked = true;

            AddTask();
            TimerManager.Singleton.DeleteTimer(_timerToLeave);
            _timerToLeave = TimerManager.Singleton.StartTimer(timeToWait, TryLeave);
        }
    }
}