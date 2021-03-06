﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupNote : MonoBehaviour
{
    public enum PopupNoteType
    {
        BottomInfo,
        NPCCollide
    };

    [SerializeField]
    string popupNoteString;

    public PopupNoteType popupNoteType;
    public string popupNoteKey;

    [SerializeField]
    NPCId.id npcId; //just used if PopupNoteType == NPCCollide

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            switch (popupNoteType)
            {
                case PopupNoteType.BottomInfo:
                    if (popupNoteString != "")
                    {
                        PopupNoteManager.instance.BottomInfo.SetActive(true);
                        PopupNoteManager.instance.UpdateBottomInfoText(popupNoteString);
                    }
                    Player.instance.inCollisionKey = popupNoteKey;
                    break;
                case PopupNoteType.NPCCollide:
                    if (popupNoteString != "")
                    {
                        PopupNoteManager.instance.BottomInfo.SetActive(true);
                        PopupNoteManager.instance.UpdateBottomInfoText(popupNoteString);
                    }
                    Player.instance.collideNPCId = npcId;
                    Player.instance.inCollisionKey = popupNoteKey;
                    break;
            }
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            switch (popupNoteType)
            {
                case PopupNoteType.BottomInfo:
                    PopupNoteManager.instance.BottomInfo.SetActive(false);
                    Player.instance.inCollisionKey = "";
                    break;
                case PopupNoteType.NPCCollide:
                    PopupNoteManager.instance.BottomInfo.SetActive(false);
                    Player.instance.inCollisionKey = "";
                    break;
            }
        }
    }
}