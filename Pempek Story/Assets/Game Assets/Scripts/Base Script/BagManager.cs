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
    Sprite selectedIconObject;
    [SerializeField]
    Text selectedObjectName;
    [SerializeField]
    Text selectedObjectDescription;

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
        selectedObjectName.text = "";
        selectedObjectDescription.text = "";
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
        selectedObjectName.text = selectedObject.GetComponent<ItemToolSetting>().objectName;
        selectedObjectDescription.text = selectedObject.GetComponent<ItemToolSetting>().objectDescription;
        bagPointer.transform.position =
            new Vector3(selectedObject.transform.position.x, selectedObject.transform.position.y, bagPointer.transform.position.z);
    }

    public void UpdateBagChoosenPointer(bool state)
    {
        if ((state && !bagChoosenPointer.activeSelf) || !state)
        {
            //if (selectedObject.GetComponent<ItemToolSetting>().id >= 0)
            //{
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
            //}
        }
        else
        {
            if (selectedObject.GetComponent<ItemToolSetting>().type == choosenObject.GetComponent<ItemToolSetting>().type)
            {
                changeObjectPosition();
                UpdateBagChoosenPointer(false);
            }
        }
    }

    void changeObjectPosition()
    {
        int tempId = selectedObject.GetComponent<ItemToolSetting>().id;
        Image tempObjectIcon = selectedObject.GetComponent<ItemToolSetting>().objectIcon;
        string tempObjectName = selectedObject.GetComponent<ItemToolSetting>().objectName;
        string tempObjectDescription = selectedObject.GetComponent<ItemToolSetting>().objectDescription;
        Sprite tempSprite = selectedObject.GetComponent<Image>().sprite;

        PlayerPrefs.SetInt("active" + selectedObject.GetComponent<ItemToolSetting>().type + "Id" + selectedObject.GetComponent<ItemToolSetting>().propertiesId,
            choosenObject.GetComponent<ItemToolSetting>().id);
        selectedObject.GetComponent<ItemToolSetting>().id = choosenObject.GetComponent<ItemToolSetting>().id;
        selectedObject.GetComponent<ItemToolSetting>().objectIcon = choosenObject.GetComponent<ItemToolSetting>().objectIcon;
        selectedObject.GetComponent<ItemToolSetting>().objectName = choosenObject.GetComponent<ItemToolSetting>().objectName;
        selectedObject.GetComponent<ItemToolSetting>().objectDescription = choosenObject.GetComponent<ItemToolSetting>().objectDescription;
        selectedObject.GetComponent<Image>().sprite = choosenObject.GetComponent<Image>().sprite;

        PlayerPrefs.SetInt("active" + selectedObject.GetComponent<ItemToolSetting>().type + "Id" + selectedObject.GetComponent<ItemToolSetting>().propertiesId,
            tempId);
        choosenObject.GetComponent<ItemToolSetting>().id = tempId;
        choosenObject.GetComponent<ItemToolSetting>().objectIcon = tempObjectIcon;
        choosenObject.GetComponent<ItemToolSetting>().objectName = tempObjectName;
        choosenObject.GetComponent<ItemToolSetting>().objectDescription = tempObjectDescription;
        choosenObject.GetComponent<Image>().sprite = tempSprite;

        selectedObjectId = selectedObject.GetComponent<ItemToolSetting>().id;
        selectedObjectName.text = selectedObject.GetComponent<ItemToolSetting>().objectName;
        selectedObjectDescription.text = selectedObject.GetComponent<ItemToolSetting>().objectDescription;
    }

    void Update()
    {

    }
}