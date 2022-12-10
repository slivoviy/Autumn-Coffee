using System.Collections;
using System.Collections.Generic;
using Articy.Unity;
using UnityEngine;
using Ruinum.Core;
using DG.Tweening;
using DialogueSystem;
using Ruinum.Utils;

public class CustomersSystem : BaseSingleton<CustomersSystem> {
    public float minTime;
    public float maxTime;

    public List<GameObject> TodaySpecialCustomers { get; set; }

    [SerializeField] private List<GameObject> specialCustomersGO;

    private Dictionary<DayType, List<GameObject>> allSpecialCustomers = new Dictionary<DayType, List<GameObject>>();
    private int customersCount;
    private bool[] Place = new bool[3];
    private float spawningProbability = 1f;
    private bool specialExists;

    private readonly Vector3 startPos = new Vector3(-4.5f, -2, 0);
    private readonly Vector3 startPos2 = new Vector3(-8f, -2, 0);

    protected override void Awake() {
        base.Awake();
        
        InitializeSchedule();
    }

    private void Start() {
        SubscribeSchedule();
        StartCoroutine(Generator());
    }

    private GameObject GetCustomer() {
        if (TodaySpecialCustomers == null || TodaySpecialCustomers.Count == 0)
            return Resources.Load<GameObject>("Prefabs/CustomerTest");

        if (!RandomExtentions.RandomLess(spawningProbability)) {
            specialExists = true;
            GameObject customer = TodaySpecialCustomers[0];
            TodaySpecialCustomers.Remove(customer);

            if (spawningProbability > 0.4f) spawningProbability -= 0.4f;

            return customer;
        }

        spawningProbability += 0.1f;
        return Resources.Load<GameObject>("Prefabs/CustomerTest");
    }

    private void CreateNewCustomer() {
        GameObject customerGO = Instantiate(GetCustomer(), null);
        int cPos = 1;
        for (int i = 0; i < 3; i++) {
            if (!Place[i]) {
                cPos = i;
                Place[i] = true;
                break;
            }
        }

        customerGO.transform.position = startPos2;
        var customer = customerGO.GetComponent<Customer>();
        customer._Pos = cPos;

        customersCount++;
        ComeAnimation2(customerGO, startPos + ((transform.right * 4.5f) * cPos));
    }

    private static void ComeAnimation2(GameObject @object, Vector3 _Pos) {
        Sequence mySequence = DOTween.Sequence();

        mySequence.Append(@object.transform.DOMove(_Pos, 1));
        mySequence.Append(@object.transform.DOPunchScale(new Vector3(.2f, .2f, .2f), 0.25f));
    }

    public void CustomerLeave(GameObject customer) {
        specialExists = false;
        customersCount--;
        Place[customer.GetComponent<Customer>()._Pos] = false;
        Destroy(customer);
    }

    private IEnumerator Generator() {
        for (;;) {
            yield return new WaitForSeconds(Random.Range(minTime, maxTime));
            if (!specialExists && customersCount < 3) CreateNewCustomer();
        }
    }

    private void InitializeSchedule() {
        //0 - Barista Friend, 1 - Evil Barista, 2 - Kind Old Man, 3 - Reviewer, 4 - Tired Clerk
        allSpecialCustomers.Add(DayType.Monday, new List<GameObject> {specialCustomersGO[0], specialCustomersGO[1]});

        allSpecialCustomers.Add(DayType.Tuesday,
            new List<GameObject> {specialCustomersGO[4], specialCustomersGO[3], specialCustomersGO[2]});

        allSpecialCustomers.Add(DayType.Wednesday,
            new List<GameObject> {specialCustomersGO[1], specialCustomersGO[3], specialCustomersGO[2]});

        allSpecialCustomers.Add(DayType.Thursday, new List<GameObject> {specialCustomersGO[4], specialCustomersGO[2]});

        allSpecialCustomers.Add(DayType.Friday, new List<GameObject> {specialCustomersGO[1], specialCustomersGO[2]});

        allSpecialCustomers.Add(DayType.Saturday, new List<GameObject> {specialCustomersGO[2], specialCustomersGO[3]});

        allSpecialCustomers.Add(DayType.Sunday, new List<GameObject> {specialCustomersGO[1], specialCustomersGO[2]});
    }

    private void SubscribeSchedule() {
        foreach (var pair in allSpecialCustomers) {
            WeekSystem.Singleton.AddDayLogic(new CustomersSchedule {Customers = pair.Value}, pair.Key);
        }
    }
}

public class CustomersSchedule : IDayLogic {
    public List<GameObject> Customers { get; set; }

    public void DayLogic() {
        CustomersSystem.Singleton.TodaySpecialCustomers = Customers;
    }
}