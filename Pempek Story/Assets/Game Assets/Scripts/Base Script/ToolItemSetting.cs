using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ToolItemSetting : MonoBehaviour
{
    [System.Serializable]
    public class ToolData
    {
        public Sprite objectIcon;
        public string objectName;
        public string objectDescription;
        public string type;
    }

    [System.Serializable]
    public class ItemData
    {
        public Sprite objectIcon;
        public string objectName;
        public string objectDescription;
        public string type;
    }

    public ToolData[] toolData;
    public ItemData[] itemData;
    int maxActiveTools = 9;
    int maxActiveItems = 9;
    int maxActiveShelf = 39;

    public GameObject[] activeTools_BagMenu;
    public GameObject[] activeItems_BagMenu;

    public GameObject[] activeTools_ShelfMenu;
    public GameObject[] activeShelfTools_ShelfMenu;

    public Sprite transparentSprite;

    void Start()
    {
        initiateActiveTools();
        initiateActiveItems();
        initiateActiveShelf();
        initiateActiveToolsinShelf();
    }

    public void initiateActiveTools()
    {
        for (int i = 0; i < maxActiveTools; i++)
        {
            //PlayerPrefs.SetInt("activeToolsId" + i, 0);
            int tempId = PlayerPrefs.GetInt("activeToolsId" + i);
            if (tempId >= 0 && tempId < toolData.Length)
            {
                activeTools_BagMenu[i].GetComponent<ItemToolSetting>().id = tempId;
                activeTools_BagMenu[i].GetComponent<ItemToolSetting>().objectIcon.sprite = toolData[tempId].objectIcon;
                activeTools_BagMenu[i].GetComponent<ItemToolSetting>().objectName = toolData[tempId].objectName;
                activeTools_BagMenu[i].GetComponent<ItemToolSetting>().objectDescription = toolData[tempId].objectDescription;
                activeTools_BagMenu[i].GetComponent<ItemToolSetting>().type = toolData[tempId].type;
                activeTools_BagMenu[i].GetComponent<Image>().sprite = toolData[tempId].objectIcon;
                activeTools_BagMenu[i].GetComponent<ItemToolSetting>().totalObject = 1;
            }
            else
            {
                activeTools_BagMenu[i].GetComponent<ItemToolSetting>().id = -1;
                activeTools_BagMenu[i].GetComponent<ItemToolSetting>().objectIcon = null;
                activeTools_BagMenu[i].GetComponent<ItemToolSetting>().objectName = "";
                activeTools_BagMenu[i].GetComponent<ItemToolSetting>().objectDescription = "";
                activeTools_BagMenu[i].GetComponent<ItemToolSetting>().type = "Tools";
                activeTools_BagMenu[i].GetComponent<Image>().sprite = transparentSprite;
                activeTools_BagMenu[i].GetComponent<ItemToolSetting>().totalObject = 0;
            }
        }
    }

    public void initiateActiveItems()
    {
        for (int i = 0; i < maxActiveItems; i++)
        {
            int tempId = PlayerPrefs.GetInt("activeItemsId" + i);
            if (tempId >= 0 && tempId < itemData.Length)
            {
                activeItems_BagMenu[i].GetComponent<ItemToolSetting>().id = tempId;
                activeItems_BagMenu[i].GetComponent<ItemToolSetting>().objectIcon.sprite = itemData[tempId].objectIcon;
                activeItems_BagMenu[i].GetComponent<ItemToolSetting>().objectName = itemData[tempId].objectName;
                activeItems_BagMenu[i].GetComponent<ItemToolSetting>().objectDescription = itemData[tempId].objectDescription;
                activeItems_BagMenu[i].GetComponent<ItemToolSetting>().type = itemData[tempId].type;
                activeItems_BagMenu[i].GetComponent<Image>().sprite = itemData[tempId].objectIcon;
                activeItems_BagMenu[i].GetComponent<ItemToolSetting>().totalObject = 1;
            }
            else
            {
                activeItems_BagMenu[i].GetComponent<ItemToolSetting>().id = -1;
                activeItems_BagMenu[i].GetComponent<ItemToolSetting>().objectIcon = null;
                activeItems_BagMenu[i].GetComponent<ItemToolSetting>().objectName = "";
                activeItems_BagMenu[i].GetComponent<ItemToolSetting>().objectDescription = "";
                activeItems_BagMenu[i].GetComponent<ItemToolSetting>().type = "Items";
                activeItems_BagMenu[i].GetComponent<Image>().sprite = transparentSprite;
                activeItems_BagMenu[i].GetComponent<ItemToolSetting>().totalObject = 0;
            }
        }
    }

    public void initiateActiveToolsinShelf()
    {
        for (int i = 0; i < maxActiveTools; i++)
        {
            int tempId = PlayerPrefs.GetInt("activeToolsId" + i);
            if (tempId >= 0 && tempId < toolData.Length)
            {
                activeTools_ShelfMenu[i].GetComponent<ItemToolSetting>().id = tempId;
                activeTools_ShelfMenu[i].GetComponent<ItemToolSetting>().objectIcon.sprite = toolData[tempId].objectIcon;
                activeTools_ShelfMenu[i].GetComponent<ItemToolSetting>().objectName = toolData[tempId].objectName;
                activeTools_ShelfMenu[i].GetComponent<ItemToolSetting>().objectDescription = toolData[tempId].objectDescription;
                activeTools_ShelfMenu[i].GetComponent<ItemToolSetting>().type = toolData[tempId].type;
                activeTools_ShelfMenu[i].GetComponent<Image>().sprite = toolData[tempId].objectIcon;
                activeTools_ShelfMenu[i].GetComponent<ItemToolSetting>().totalObject = 1;
            }
            else
            {
                activeTools_ShelfMenu[i].GetComponent<ItemToolSetting>().id = -1;
                activeTools_ShelfMenu[i].GetComponent<ItemToolSetting>().objectIcon = null;
                activeTools_ShelfMenu[i].GetComponent<ItemToolSetting>().objectName = "";
                activeTools_ShelfMenu[i].GetComponent<ItemToolSetting>().objectDescription = "";
                activeTools_ShelfMenu[i].GetComponent<ItemToolSetting>().type = "Tools";
                activeTools_ShelfMenu[i].GetComponent<Image>().sprite = transparentSprite;
                activeTools_ShelfMenu[i].GetComponent<ItemToolSetting>().totalObject = 0;
            }
        }
    }

    public void initiateActiveShelf()
    {
        for (int i = 0; i < maxActiveShelf; i++)
        {
            int tempId = PlayerPrefs.GetInt("activeShelfId" + i);
            int tempTotalInId = PlayerPrefs.GetInt("activeShelfTotalInId" + i);

            activeShelfTools_ShelfMenu[i].GetComponent<ItemToolSetting>().propertiesId = i;
            if (tempId >= 0 && tempId < itemData.Length)
            {
                activeShelfTools_ShelfMenu[i].GetComponent<ItemToolSetting>().id = tempId;
                activeShelfTools_ShelfMenu[i].GetComponent<ItemToolSetting>().objectIcon.sprite = toolData[tempId].objectIcon;
                activeShelfTools_ShelfMenu[i].GetComponent<ItemToolSetting>().objectName = toolData[tempId].objectName;
                activeShelfTools_ShelfMenu[i].GetComponent<ItemToolSetting>().objectDescription = toolData[tempId].objectDescription;
                activeShelfTools_ShelfMenu[i].GetComponent<ItemToolSetting>().type = "Shelf";
                activeShelfTools_ShelfMenu[i].GetComponent<Image>().sprite = toolData[tempId].objectIcon;

                activeShelfTools_ShelfMenu[i].GetComponent<ItemToolSetting>().totalObject = tempTotalInId;
                activeShelfTools_ShelfMenu[i].GetComponent<ItemToolSetting>().totalObjectText.GetComponent<TextMeshProUGUI>().text =
                   activeShelfTools_ShelfMenu[i].GetComponent<ItemToolSetting>().totalObject + "";
            }
            else
            {
                activeShelfTools_ShelfMenu[i].GetComponent<ItemToolSetting>().id = -1;
                activeShelfTools_ShelfMenu[i].GetComponent<ItemToolSetting>().objectIcon = null;
                activeShelfTools_ShelfMenu[i].GetComponent<ItemToolSetting>().objectName = "";
                activeShelfTools_ShelfMenu[i].GetComponent<ItemToolSetting>().objectDescription = "";
                activeShelfTools_ShelfMenu[i].GetComponent<ItemToolSetting>().type = "Shelf";
                activeShelfTools_ShelfMenu[i].GetComponent<Image>().sprite = transparentSprite;
                
                activeShelfTools_ShelfMenu[i].GetComponent<ItemToolSetting>().totalObject = 0;
                activeShelfTools_ShelfMenu[i].GetComponent<ItemToolSetting>().totalObjectText.GetComponent<TextMeshProUGUI>().text = "";
            }
        }
    }
}