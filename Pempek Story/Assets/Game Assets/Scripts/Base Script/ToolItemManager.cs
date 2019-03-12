using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ToolItemManager : MonoBehaviour
{
    public enum UIState
    {
        BagMenu,
        ShelfMenu,
        FridgeMenu
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
    GameObject UIMenu;

    [SerializeField]
    GameObject BagMenu;
    [SerializeField]
    GameObject FirstSelectedObjectInBag;

    [SerializeField]
    GameObject ShelfMenu;
    [SerializeField]
    GameObject FirstSelectedObjectInShelf;
    [SerializeField]
    GameObject[] SubShelf;
    int activeSubShelfID;
    [SerializeField]
    Text subShelfActiveIdText;

    [SerializeField]
    GameObject FridgeMenu;
    [SerializeField]
    GameObject FirstSelectedObjectInFridge;
    [SerializeField]
    GameObject[] SubFridge;

    [SerializeField]
    GameObject DescriptionGroupObject;

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
        changeActiveSubShelf(0);
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
                    BagMenu.SetActive(isUIMenuOpen);
                    ShelfMenu.SetActive(!isUIMenuOpen);
                    FridgeMenu.SetActive(!isUIMenuOpen);
                    selectedObject = FirstSelectedObjectInBag;
                    bagPointer.transform.SetParent(BagMenu.transform.GetChild(3).transform);
                    bagChoosenPointer.transform.SetParent(BagMenu.transform.GetChild(3).transform);
                    DescriptionGroupObject.transform.position = BagMenu.transform.GetChild(6).transform.position;
                }
                else if (uiState == UIState.ShelfMenu)
                {
                    UIMenu.SetActive(isUIMenuOpen);
                    BagMenu.SetActive(!isUIMenuOpen);
                    ShelfMenu.SetActive(isUIMenuOpen);
                    FridgeMenu.SetActive(!isUIMenuOpen);
                    selectedObject = FirstSelectedObjectInShelf;
                    bagPointer.transform.SetParent(ShelfMenu.transform.GetChild(3).transform);
                    bagChoosenPointer.transform.SetParent(ShelfMenu.transform.GetChild(3).transform);
                    DescriptionGroupObject.transform.position = ShelfMenu.transform.GetChild(6).transform.position;
                }
                else if (uiState == UIState.FridgeMenu)
                {
                    UIMenu.SetActive(isUIMenuOpen);
                    BagMenu.SetActive(!isUIMenuOpen);
                    ShelfMenu.SetActive(!isUIMenuOpen);
                    FridgeMenu.SetActive(isUIMenuOpen);
                    selectedObject = FirstSelectedObjectInFridge;
                    bagPointer.transform.SetParent(FridgeMenu.transform.GetChild(4).transform);
                    bagChoosenPointer.transform.SetParent(FridgeMenu.transform.GetChild(4).transform);
                    DescriptionGroupObject.transform.position = FridgeMenu.transform.GetChild(7).transform.position;
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
                if (!bagChoosenPointer.activeSelf)
                {
                    isUIMenuOpen = state;
                    if (uiState == UIState.BagMenu)
                    {
                        UIMenu.SetActive(isUIMenuOpen);
                        BagMenu.SetActive(isUIMenuOpen);
                        ShelfMenu.SetActive(!isUIMenuOpen);
                        FridgeMenu.SetActive(!isUIMenuOpen);
                        selectedObject = FirstSelectedObjectInBag;
                    }
                    else if (uiState == UIState.ShelfMenu)
                    {
                        UIMenu.SetActive(isUIMenuOpen);
                        BagMenu.SetActive(!isUIMenuOpen);
                        ShelfMenu.SetActive(isUIMenuOpen);
                        FridgeMenu.SetActive(!isUIMenuOpen);
                        selectedObject = FirstSelectedObjectInShelf;
                    }
                    else if (uiState == UIState.FridgeMenu)
                    {
                        UIMenu.SetActive(isUIMenuOpen);
                        BagMenu.SetActive(!isUIMenuOpen);
                        ShelfMenu.SetActive(!isUIMenuOpen);
                        FridgeMenu.SetActive(isUIMenuOpen);
                        selectedObject = FirstSelectedObjectInFridge;
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
        switch (moveTo)
        {
            case "left":
                if (selectedObject.GetComponent<ItemToolSetting>().leftNeighbour != null)
                    selectedObject = selectedObject.GetComponent<ItemToolSetting>().leftNeighbour;
                else
                {
                    if (uiState == UIState.BagMenu)
                    {
                        if (selectedObject.GetComponent<ItemToolSetting>().type == "Tools")
                            selectedObject = toolItemSetting.activeTools_BagMenu[selectedObject.GetComponent<ItemToolSetting>().propertiesId - 1];
                        if (selectedObject.GetComponent<ItemToolSetting>().type == "Items")
                            selectedObject = toolItemSetting.activeItems_BagMenu[selectedObject.GetComponent<ItemToolSetting>().propertiesId - 1];
                    }
                    else if (uiState == UIState.ShelfMenu)
                    {
                        if (selectedObject.GetComponent<ItemToolSetting>().type == "Tools")
                            selectedObject = toolItemSetting.activeTools_ShelfMenu[selectedObject.GetComponent<ItemToolSetting>().propertiesId - 1];
                        if (selectedObject.GetComponent<ItemToolSetting>().type == "Shelf")
                            selectedObject = toolItemSetting.activeShelfTools_ShelfMenu[selectedObject.GetComponent<ItemToolSetting>().propertiesId - 1];
                    }
                    else if (uiState == UIState.FridgeMenu)
                    {
                        if (selectedObject.GetComponent<ItemToolSetting>().type == "Items")
                            selectedObject = toolItemSetting.activeItems_FridgeMenu[selectedObject.GetComponent<ItemToolSetting>().propertiesId - 1];
                        if (selectedObject.GetComponent<ItemToolSetting>().type == "Fridge")
                            selectedObject = toolItemSetting.activeFridgeItems_FridgeMenu[selectedObject.GetComponent<ItemToolSetting>().propertiesId - 1];
                    }
                }
                break;
            case "right":
                if (selectedObject.GetComponent<ItemToolSetting>().rightNeighbour != null)
                    selectedObject = selectedObject.GetComponent<ItemToolSetting>().rightNeighbour;
                else
                {
                    if (uiState == UIState.BagMenu)
                    {
                        if (selectedObject.GetComponent<ItemToolSetting>().type == "Tools")
                            selectedObject = toolItemSetting.activeTools_BagMenu[selectedObject.GetComponent<ItemToolSetting>().propertiesId + 1];
                        if (selectedObject.GetComponent<ItemToolSetting>().type == "Items")
                            selectedObject = toolItemSetting.activeItems_BagMenu[selectedObject.GetComponent<ItemToolSetting>().propertiesId + 1];
                    }
                    else if (uiState == UIState.ShelfMenu)
                    {
                        if (selectedObject.GetComponent<ItemToolSetting>().type == "Tools")
                            selectedObject = toolItemSetting.activeTools_ShelfMenu[selectedObject.GetComponent<ItemToolSetting>().propertiesId + 1];
                        if (selectedObject.GetComponent<ItemToolSetting>().type == "Shelf")
                            selectedObject = toolItemSetting.activeShelfTools_ShelfMenu[selectedObject.GetComponent<ItemToolSetting>().propertiesId + 1];
                    }
                    else if (uiState == UIState.FridgeMenu)
                    {
                        if (selectedObject.GetComponent<ItemToolSetting>().type == "Items")
                            selectedObject = toolItemSetting.activeItems_FridgeMenu[selectedObject.GetComponent<ItemToolSetting>().propertiesId + 1];
                        if (selectedObject.GetComponent<ItemToolSetting>().type == "Fridge")
                            selectedObject = toolItemSetting.activeFridgeItems_FridgeMenu[selectedObject.GetComponent<ItemToolSetting>().propertiesId + 1];
                    }
                }
                break;
            case "up":
                if (selectedObject.GetComponent<ItemToolSetting>().upNeighbour != null)
                    selectedObject = selectedObject.GetComponent<ItemToolSetting>().upNeighbour;
                else
                {
                    if (uiState == UIState.ShelfMenu)
                    {
                        if (selectedObject.GetComponent<ItemToolSetting>().type == "Shelf")
                            selectedObject = toolItemSetting.activeShelfTools_ShelfMenu[selectedObject.GetComponent<ItemToolSetting>().propertiesId - 4];
                        else if (selectedObject.GetComponent<ItemToolSetting>().type == "Tools")
                        {
                            selectedObject = toolItemSetting.activeShelfTools_ShelfMenu[activeSubShelfID * 8
                                + (selectedObject.GetComponent<ItemToolSetting>().propertiesId + 3)]; //still harcode but code is true
                        }
                    }
                    else if (uiState == UIState.FridgeMenu)
                    {
                        if (selectedObject.GetComponent<ItemToolSetting>().type == "Fridge")
                            selectedObject = toolItemSetting.activeFridgeItems_FridgeMenu[selectedObject.GetComponent<ItemToolSetting>().propertiesId - 4];
                    }
                }
                break;
            case "down":
                if (selectedObject.GetComponent<ItemToolSetting>().downNeighbour != null)
                    selectedObject = selectedObject.GetComponent<ItemToolSetting>().downNeighbour;
                else
                {
                    if (uiState == UIState.ShelfMenu)
                    {
                        if (selectedObject.GetComponent<ItemToolSetting>().type == "Shelf")
                            selectedObject = toolItemSetting.activeShelfTools_ShelfMenu[selectedObject.GetComponent<ItemToolSetting>().propertiesId + 4];
                        else if (selectedObject.GetComponent<ItemToolSetting>().type == "Tools")
                        {

                            selectedObject = toolItemSetting.activeShelfTools_ShelfMenu[activeSubShelfID * 8
                                + (selectedObject.GetComponent<ItemToolSetting>().propertiesId - 5)]; //still harcode but code is true
                        }
                    }
                    else if (uiState == UIState.FridgeMenu)
                    {
                        if (selectedObject.GetComponent<ItemToolSetting>().type == "Fridge")
                            selectedObject = toolItemSetting.activeFridgeItems_FridgeMenu[selectedObject.GetComponent<ItemToolSetting>().propertiesId + 4];
                    }
                }
                break;
        }

        selectedObjectId = selectedObject.GetComponent<ItemToolSetting>().id;
        selectedObjectName.text = selectedObject.GetComponent<ItemToolSetting>().objectName;
        selectedObjectDescription.text = selectedObject.GetComponent<ItemToolSetting>().objectDescription;
        bagPointer.transform.position =
            new Vector3(selectedObject.transform.position.x, selectedObject.transform.position.y, bagPointer.transform.position.z);

        if (uiState == UIState.ShelfMenu)
        {
            if (selectedObject.GetComponent<ItemToolSetting>().type == "Shelf")
            {
                changeActiveSubShelf(selectedObject.GetComponent<ItemToolSetting>().propertiesId / 8);
            }
        }
    }

    public void UpdateToolItemChoosenPointer(bool state)
    {
        if ((state && !bagChoosenPointer.activeSelf) || !state)
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

            bool tempClearSelectedObject = false;
            bool tempClearChoosenObject = false;
            bool tempIsPartialMoveSelectedObject = false;
            bool tempIsPartialMoveChoosenObject = false;

            if (tempSelectedString == tempChoosenString)
            {
                #region update tools-shelf, and vice versa
                if (selectedObject.GetComponent<ItemToolSetting>().type == "Tools")
                {
                    if (choosenObject.GetComponent<ItemToolSetting>().type == "Tools")
                    {
                        //nothing, just exchange it
                    }
                    else if (choosenObject.GetComponent<ItemToolSetting>().type == "Shelf")
                    {
                        if (selectedObject.GetComponent<ItemToolSetting>().id != -1)
                        {
                            if (choosenObject.GetComponent<ItemToolSetting>().id != -1)
                            {
                                if (choosenObject.GetComponent<ItemToolSetting>().totalObject > 1)
                                {
                                    tempIsPartialMoveSelectedObject = true;
                                    tempIsPartialMoveChoosenObject = true;
                                }
                                else
                                {
                                    //nothing, just exchange it
                                }
                            }
                            else
                            {
                                choosenObject.GetComponent<ItemToolSetting>().totalObject += 1;
                                selectedObject.GetComponent<ItemToolSetting>().totalObject = 0;
                            }
                        }
                        else
                        {
                            if (choosenObject.GetComponent<ItemToolSetting>().id != -1)
                            {
                                if (choosenObject.GetComponent<ItemToolSetting>().totalObject > 1)
                                {
                                    choosenObject.GetComponent<ItemToolSetting>().totalObject -= 1;
                                    tempIsPartialMoveSelectedObject = true;
                                }
                                else if (choosenObject.GetComponent<ItemToolSetting>().totalObject == 1)
                                {
                                    choosenObject.GetComponent<ItemToolSetting>().totalObject = 0;
                                }
                                else if (choosenObject.GetComponent<ItemToolSetting>().totalObject == 0)
                                {
                                    //nothing, just exchange it
                                }
                            }
                        }
                    }
                }

                else if (selectedObject.GetComponent<ItemToolSetting>().type == "Shelf")
                {
                    if (choosenObject.GetComponent<ItemToolSetting>().type == "Tools")
                    {
                        if (selectedObject.GetComponent<ItemToolSetting>().id != -1)
                        {
                            if (choosenObject.GetComponent<ItemToolSetting>().id != -1)
                            {
                                if (selectedObject.GetComponent<ItemToolSetting>().id == choosenObject.GetComponent<ItemToolSetting>().id)
                                {
                                    selectedObject.GetComponent<ItemToolSetting>().totalObject += 1;
                                    choosenObject.GetComponent<ItemToolSetting>().totalObject = 0;
                                    tempClearChoosenObject = true;
                                }
                                else
                                {
                                    if (selectedObject.GetComponent<ItemToolSetting>().totalObject > 1)
                                    {
                                        tempIsPartialMoveSelectedObject = true;
                                        tempIsPartialMoveChoosenObject = true;
                                    }
                                    else
                                    {
                                        //nothing, just exchange it
                                    }
                                }
                            }
                            else
                            {
                                if (selectedObject.GetComponent<ItemToolSetting>().totalObject > 1)
                                {
                                    selectedObject.GetComponent<ItemToolSetting>().totalObject -= 1;
                                    tempIsPartialMoveChoosenObject = true;
                                }
                                else if (selectedObject.GetComponent<ItemToolSetting>().totalObject == 1)
                                {
                                    selectedObject.GetComponent<ItemToolSetting>().totalObject = 0;
                                }
                                else if (selectedObject.GetComponent<ItemToolSetting>().totalObject == 0)
                                {
                                    //nothing, just exchange it
                                }
                            }
                        }
                        else
                        {
                            if (choosenObject.GetComponent<ItemToolSetting>().id != -1)
                            {
                                selectedObject.GetComponent<ItemToolSetting>().totalObject += 1;
                                choosenObject.GetComponent<ItemToolSetting>().totalObject = 0;
                                tempClearChoosenObject = true;
                            }
                            else
                            {
                                //nothing, just exchange it
                            }
                        }
                    }
                    else if (choosenObject.GetComponent<ItemToolSetting>().type == "Shelf")
                    {
                        if (choosenObject.GetComponent<ItemToolSetting>().id != -1)
                        {
                            if (selectedObject.GetComponent<ItemToolSetting>().id != -1)
                            {
                                if (choosenObject.GetComponent<ItemToolSetting>().id == selectedObject.GetComponent<ItemToolSetting>().id)
                                {
                                    if (choosenObject.GetComponent<ItemToolSetting>().propertiesId
                                        != selectedObject.GetComponent<ItemToolSetting>().propertiesId)
                                    {
                                        selectedObject.GetComponent<ItemToolSetting>().totalObject
                                            += choosenObject.GetComponent<ItemToolSetting>().totalObject;
                                        choosenObject.GetComponent<ItemToolSetting>().totalObject = 0;
                                        tempClearChoosenObject = true;
                                    }
                                }
                                else
                                {
                                    int tempTotalObject = selectedObject.GetComponent<ItemToolSetting>().totalObject;
                                    selectedObject.GetComponent<ItemToolSetting>().totalObject = choosenObject.GetComponent<ItemToolSetting>().totalObject;
                                    choosenObject.GetComponent<ItemToolSetting>().totalObject = tempTotalObject;
                                }
                            }
                            else
                            {
                                selectedObject.GetComponent<ItemToolSetting>().totalObject = choosenObject.GetComponent<ItemToolSetting>().totalObject;
                                choosenObject.GetComponent<ItemToolSetting>().totalObject = 0;
                                tempClearChoosenObject = true;
                            }
                        }
                        else
                        {
                            if (selectedObject.GetComponent<ItemToolSetting>().id != -1)
                            {
                                choosenObject.GetComponent<ItemToolSetting>().totalObject = selectedObject.GetComponent<ItemToolSetting>().totalObject;
                                selectedObject.GetComponent<ItemToolSetting>().totalObject = 0;
                                tempClearSelectedObject = true;
                            }
                            else
                            {
                                //nothing, just exchange it
                            }
                        }
                    }
                }

                if (selectedObject.GetComponent<ItemToolSetting>().type == "Shelf")
                {
                    if (selectedObject.GetComponent<ItemToolSetting>().totalObject > 0)
                        selectedObject.GetComponent<ItemToolSetting>().totalObjectText.GetComponent<TextMeshProUGUI>().text =
                                selectedObject.GetComponent<ItemToolSetting>().totalObject + "";
                    else
                        selectedObject.GetComponent<ItemToolSetting>().totalObjectText.GetComponent<TextMeshProUGUI>().text = "";
                }

                if (choosenObject.GetComponent<ItemToolSetting>().type == "Shelf")
                {
                    if (choosenObject.GetComponent<ItemToolSetting>().totalObject > 0)
                        choosenObject.GetComponent<ItemToolSetting>().totalObjectText.GetComponent<TextMeshProUGUI>().text =
                                choosenObject.GetComponent<ItemToolSetting>().totalObject + "";
                    else
                        choosenObject.GetComponent<ItemToolSetting>().totalObjectText.GetComponent<TextMeshProUGUI>().text = "";
                }
                #endregion

                #region update items-fridge, and vice versa
                if (selectedObject.GetComponent<ItemToolSetting>().type == "Items")
                {
                    if (choosenObject.GetComponent<ItemToolSetting>().type == "Items")
                    {
                        //nothing, just exchange it
                    }
                    else if (choosenObject.GetComponent<ItemToolSetting>().type == "Fridge")
                    {
                        if (selectedObject.GetComponent<ItemToolSetting>().id != -1)
                        {
                            if (choosenObject.GetComponent<ItemToolSetting>().id != -1)
                            {
                                if (choosenObject.GetComponent<ItemToolSetting>().totalObject > 1)
                                {
                                    tempIsPartialMoveSelectedObject = true;
                                    tempIsPartialMoveChoosenObject = true;
                                }
                                else
                                {
                                    //nothing, just exchange it
                                }
                            }
                            else
                            {
                                choosenObject.GetComponent<ItemToolSetting>().totalObject += 1;
                                selectedObject.GetComponent<ItemToolSetting>().totalObject = 0;
                            }
                        }
                        else
                        {
                            if (choosenObject.GetComponent<ItemToolSetting>().id != -1)
                            {
                                if (choosenObject.GetComponent<ItemToolSetting>().totalObject > 1)
                                {
                                    choosenObject.GetComponent<ItemToolSetting>().totalObject -= 1;
                                    tempIsPartialMoveSelectedObject = true;
                                }
                                else if (choosenObject.GetComponent<ItemToolSetting>().totalObject == 1)
                                {
                                    choosenObject.GetComponent<ItemToolSetting>().totalObject = 0;
                                }
                                else if (choosenObject.GetComponent<ItemToolSetting>().totalObject == 0)
                                {
                                    //nothing, just exchange it
                                }
                            }
                        }
                    }
                }

                else if (selectedObject.GetComponent<ItemToolSetting>().type == "Fridge")
                {
                    if (choosenObject.GetComponent<ItemToolSetting>().type == "Items")
                    {
                        if (selectedObject.GetComponent<ItemToolSetting>().id != -1)
                        {
                            if (choosenObject.GetComponent<ItemToolSetting>().id != -1)
                            {
                                if (selectedObject.GetComponent<ItemToolSetting>().id == choosenObject.GetComponent<ItemToolSetting>().id)
                                {
                                    selectedObject.GetComponent<ItemToolSetting>().totalObject += 1;
                                    choosenObject.GetComponent<ItemToolSetting>().totalObject = 0;
                                    tempClearChoosenObject = true;
                                }
                                else
                                {
                                    if (selectedObject.GetComponent<ItemToolSetting>().totalObject > 1)
                                    {
                                        tempIsPartialMoveSelectedObject = true;
                                        tempIsPartialMoveChoosenObject = true;
                                    }
                                    else
                                    {
                                        //nothing, just exchange it
                                    }
                                }
                            }
                            else
                            {
                                if (selectedObject.GetComponent<ItemToolSetting>().totalObject > 1)
                                {
                                    selectedObject.GetComponent<ItemToolSetting>().totalObject -= 1;
                                    tempIsPartialMoveChoosenObject = true;
                                }
                                else if (selectedObject.GetComponent<ItemToolSetting>().totalObject == 1)
                                {
                                    selectedObject.GetComponent<ItemToolSetting>().totalObject = 0;
                                }
                                else if (selectedObject.GetComponent<ItemToolSetting>().totalObject == 0)
                                {
                                    //nothing, just exchange it
                                }
                            }
                        }
                        else
                        {
                            if (choosenObject.GetComponent<ItemToolSetting>().id != -1)
                            {
                                selectedObject.GetComponent<ItemToolSetting>().totalObject += 1;
                                choosenObject.GetComponent<ItemToolSetting>().totalObject = 0;
                                tempClearChoosenObject = true;
                            }
                            else
                            {
                                //nothing, just exchange it
                            }
                        }
                    }
                    else if (choosenObject.GetComponent<ItemToolSetting>().type == "Fridge")
                    {
                        if (choosenObject.GetComponent<ItemToolSetting>().id != -1)
                        {
                            if (selectedObject.GetComponent<ItemToolSetting>().id != -1)
                            {
                                if (choosenObject.GetComponent<ItemToolSetting>().id == selectedObject.GetComponent<ItemToolSetting>().id)
                                {
                                    if (choosenObject.GetComponent<ItemToolSetting>().propertiesId
                                        != selectedObject.GetComponent<ItemToolSetting>().propertiesId)
                                    {
                                        selectedObject.GetComponent<ItemToolSetting>().totalObject
                                            += choosenObject.GetComponent<ItemToolSetting>().totalObject;
                                        choosenObject.GetComponent<ItemToolSetting>().totalObject = 0;
                                        tempClearChoosenObject = true;
                                    }
                                }
                                else
                                {
                                    int tempTotalObject = selectedObject.GetComponent<ItemToolSetting>().totalObject;
                                    selectedObject.GetComponent<ItemToolSetting>().totalObject = choosenObject.GetComponent<ItemToolSetting>().totalObject;
                                    choosenObject.GetComponent<ItemToolSetting>().totalObject = tempTotalObject;
                                }
                            }
                            else
                            {
                                selectedObject.GetComponent<ItemToolSetting>().totalObject = choosenObject.GetComponent<ItemToolSetting>().totalObject;
                                choosenObject.GetComponent<ItemToolSetting>().totalObject = 0;
                                tempClearChoosenObject = true;
                            }
                        }
                        else
                        {
                            if (selectedObject.GetComponent<ItemToolSetting>().id != -1)
                            {
                                choosenObject.GetComponent<ItemToolSetting>().totalObject = selectedObject.GetComponent<ItemToolSetting>().totalObject;
                                selectedObject.GetComponent<ItemToolSetting>().totalObject = 0;
                                tempClearSelectedObject = true;
                            }
                            else
                            {
                                //nothing, just exchange it
                            }
                        }
                    }
                }

                if (selectedObject.GetComponent<ItemToolSetting>().type == "Fridge")
                {
                    if (selectedObject.GetComponent<ItemToolSetting>().totalObject > 0)
                        selectedObject.GetComponent<ItemToolSetting>().totalObjectText.GetComponent<TextMeshProUGUI>().text =
                                selectedObject.GetComponent<ItemToolSetting>().totalObject + "";
                    else
                        selectedObject.GetComponent<ItemToolSetting>().totalObjectText.GetComponent<TextMeshProUGUI>().text = "";
                }

                if (choosenObject.GetComponent<ItemToolSetting>().type == "Fridge")
                {
                    if (choosenObject.GetComponent<ItemToolSetting>().totalObject > 0)
                        choosenObject.GetComponent<ItemToolSetting>().totalObjectText.GetComponent<TextMeshProUGUI>().text =
                                choosenObject.GetComponent<ItemToolSetting>().totalObject + "";
                    else
                        choosenObject.GetComponent<ItemToolSetting>().totalObjectText.GetComponent<TextMeshProUGUI>().text = "";
                }
                #endregion

                changeObjectPosition(tempClearSelectedObject, tempClearChoosenObject, tempIsPartialMoveSelectedObject, tempIsPartialMoveChoosenObject);
                UpdateToolItemChoosenPointer(false);
            }
        }
    }

    void clearObjectSlot(GameObject temp)
    {
        temp.GetComponent<ItemToolSetting>().id = -1;
        temp.GetComponent<ItemToolSetting>().objectIcon = null;
        temp.GetComponent<ItemToolSetting>().objectName = "";
        temp.GetComponent<ItemToolSetting>().objectDescription = "";
        temp.GetComponent<Image>().sprite = toolItemSetting.transparentSprite;

        temp.GetComponent<ItemToolSetting>().totalObject = 0;
        PlayerPrefs.SetInt("active" + temp.GetComponent<ItemToolSetting>().type + "Id" + temp.GetComponent<ItemToolSetting>().propertiesId,
            temp.GetComponent<ItemToolSetting>().id);
    }

    void changeObjectPosition(bool tempClearSelectedObject, bool tempClearChoosenObject, bool tempIsPartialMoveSelectedObject, bool tempIsPartialMoveChoosenObject)
    {
        int tempId = selectedObject.GetComponent<ItemToolSetting>().id;
        Image tempObjectIcon = selectedObject.GetComponent<ItemToolSetting>().objectIcon;
        string tempObjectName = selectedObject.GetComponent<ItemToolSetting>().objectName;
        string tempObjectDescription = selectedObject.GetComponent<ItemToolSetting>().objectDescription;
        //int tempTotalObject = selectedObject.GetComponent<ItemToolSetting>().totalObject;
        Sprite tempSprite = selectedObject.GetComponent<Image>().sprite;

        if (!tempIsPartialMoveChoosenObject)
        {
            selectedObject.GetComponent<ItemToolSetting>().id = choosenObject.GetComponent<ItemToolSetting>().id;
            selectedObject.GetComponent<ItemToolSetting>().objectIcon = choosenObject.GetComponent<ItemToolSetting>().objectIcon;
            selectedObject.GetComponent<ItemToolSetting>().objectName = choosenObject.GetComponent<ItemToolSetting>().objectName;
            selectedObject.GetComponent<ItemToolSetting>().objectDescription = choosenObject.GetComponent<ItemToolSetting>().objectDescription;
            //selectedObject.GetComponent<ItemToolSetting>().totalObject = choosenObject.GetComponent<ItemToolSetting>().totalObject;
            selectedObject.GetComponent<Image>().sprite = choosenObject.GetComponent<Image>().sprite;
        }

        if (!tempIsPartialMoveSelectedObject)
        {
            choosenObject.GetComponent<ItemToolSetting>().id = tempId;
            choosenObject.GetComponent<ItemToolSetting>().objectIcon = tempObjectIcon;
            choosenObject.GetComponent<ItemToolSetting>().objectName = tempObjectName;
            choosenObject.GetComponent<ItemToolSetting>().objectDescription = tempObjectDescription;
            //choosenObject.GetComponent<ItemToolSetting>().totalObject = tempTotalObject;
            choosenObject.GetComponent<Image>().sprite = tempSprite;
        }

        if (tempClearSelectedObject)
            clearObjectSlot(selectedObject);
        if (tempClearChoosenObject)
            clearObjectSlot(choosenObject);

        PlayerPrefs.SetInt("active" + choosenObject.GetComponent<ItemToolSetting>().type + "Id" + choosenObject.GetComponent<ItemToolSetting>().propertiesId,
            choosenObject.GetComponent<ItemToolSetting>().id);
        PlayerPrefs.SetInt("active" + choosenObject.GetComponent<ItemToolSetting>().type + "TotalInId" + choosenObject.GetComponent<ItemToolSetting>().propertiesId,
            choosenObject.GetComponent<ItemToolSetting>().totalObject);

        PlayerPrefs.SetInt("active" + selectedObject.GetComponent<ItemToolSetting>().type + "Id" + selectedObject.GetComponent<ItemToolSetting>().propertiesId,
            selectedObject.GetComponent<ItemToolSetting>().id);
        PlayerPrefs.SetInt("active" + selectedObject.GetComponent<ItemToolSetting>().type + "TotalInId" + selectedObject.GetComponent<ItemToolSetting>().propertiesId,
            selectedObject.GetComponent<ItemToolSetting>().totalObject);
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
                toolItemSetting.activeTools_ShelfMenu[selectedObject.GetComponent<ItemToolSetting>().propertiesId].GetComponent<ItemToolSetting>().totalObject
                    = selectedObject.GetComponent<ItemToolSetting>().totalObject;
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
                toolItemSetting.activeTools_ShelfMenu[choosenObject.GetComponent<ItemToolSetting>().propertiesId].GetComponent<ItemToolSetting>().totalObject
                    = choosenObject.GetComponent<ItemToolSetting>().totalObject;
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
                toolItemSetting.activeTools_BagMenu[selectedObject.GetComponent<ItemToolSetting>().propertiesId].GetComponent<ItemToolSetting>().totalObject
                    = selectedObject.GetComponent<ItemToolSetting>().totalObject;
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
                toolItemSetting.activeTools_BagMenu[choosenObject.GetComponent<ItemToolSetting>().propertiesId].GetComponent<ItemToolSetting>().totalObject
                    = choosenObject.GetComponent<ItemToolSetting>().totalObject;
            }
        }
        #endregion

        #region Synchronize Bag Menu and Fridge Menu
        if (uiState == UIState.BagMenu)
        {
            if (selectedObject.GetComponent<ItemToolSetting>().type == "Items")
            {
                toolItemSetting.activeItems_FridgeMenu[selectedObject.GetComponent<ItemToolSetting>().propertiesId].GetComponent<ItemToolSetting>().id
                    = selectedObject.GetComponent<ItemToolSetting>().id;
                toolItemSetting.activeItems_FridgeMenu[selectedObject.GetComponent<ItemToolSetting>().propertiesId].GetComponent<ItemToolSetting>().objectIcon
                    = selectedObject.GetComponent<ItemToolSetting>().objectIcon;
                toolItemSetting.activeItems_FridgeMenu[selectedObject.GetComponent<ItemToolSetting>().propertiesId].GetComponent<ItemToolSetting>().objectName
                    = selectedObject.GetComponent<ItemToolSetting>().objectName;
                toolItemSetting.activeItems_FridgeMenu[selectedObject.GetComponent<ItemToolSetting>().propertiesId].GetComponent<ItemToolSetting>().objectDescription
                    = selectedObject.GetComponent<ItemToolSetting>().objectDescription;
                toolItemSetting.activeItems_FridgeMenu[selectedObject.GetComponent<ItemToolSetting>().propertiesId].GetComponent<Image>().sprite
                    = selectedObject.GetComponent<Image>().sprite;
                toolItemSetting.activeItems_FridgeMenu[selectedObject.GetComponent<ItemToolSetting>().propertiesId].GetComponent<ItemToolSetting>().totalObject
                    = selectedObject.GetComponent<ItemToolSetting>().totalObject;
            }

            if (choosenObject.GetComponent<ItemToolSetting>().type == "Items")
            {
                toolItemSetting.activeItems_FridgeMenu[choosenObject.GetComponent<ItemToolSetting>().propertiesId].GetComponent<ItemToolSetting>().id
                    = choosenObject.GetComponent<ItemToolSetting>().id;
                toolItemSetting.activeItems_FridgeMenu[choosenObject.GetComponent<ItemToolSetting>().propertiesId].GetComponent<ItemToolSetting>().objectIcon
                    = choosenObject.GetComponent<ItemToolSetting>().objectIcon;
                toolItemSetting.activeItems_FridgeMenu[choosenObject.GetComponent<ItemToolSetting>().propertiesId].GetComponent<ItemToolSetting>().objectName
                    = choosenObject.GetComponent<ItemToolSetting>().objectName;
                toolItemSetting.activeItems_FridgeMenu[choosenObject.GetComponent<ItemToolSetting>().propertiesId].GetComponent<ItemToolSetting>().objectDescription
                    = choosenObject.GetComponent<ItemToolSetting>().objectDescription;
                toolItemSetting.activeItems_FridgeMenu[choosenObject.GetComponent<ItemToolSetting>().propertiesId].GetComponent<Image>().sprite
                    = choosenObject.GetComponent<Image>().sprite;
                toolItemSetting.activeItems_FridgeMenu[choosenObject.GetComponent<ItemToolSetting>().propertiesId].GetComponent<ItemToolSetting>().totalObject
                    = choosenObject.GetComponent<ItemToolSetting>().totalObject;
            }
        }
        else if (uiState == UIState.FridgeMenu)
        {
            if (selectedObject.GetComponent<ItemToolSetting>().type == "Items")
            {
                toolItemSetting.activeItems_BagMenu[selectedObject.GetComponent<ItemToolSetting>().propertiesId].GetComponent<ItemToolSetting>().id
                    = selectedObject.GetComponent<ItemToolSetting>().id;
                toolItemSetting.activeItems_BagMenu[selectedObject.GetComponent<ItemToolSetting>().propertiesId].GetComponent<ItemToolSetting>().objectIcon
                    = selectedObject.GetComponent<ItemToolSetting>().objectIcon;
                toolItemSetting.activeItems_BagMenu[selectedObject.GetComponent<ItemToolSetting>().propertiesId].GetComponent<ItemToolSetting>().objectName
                    = selectedObject.GetComponent<ItemToolSetting>().objectName;
                toolItemSetting.activeItems_BagMenu[selectedObject.GetComponent<ItemToolSetting>().propertiesId].GetComponent<ItemToolSetting>().objectDescription
                    = selectedObject.GetComponent<ItemToolSetting>().objectDescription;
                toolItemSetting.activeItems_BagMenu[selectedObject.GetComponent<ItemToolSetting>().propertiesId].GetComponent<Image>().sprite
                    = selectedObject.GetComponent<Image>().sprite;
                toolItemSetting.activeItems_BagMenu[selectedObject.GetComponent<ItemToolSetting>().propertiesId].GetComponent<ItemToolSetting>().totalObject
                    = selectedObject.GetComponent<ItemToolSetting>().totalObject;
            }

            if (choosenObject.GetComponent<ItemToolSetting>().type == "Items")
            {
                toolItemSetting.activeItems_BagMenu[choosenObject.GetComponent<ItemToolSetting>().propertiesId].GetComponent<ItemToolSetting>().id
                    = choosenObject.GetComponent<ItemToolSetting>().id;
                toolItemSetting.activeItems_BagMenu[choosenObject.GetComponent<ItemToolSetting>().propertiesId].GetComponent<ItemToolSetting>().objectIcon
                    = choosenObject.GetComponent<ItemToolSetting>().objectIcon;
                toolItemSetting.activeItems_BagMenu[choosenObject.GetComponent<ItemToolSetting>().propertiesId].GetComponent<ItemToolSetting>().objectName
                    = choosenObject.GetComponent<ItemToolSetting>().objectName;
                toolItemSetting.activeItems_BagMenu[choosenObject.GetComponent<ItemToolSetting>().propertiesId].GetComponent<ItemToolSetting>().objectDescription
                    = choosenObject.GetComponent<ItemToolSetting>().objectDescription;
                toolItemSetting.activeItems_BagMenu[choosenObject.GetComponent<ItemToolSetting>().propertiesId].GetComponent<Image>().sprite
                    = choosenObject.GetComponent<Image>().sprite;
                toolItemSetting.activeItems_BagMenu[choosenObject.GetComponent<ItemToolSetting>().propertiesId].GetComponent<ItemToolSetting>().totalObject
                    = choosenObject.GetComponent<ItemToolSetting>().totalObject;
            }
        }
        #endregion
    }

    void changeActiveSubShelf(int newActiveId)
    {
        for (int i = 0; i < SubShelf.Length; i++)
        {
            SubShelf[i].gameObject.SetActive(false);
        }

        activeSubShelfID = newActiveId;
        subShelfActiveIdText.text = (activeSubShelfID + 1) + " / " + SubShelf.Length;
        SubShelf[activeSubShelfID].gameObject.SetActive(true);
    }

    void Update()
    {

    }
}