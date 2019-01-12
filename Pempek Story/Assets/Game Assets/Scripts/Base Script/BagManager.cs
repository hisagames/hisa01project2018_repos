using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BagManager : MonoBehaviour
{
    public static BagManager instance;

    [SerializeField]
    GameObject bagPointer;

    [SerializeField]
    GameObject selectedObject;

    int selectedObjectId;

    void Awake()
    {
        instance = this;
    }

    [SerializeField]
    GameObject BagMenu;
    bool isBagOpen;

    void Start()
    {
        isBagOpen = false;
        changeBagOpenState(false);
        UpdateBagPointer("initiate");
    }

    public void changeBagOpenState(bool state)
    {
        if (isBagOpen != state)
        {
            isBagOpen = state;
            BagMenu.SetActive(isBagOpen);
        }
    }

    public void UpdateBagPointer(string moveTo)
    {
        switch (moveTo)
        {
            case "left":
                selectedObject = selectedObject.GetComponent<ItemToolSetting>().leftNeighbour;
                break;
            case "right":
                selectedObject = selectedObject.GetComponent<ItemToolSetting>().rightNeighbour;
                break;
            case "up":
                selectedObject = selectedObject.GetComponent<ItemToolSetting>().upNeighbour;
                break;
            case "down":
                selectedObject = selectedObject.GetComponent<ItemToolSetting>().downNeighbour;
                break;
        }

        selectedObjectId = selectedObject.GetComponent<ItemToolSetting>().id;
        bagPointer.transform.position =
            new Vector3(selectedObject.transform.position.x, selectedObject.transform.position.y, bagPointer.transform.position.z);
    }

    void Update()
    {

    }
}