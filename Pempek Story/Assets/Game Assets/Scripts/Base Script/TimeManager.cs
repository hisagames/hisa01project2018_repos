using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    public static TimeManager instance;

    #region Time Setting Attribute
    [SerializeField]
    Text TimeText;

    [SerializeField]
    Text TimeStampText;

    int Hours;
    int Minutes;
    string timeStamp;
    #endregion

    #region Date Setting Attribute
    [SerializeField]
    Image MonthIcon; //Seasons Icon

    [SerializeField]
    Text DateText;

    [SerializeField]
    Text DayWeekNameText;

    [SerializeField]
    string[] DayWeekName;

    int Day;
    int DayWeek;
    int Month;
    int Year;

    #endregion

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        DateTimeInitiate();
        DateTimeValidate();

        InvokeRepeating("UpdateTime", 0f, TimeSetting.timeSpeed);
    }

    void DateTimeInitiate()
    {
        Hours = PlayerPrefs.GetInt("Hours");
        Minutes = PlayerPrefs.GetInt("Minutes");
        Day = PlayerPrefs.GetInt("Day");
        DayWeek = PlayerPrefs.GetInt("DayWeek");
        Month = PlayerPrefs.GetInt("Month");
        Year = PlayerPrefs.GetInt("Year");
    }

    void DateTimeValidate()
    {
        if (Day == 0) Day = 1;
        if (Month == 0) Month = 1;
        if (Year == 0) Year = 1;
        if (DayWeek == 0) DayWeek = 1;

        if (Day > TimeSetting.maxDay)
        {
            Day = Day % TimeSetting.maxDay;
            PlayerPrefs.SetInt("Day", Day);
        }
        if (Month > TimeSetting.maxMonth)
        {
            Month = Month % TimeSetting.maxMonth;
            PlayerPrefs.SetInt("Month", Month);
        }
        if (DayWeek > TimeSetting.maxDayWeek)
        {
            DayWeek = DayWeek % TimeSetting.maxDayWeek;
            PlayerPrefs.SetInt("DayWeek", DayWeek);
        }
    }

    void UpdateTime()
    {
        Minutes += 10;
        PlayerPrefs.SetInt("Minutes", Minutes);

        if (Minutes >= TimeSetting.maxMinutes)
        {
            Hours += Minutes / TimeSetting.maxMinutes;
            PlayerPrefs.SetInt("Hours", Hours);

            if (Hours >= TimeSetting.maxHours)
            {
                Day += 1;
                PlayerPrefs.SetInt("Day", Day);

                if (Day > TimeSetting.maxDay)
                {
                    Month += 1;
                    PlayerPrefs.SetInt("Month", Month);

                    if (Month > TimeSetting.maxMonth)
                    {
                        Year += 1;
                        PlayerPrefs.SetInt("Year", Year);
                    }

                    Day = 1;
                }

                DayWeek += 1;
                PlayerPrefs.SetInt("DayWeek", DayWeek);
                if (DayWeek > TimeSetting.maxDayWeek)
                    DayWeek = 1;

                Hours = 0;
            }

            Minutes = Minutes % TimeSetting.maxMinutes;
        }

        if (Hours < TimeSetting.maxHours / 2)
            timeStamp = "AM";
        else
            timeStamp = "PM";

        UpdateTimeUI();
        UpdateDateUI();
    }

    void UpdateTimeUI()
    {
        TimeStampText.text = timeStamp;

        string temp = "";

        if (Hours % (TimeSetting.maxHours / 2) < 10)
            temp += "0";
        temp += (Hours % (TimeSetting.maxHours / 2)) + ":";

        if (Minutes < TimeSetting.doubleDigit)
            temp += "0";
        temp += Minutes + "";

        TimeText.text = temp;
    }

    void UpdateDateUI()
    {
        DateText.text = Day + "";
        MonthIcon.sprite = TimeSetting.monthIcon[Month - 1];
        
        DayWeekNameText.text = DayWeekName[DayWeek - 1];
    }

    public void addTransitionMinutes()
    {
        Minutes += 30;
        PlayerPrefs.SetInt("Minutes", Minutes);
    }
}