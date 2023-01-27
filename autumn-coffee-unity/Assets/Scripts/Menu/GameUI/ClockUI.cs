using UnityEngine;

using TMPro;


public class ClockUI : MonoBehaviour
{
    public const float IRL_secs_per_ingame_day = 600f; //1 день в игре = 5 минут 300

    public Transform clockHourHandTransform;
    public Transform clockMinHandTransform;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI CurrentDayText;
    public TextMeshProUGUI MonthText;

    public float day;
    public int theCurrentDay;
    public bool end_of_day;
    private bool _nextdaystarted;
    private bool dayInitialized;

    public void Start()
    {
        clockHourHandTransform = transform.Find("Hour_hand");
        clockMinHandTransform = transform.Find("Min_hand");
        end_of_day = false;
        theCurrentDay = WeekSystem.Singleton.GetDayNum();
        CurrentDayText.text = theCurrentDay.ToString();
        string[] weekdays = { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
        MonthText.text = weekdays[(theCurrentDay - 1) % 7];
    }


    public void Update()
    {
        if (end_of_day != true)
        {
            day += Time.deltaTime / IRL_secs_per_ingame_day;

            float dayNormalized = day % 1f;


            float rotationDegreesPerDay = 720f;
            clockHourHandTransform.eulerAngles = new Vector3(0, 0, -dayNormalized * rotationDegreesPerDay + 90f);


            float hoursPerDay = 24f;
            clockMinHandTransform.eulerAngles = new Vector3(0, 0, -dayNormalized * 360f * hoursPerDay);

            string hoursString = Mathf.Floor((dayNormalized * 24f + 9f)).ToString("00");
            if (hoursString != "19")
            {
                string minString = Mathf.Floor(((dayNormalized * 24f) % 1f) * 60f).ToString("00");
                string TextString = hoursString + ":" + minString;
                timeText.text = TextString;
            }
            else
            {
                timeText.text = "19:00";
                end_of_day = true;
            }
            
        }
        else
        {
            
            if (!_nextdaystarted) { EndOfDay(); _nextdaystarted = true; }
        }

    }

    private void LateUpdate() {
        if (dayInitialized) return;
        
        WeekSystem.Singleton.InvokeDayLogic();
        dayInitialized = true;
    }

    public void EndOfDay()
    {
        WeekSystem.Singleton.AddDay();
        WeekSystem.Singleton.ChangeDay();
    }
}
