using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCEmoticon : MonoBehaviour
{    
    public enum Type
    {
        none,
        thinking,
        interested,
        happy,
        love,
        bored,
        dislike   
    };

    public Sprite[] sprite;
    public Type type;

    public void changeEmoticon(Type type)
    {
        this.type = type;
        if (sprite[(int)type] != null)
        {
            //Debug.Log("Change to emoticon " + type.ToString());
            GetComponent<SpriteRenderer>().enabled = true;
            GetComponent<SpriteRenderer>().sprite = sprite[(int)type];
        }
        else
        {
            GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
