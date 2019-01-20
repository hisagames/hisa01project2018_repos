using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShelfManager : MonoBehaviour
{
    public static ShelfManager instance;

    [SerializeField]
    GameObject shelfPointer;
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
    GameObject shelfChoosenPointer;
    [SerializeField]
    GameObject choosenObject;
    int choosenObjectId;

    void Awake()
    {
        instance = this;
    }

    [SerializeField]
    GameObject ShelfMenu;
    bool isShelfOpen;

    void Start()
    {
        isShelfOpen = false;
        changeShelfOpenState(false);
        selectedObjectName.text = "";
        selectedObjectDescription.text = "";
        UpdateShelfPointer("initiate");
        choosenObjectId = -1;
    }

    public bool changeShelfOpenState(bool state)
    {
        if (isShelfOpen != state)
        {
            if (state)
            {
                isShelfOpen = state;
                ShelfMenu.SetActive(isShelfOpen);
                return true;
            }
            else
            {
                if (!shelfChoosenPointer.activeSelf)
                {
                    isShelfOpen = state;
                    ShelfMenu.SetActive(isShelfOpen);
                    return true;
                }
                else
                {
                    UpdateShelfChoosenPointer(false);
                }
            }
        }
        return false;
    }

    public void UpdateShelfPointer(string moveTo)
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
        shelfPointer.transform.position =
            new Vector3(selectedObject.transform.position.x, selectedObject.transform.position.y, shelfPointer.transform.position.z);
    }

    public void UpdateShelfChoosenPointer(bool state)
    {
        if ((state && !shelfChoosenPointer.activeSelf) || !state)
        {
            //if (selectedObject.GetComponent<ItemToolSetting>().id >= 0)
            //{
            shelfChoosenPointer.SetActive(state);
            if (state)
            {
                choosenObject = selectedObject;
                choosenObjectId = choosenObject.GetComponent<ItemToolSetting>().id;
                shelfChoosenPointer.transform.position =
                    new Vector3(choosenObject.transform.position.x, choosenObject.transform.position.y, shelfChoosenPointer.transform.position.z);
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
                UpdateShelfChoosenPointer(false);
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

        selectedObject.GetComponent<ItemToolSetting>().id = choosenObject.GetComponent<ItemToolSetting>().id;
        selectedObject.GetComponent<ItemToolSetting>().objectIcon = choosenObject.GetComponent<ItemToolSetting>().objectIcon;
        selectedObject.GetComponent<ItemToolSetting>().objectName = choosenObject.GetComponent<ItemToolSetting>().objectName;
        selectedObject.GetComponent<ItemToolSetting>().objectDescription = choosenObject.GetComponent<ItemToolSetting>().objectDescription;
        selectedObject.GetComponent<Image>().sprite = choosenObject.GetComponent<Image>().sprite;


        choosenObject.GetComponent<ItemToolSetting>().id = tempId;
        choosenObject.GetComponent<ItemToolSetting>().objectIcon = tempObjectIcon;
        choosenObject.GetComponent<ItemToolSetting>().objectName = tempObjectName;
        choosenObject.GetComponent<ItemToolSetting>().objectDescription = tempObjectDescription;
        choosenObject.GetComponent<Image>().sprite = tempSprite;

        PlayerPrefs.SetInt("active" + choosenObject.GetComponent<ItemToolSetting>().type + "Id" + choosenObject.GetComponent<ItemToolSetting>().propertiesId,
            choosenObject.GetComponent<ItemToolSetting>().id);

        PlayerPrefs.SetInt("active" + selectedObject.GetComponent<ItemToolSetting>().type + "Id" + selectedObject.GetComponent<ItemToolSetting>().propertiesId,
            selectedObject.GetComponent<ItemToolSetting>().id);
        PlayerPrefs.Save();

        selectedObjectId = selectedObject.GetComponent<ItemToolSetting>().id;
        selectedObjectName.text = selectedObject.GetComponent<ItemToolSetting>().objectName;
        selectedObjectDescription.text = selectedObject.GetComponent<ItemToolSetting>().objectDescription;
    }

    void Update()
    {

    }
}