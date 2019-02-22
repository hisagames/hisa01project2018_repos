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
                ChatNPCSetting.NPCBankChat[] tempNBC = ChatNPCSetting.instance.npcBankChat;
                NPCId.id tempNPCId = Player.instance.collideNPCId;

                nameText.text = tempNBC[(int)(tempNPCId)].name;
                chatImageShot.sprite = tempNBC[(int)(tempNPCId)].npcImageShot;

                //check or validate type text ----------------------------------------------------
                bool tempState = false;
                ChatNPCSetting.NPCBankChat.SpecificChat[] tempSpecificChat = tempNBC[(int)(tempNPCId)].specificChat;
                int tempTime = TimeManager.instance.getGameTimeInMinutes();

                for (int i = 0; i < tempSpecificChat.Length; i++)
                {
                    if(tempTime >= tempSpecificChat[i].minTime && tempTime <= tempSpecificChat[i].maxTime)
                    {
                        tempState = true;
                        chatText.text = tempSpecificChat[i].text;
                        break;
                    }
                }

                if (!tempState)
                    chatText.text = tempNBC[(int)(tempNPCId)].defaultChatText;
                //--------------------------------------------------------------------------------
            }
            isChatUIOpen = state;
            ChatUI.SetActive(isChatUIOpen);
        }
    }
}
