using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueueManager : MonoBehaviour {

    public static QueueManager instance;
    public GameObject[] queueZone;
    public GameObject[] queueCustomer;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        queueCustomer = new GameObject[queueZone.Length];
    }

    public bool isQueueEmpty()
    {
        bool temp = true;
        for (int i = 0; i < queueCustomer.Length; i++)
        {
            if (queueCustomer[i] != null)
            {
                temp = false;
                break;
            }
        }
        return temp;
    }

    public int isThereEmptyQueue()
    {
        for (int i = 0; i < queueCustomer.Length; i++)
        {
            if (queueCustomer[i] == null)
                return i;
        }
        return -1;
    }

    public void queueTransition()
    {
        for(int i = 0; i < queueCustomer.Length - 1; i++)
        {
            queueCustomer[i] = queueCustomer[i + 1];
        }
        queueCustomer[queueCustomer.Length - 1] = null;
    }

    public int getCustomerQueueNumber(GameObject customer)
    {
        for (int i = 0; i < queueCustomer.Length; i++)
        {
            if (queueCustomer[i] == customer)
                return i;
        }
        return -1;
    }
}
