using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SleepManager : MonoBehaviour
{
    public static SleepManager instance;

    public GameObject SleepConfirmationInfo;
    public bool inYesConfirmationState;

    [SerializeField]
    GameObject buttonFrame;
    [SerializeField]
    GameObject buttonYes;
    [SerializeField]
    GameObject buttonNo;

    [SerializeField]
    Color colorTextActive;
    [SerializeField]
    Color colorTextNotActive;

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
        if (state)
        {
            inYesConfirmationState = false;
            updateSleepConfirmationState(false);
        }
    }

    public void updateSleepConfirmationState(bool state)
    {
        inYesConfirmationState = state;
        if (state)
        {
            buttonFrame.transform.position = buttonYes.transform.position;
            buttonYes.GetComponent<Text>().color = new Color(colorTextActive.r, colorTextActive.g, colorTextActive.b);
            buttonNo.GetComponent<Text>().color = new Color(colorTextNotActive.r, colorTextNotActive.g, colorTextNotActive.b);
        }
        else
        {
            buttonFrame.transform.position = buttonNo.transform.position;
            buttonYes.GetComponent<Text>().color = new Color(colorTextNotActive.r, colorTextNotActive.g, colorTextNotActive.b);
            buttonNo.GetComponent<Text>().color = new Color(colorTextActive.r, colorTextActive.g, colorTextActive.b);
        }
    }
}