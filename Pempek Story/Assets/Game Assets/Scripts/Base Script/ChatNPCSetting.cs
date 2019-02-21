using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatNPCSetting : MonoBehaviour
{
    public static ChatNPCSetting instance;

    [System.Serializable]
    public class NPCBankChat
    {
        [System.Serializable]
        public struct SpecificChat
        {
            public string text;
            public int minTime;
            public int maxTime;
            public int day;
        }

        [System.Serializable]
        public struct EventChat
        {
            public string text;
            public int month;
            public int date;
            public int eventState;
        }

        public string name;
        public Sprite npcImageShot;
        public SpecificChat[] specificChat;
        public string defaultChatText;
        public string getGreatGiftChatText;
        public string getGoodGiftChatText;
        public string getNormalGiftChatText;
        public string getBadGiftChatText;
        public string getBirthdayGiftChatText;
        public EventChat[] eventChat;
    }

    public NPCBankChat[] npcBankChat;

    void Awake()
    {
        instance = this;
    }
}