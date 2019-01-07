using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupNoteManager : MonoBehaviour
{
    public static PopupNoteManager instance;

    public GameObject BottomInfo;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        BottomInfo.SetActive(false);
    }

    public void UpdateBottomInfoText(string text)
    {
        BottomInfo.GetComponentInChildren<Text>().text = text;
    }
}