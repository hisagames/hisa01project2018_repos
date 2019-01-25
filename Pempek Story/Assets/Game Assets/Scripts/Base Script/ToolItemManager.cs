using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolItemManager : MonoBehaviour
{
    public enum UIState
    {
        BagMenu,
        ShelfMenu
    }

    public static ToolItemManager instance;
    public UIState uiState;

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

    [SerializeField]
    GameObject BoxItemUI; //box sprite yg berada dibawah, show saat bag dan hidden saat shelf dan fridge
    [SerializeField]
    GameObject UIMenu;

    [SerializeField]
    GameObject BagMenu;
    [SerializeField]
    GameObject FirstSelectedObjectInBag;

    [SerializeField]
    GameObject ShelfMenu;
    [SerializeField]
    GameObject FirstSelectedObjectInShelf;

    public ToolItemSetting toolItemSetting;

    bool isUIMenuOpen;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        isUIMenuOpen = false;
        changeToolItemOpenState(false);
        selectedObjectName.text = "";
        selectedObjectDescription.text = "";
        UpdateToolItemPointer("initiate");
        choosenObjectId = -1;
    }

    public bool changeToolItemOpenState(bool state)
    {
        if (isUIMenuOpen != state)
        {
            if (state)
            {
                isUIMenuOpen = state;
                if (uiState == UIState.BagMenu)
                {
                    UIMenu.SetActive(isUIMenuOpen);
                    BoxItemUI.SetActive(isUIMenuOpen);
                    BagMenu.SetActive(isUIMenuOpen);
                    ShelfMenu.SetActive(!isUIMenuOpen);
                    //toolItemSetting.initiateActiveTools();
                    //toolItemSetting.initiateActiveItems();
                    selectedObject = FirstSelectedObjectInBag;
                }
                else if (uiState == UIState.ShelfMenu)
                {
                    UIMenu.SetActive(isUIMenuOpen);
                    BoxItemUI.SetActive(!isUIMenuOpen);
                    BagMenu.SetActive(!isUIMenuOpen);
                    ShelfMenu.SetActive(isUIMenuOpen);
                    //toolItemSetting.initiateActiveToolsinShelf();
                    //toolItemSetting.initiateActiveShelf();
                    selectedObject = FirstSelectedObjectInShelf;
                }

                selectedObjectId = selectedObject.GetComponent<ItemToolSetting>().id;
                selectedObjectName.text = selectedObject.GetComponent<ItemToolSetting>().objectName;
                selectedObjectDescription.text = selectedObject.GetComponent<ItemToolSetting>().objectDescription;
                bagPointer.transform.position =
                    new Vector3(selectedObject.transform.position.x, selectedObject.transform.position.y, bagPointer.transform.position.z);
                return true;
            }
            else
            {
                if(!bagChoosenPointer.activeSelf)
                {
                    isUIMenuOpen = state;
                    if (uiState == UIState.BagMenu)
                    {
                        UIMenu.SetActive(isUIMenuOpen);
                        BoxItemUI.SetActive(isUIMenuOpen);
                        BagMenu.SetActive(isUIMenuOpen);
                        ShelfMenu.SetActive(!isUIMenuOpen);
                        selectedObject = FirstSelectedObjectInShelf;
                    }
                    else if (uiState == UIState.ShelfMenu)
                    {
                        UIMenu.SetActive(isUIMenuOpen);
                        BoxItemUI.SetActive(!isUIMenuOpen);
                        BagMenu.SetActive(!isUIMenuOpen);
                        ShelfMenu.SetActive(isUIMenuOpen);
                        selectedObject = FirstSelectedObjectInShelf;
                    }

                    selectedObjectId = selectedObject.GetComponent<ItemToolSetting>().id;
                    selectedObjectName.text = selectedObject.GetComponent<ItemToolSetting>().objectName;
                    selectedObjectDescription.text = selectedObject.GetComponent<ItemToolSetting>().objectDescription;
                    bagPointer.transform.position =
                        new Vector3(selectedObject.transform.position.x, selectedObject.transform.position.y, bagPointer.transform.position.z);
                    return true;
                }
                else
                {
                    UpdateToolItemChoosenPointer(false);
                }
            }
        }
        return false;
    }

    public void UpdateToolItemPointer(string moveTo)
    {
        //MASIH TERDAPAT 2 TASK YANG PERLU DI FIX KAN
        //-Saat movement pointer di shelf menu masih mengarah pada object item, fix kan.
        //-Menu Shelf menu hanya terbuka saat player menekan tombol z sambil trigger pada shelf menu
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

    public void UpdateToolItemChoosenPointer(bool state)
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
            string tempSelectedString, tempChoosenString;
            tempSelectedString = selectedObject.GetComponent<ItemToolSetting>().type;
            if (tempSelectedString == "Shelf")
                tempSelectedString = "Tools";
            else if (tempSelectedString == "Fridge")
                tempSelectedString = "Items";

            tempChoosenString = choosenObject.GetComponent<ItemToolSetting>().type;
            if (tempChoosenString == "Shelf")
                tempChoosenString = "Tools";
            else if (tempChoosenString == "Fridge")
                tempChoosenString = "Items";

            if (tempSelectedString == tempChoosenString)
            {
                changeObjectPosition();
                UpdateToolItemChoosenPointer(false);
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

        #region Synchronize Bag Menu and Shelf Menu
        if (uiState == UIState.BagMenu)
        {
            if(selectedObject.GetComponent<ItemToolSetting>().type == "Tools")
            {
                toolItemSetting.activeTools_ShelfMenu[selectedObject.GetComponent<ItemToolSetting>().propertiesId].GetComponent<ItemToolSetting>().id
                    = selectedObject.GetComponent<ItemToolSetting>().id;
                toolItemSetting.activeTools_ShelfMenu[selectedObject.GetComponent<ItemToolSetting>().propertiesId].GetComponent<ItemToolSetting>().objectIcon
                    = selectedObject.GetComponent<ItemToolSetting>().objectIcon;
                toolItemSetting.activeTools_ShelfMenu[selectedObject.GetComponent<ItemToolSetting>().propertiesId].GetComponent<ItemToolSetting>().objectName
                    = selectedObject.GetComponent<ItemToolSetting>().objectName;
                toolItemSetting.activeTools_ShelfMenu[selectedObject.GetComponent<ItemToolSetting>().propertiesId].GetComponent<ItemToolSetting>().objectDescription
                    = selectedObject.GetComponent<ItemToolSetting>().objectDescription;
                toolItemSetting.activeTools_ShelfMenu[selectedObject.GetComponent<ItemToolSetting>().propertiesId].GetComponent<Image>().sprite
                    = selectedObject.GetComponent<Image>().sprite;
            }

            if (choosenObject.GetComponent<ItemToolSetting>().type == "Tools")
            {
                toolItemSetting.activeTools_ShelfMenu[choosenObject.GetComponent<ItemToolSetting>().propertiesId].GetComponent<ItemToolSetting>().id
                    = choosenObject.GetComponent<ItemToolSetting>().id;
                toolItemSetting.activeTools_ShelfMenu[choosenObject.GetComponent<ItemToolSetting>().propertiesId].GetComponent<ItemToolSetting>().objectIcon
                    = choosenObject.GetComponent<ItemToolSetting>().objectIcon;
                toolItemSetting.activeTools_ShelfMenu[choosenObject.GetComponent<ItemToolSetting>().propertiesId].GetComponent<ItemToolSetting>().objectName
                    = choosenObject.GetComponent<ItemToolSetting>().objectName;
                toolItemSetting.activeTools_ShelfMenu[choosenObject.GetComponent<ItemToolSetting>().propertiesId].GetComponent<ItemToolSetting>().objectDescription
                    = choosenObject.GetComponent<ItemToolSetting>().objectDescription;
                toolItemSetting.activeTools_ShelfMenu[choosenObject.GetComponent<ItemToolSetting>().propertiesId].GetComponent<Image>().sprite
                    = choosenObject.GetComponent<Image>().sprite;
            }
        }
        else if (uiState == UIState.ShelfMenu)
        {
            if (selectedObject.GetComponent<ItemToolSetting>().type == "Tools")
            {
                toolItemSetting.activeTools_BagMenu[selectedObject.GetComponent<ItemToolSetting>().propertiesId].GetComponent<ItemToolSetting>().id
                    = selectedObject.GetComponent<ItemToolSetting>().id;
                toolItemSetting.activeTools_BagMenu[selectedObject.GetComponent<ItemToolSetting>().propertiesId].GetComponent<ItemToolSetting>().objectIcon
                    = selectedObject.GetComponent<ItemToolSetting>().objectIcon;
                toolItemSetting.activeTools_BagMenu[selectedObject.GetComponent<ItemToolSetting>().propertiesId].GetComponent<ItemToolSetting>().objectName
                    = selectedObject.GetComponent<ItemToolSetting>().objectName;
                toolItemSetting.activeTools_BagMenu[selectedObject.GetComponent<ItemToolSetting>().propertiesId].GetComponent<ItemToolSetting>().objectDescription
                    = selectedObject.GetComponent<ItemToolSetting>().objectDescription;
                toolItemSetting.activeTools_BagMenu[selectedObject.GetComponent<ItemToolSetting>().propertiesId].GetComponent<Image>().sprite
                    = selectedObject.GetComponent<Image>().sprite;
            }

            if (choosenObject.GetComponent<ItemToolSetting>().type == "Tools")
            {
                toolItemSetting.activeTools_BagMenu[choosenObject.GetComponent<ItemToolSetting>().propertiesId].GetComponent<ItemToolSetting>().id
                    = choosenObject.GetComponent<ItemToolSetting>().id;
                toolItemSetting.activeTools_BagMenu[choosenObject.GetComponent<ItemToolSetting>().propertiesId].GetComponent<ItemToolSetting>().objectIcon
                    = choosenObject.GetComponent<ItemToolSetting>().objectIcon;
                toolItemSetting.activeTools_BagMenu[choosenObject.GetComponent<ItemToolSetting>().propertiesId].GetComponent<ItemToolSetting>().objectName
                    = choosenObject.GetComponent<ItemToolSetting>().objectName;
                toolItemSetting.activeTools_BagMenu[choosenObject.GetComponent<ItemToolSetting>().propertiesId].GetComponent<ItemToolSetting>().objectDescription
                    = choosenObject.GetComponent<ItemToolSetting>().objectDescription;
                toolItemSetting.activeTools_BagMenu[choosenObject.GetComponent<ItemToolSetting>().propertiesId].GetComponent<Image>().sprite
                    = choosenObject.GetComponent<Image>().sprite;
            }
        }
        #endregion
    }

    void Update()
    {

    }
}