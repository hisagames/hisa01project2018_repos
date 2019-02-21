using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChatManager : MonoBehaviour
{
    public static ChatManager instance;

    [SerializeField]
    GameObject ChatUI;
    bool isChatUIOpen;

    [SerializeField]
    Image chatImageShot;
    [SerializeField]
    TextMeshProUGUI nameText;
    [SerializeField]
    TextMeshProUGUI chatText;

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
            if(state)
            {
                int tempNPCId = 0;
                //its still temporary, there is 2 function coming soon in this place
                //1 get NPCId who collide with player
                //2 validate what chat type will be choosen to be showed depend on state or some conditions

                nameText.text = ChatNPCSetting.instance.npcBankChat[tempNPCId].name;
                chatImageShot.sprite = ChatNPCSetting.instance.npcBankChat[tempNPCId].npcImageShot;
                chatText.text = ChatNPCSetting.instance.npcBankChat[tempNPCId].defaultChatText;
            }
            isChatUIOpen = state;
            ChatUI.SetActive(isChatUIOpen);
        }
    }
}
