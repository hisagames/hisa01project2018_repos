using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeSetting : MonoBehaviour
{
    public static float timeSpeed = 10f; //every time speed, time in game will add Normal minutes

    public static int maxMinutes = 60; //minutes
    public static int maxHours = 24; //hours
    public static int maxDayWeek = 7; //days
    public static int maxDay = 30; //days
    public static int maxMonth = 4; //seasons
    public static Sprite[] monthIcon; //seasonsicon
    public static int doubleDigit = 10; //starter double digit numbers

    public static int addNormalMinutes = 10; //add some minutes every timeSpeed
    public static int addTransitionMinutes = 30; //add some minutes every scene transition

    [SerializeField]
    Sprite[] _monthIcon;

    void Awake()
    {
        monthIcon = _monthIcon;
    }
}
