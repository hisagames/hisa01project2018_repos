using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupNote : MonoBehaviour
{
    enum PopupNoteType
    {
        BottomInfo
    };

    [SerializeField]
    string popupNoteString;

    [SerializeField]
    PopupNoteType popupNoteType;

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            switch (popupNoteType)
            {
                case PopupNoteType.BottomInfo:
                    PopupNoteManager.instance.BottomInfo.SetActive(true);
                    PopupNoteManager.instance.UpdateBottomInfoText(popupNoteString);
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
                    break;

            }
        }
    }
}