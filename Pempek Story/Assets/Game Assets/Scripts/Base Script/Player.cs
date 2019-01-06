using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;

    [SerializeField]
    bool playerInSellerTableZone;

    public bool inSellerMode;

    public int state;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        playerInSellerTableZone = false;
        inSellerMode = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (playerInSellerTableZone)
            {
                inSellerMode = true;
                UIManager.instance.ShowGameUI(1);
                UIManager.instance.HideGameUI(0);
            }
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (inSellerMode)
            {
                inSellerMode = false;
                UIManager.instance.HideGameUI(1);
                UIManager.instance.ShowGameUI(0);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "SellerTableZone")
        {
            playerInSellerTableZone = true;
            if (!inSellerMode)
                UIManager.instance.ShowGameUI(0);
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "SellerTableZone")
        {
            playerInSellerTableZone = false;
            UIManager.instance.HideGameUI(0);
        }
    }
}
