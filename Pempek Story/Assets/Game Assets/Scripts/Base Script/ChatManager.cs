using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatManager : MonoBehaviour
{
    public static ChatManager instance;

    [SerializeField]
    GameObject ChatUI;
    bool isChatUIOpen;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        isChatUIOpen = false;
        changeChatUIOpenState(false);
    }

    public void changeChatUIOpenState(bool state)
    {
        if (isChatUIOpen != state)
        {
            isChatUIOpen = state;
            ChatUI.SetActive(isChatUIOpen);
        }
    }
}
