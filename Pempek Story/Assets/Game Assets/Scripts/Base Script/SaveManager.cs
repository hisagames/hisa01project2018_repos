using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;

    public GameObject SaveConfirmationInfo;
    public int inYesConfirmationState; //0 = yes, 1 = no, 2 = don't want to sleep

    [SerializeField]
    GameObject buttonFrame;
    [SerializeField]
    GameObject buttonYes;
    [SerializeField]
    GameObject buttonNo;
    [SerializeField]
    GameObject buttonAwake;

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
        SaveConfirmationInfo.SetActive(false);
    }

    public void changeSaveConfirmationInfo(bool state)
    {
        SaveConfirmationInfo.SetActive(state);
        if (state)
        {
            inYesConfirmationState = 0;
            updateSaveConfirmationState(inYesConfirmationState);
        }
    }

    public void updateSaveConfirmationState(int saveState)
    {
        inYesConfirmationState = saveState;
        switch (saveState)
        {
            case 0:
                buttonFrame.transform.position = buttonYes.transform.position;
                buttonFrame.GetComponent<RectTransform>().sizeDelta = buttonYes.GetComponent<RectTransform>().sizeDelta;
                buttonYes.GetComponent<Text>().color = new Color(colorTextActive.r, colorTextActive.g, colorTextActive.b);
                buttonNo.GetComponent<Text>().color = new Color(colorTextNotActive.r, colorTextNotActive.g, colorTextNotActive.b);
                buttonAwake.GetComponent<Text>().color = new Color(colorTextNotActive.r, colorTextNotActive.g, colorTextNotActive.b);
                break;
            case 1:
                buttonFrame.transform.position = buttonNo.transform.position;
                buttonFrame.GetComponent<RectTransform>().sizeDelta = buttonNo.GetComponent<RectTransform>().sizeDelta;
                buttonYes.GetComponent<Text>().color = new Color(colorTextNotActive.r, colorTextNotActive.g, colorTextNotActive.b);
                buttonNo.GetComponent<Text>().color = new Color(colorTextActive.r, colorTextActive.g, colorTextActive.b);
                buttonAwake.GetComponent<Text>().color = new Color(colorTextNotActive.r, colorTextNotActive.g, colorTextNotActive.b);
                break;
            case 2:
                buttonFrame.transform.position = buttonAwake.transform.position;
                buttonFrame.GetComponent<RectTransform>().sizeDelta = buttonAwake.GetComponent<RectTransform>().sizeDelta;
                buttonYes.GetComponent<Text>().color = new Color(colorTextNotActive.r, colorTextNotActive.g, colorTextNotActive.b);
                buttonNo.GetComponent<Text>().color = new Color(colorTextNotActive.r, colorTextNotActive.g, colorTextNotActive.b);
                buttonAwake.GetComponent<Text>().color = new Color(colorTextActive.r, colorTextActive.g, colorTextActive.b);
                break;
        }
    }
}
