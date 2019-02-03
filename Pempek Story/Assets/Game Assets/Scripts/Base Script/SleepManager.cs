using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepManager : MonoBehaviour
{
    public static SleepManager instance;

    public GameObject SleepConfirmationInfo;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        SleepConfirmationInfo.SetActive(false);
    }

    public void changeSleepConfirmationInfo(bool state)
    {
        SleepConfirmationInfo.SetActive(state);
    }
}