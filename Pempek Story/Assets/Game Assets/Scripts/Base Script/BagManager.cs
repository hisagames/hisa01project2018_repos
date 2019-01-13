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

    [SerializeField]
    GameObject bagChoosenPointer;

    [SerializeField]
    GameObject choosenObject;

    int choosenObjectId;

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
        choosenObjectId = -1;
    }

    public bool changeBagOpenState(bool state)
    {
        if (isBagOpen != state)
        {
            if (state)
            {
                isBagOpen = state;
                BagMenu.SetActive(isBagOpen);
                return true;
            }
            else
            {
                if(!bagChoosenPointer.activeSelf)
                {
                    isBagOpen = state;
                    BagMenu.SetActive(isBagOpen);
                    return true;
                }
                else
                {
                    UpdateBagChoosenPointer(false);
                }
            }
        }
        return false;
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

    public void UpdateBagChoosenPointer(bool state)
    {
        bagChoosenPointer.SetActive(state);
        if (state)
        {
            choosenObject = selectedObject;
            choosenObjectId = choosenObject.GetComponent<ItemToolSetting>().id;
            bagChoosenPointer.transform.position =
                new Vector3(choosenObject.transform.position.x, choosenObject.transform.position.y, bagChoosenPointer.transform.position.z);
        }
        else
        {
            choosenObject = null;
            choosenObjectId = -1;
        }
    }

    void Update()
    {

    }
}