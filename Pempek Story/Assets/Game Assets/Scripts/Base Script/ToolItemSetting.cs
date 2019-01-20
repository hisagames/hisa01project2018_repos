using System.Collections;
using System.Collections.Generic;
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
    int maxActiveShelf = 8;

    public GameObject[] activeTools;
    public GameObject[] activeItems;
    public GameObject[] activeShelfTools;

    [SerializeField]
    Sprite transparentSprite;

    void Start()
    {
        initiateActiveTools();
        initiateActiveItems();
        initiateActiveShelf();
    }

    void initiateActiveTools()
    {
        for (int i = 0; i < maxActiveTools; i++)
        {
            int tempId = PlayerPrefs.GetInt("activeToolsId" + i);
            if (tempId >= 0 && tempId < toolData.Length)
            {
                activeTools[i].GetComponent<ItemToolSetting>().id = tempId;
                activeTools[i].GetComponent<ItemToolSetting>().objectIcon.sprite = toolData[tempId].objectIcon;
                activeTools[i].GetComponent<ItemToolSetting>().objectName = toolData[tempId].objectName;
                activeTools[i].GetComponent<ItemToolSetting>().objectDescription = toolData[tempId].objectDescription;
                activeTools[i].GetComponent<ItemToolSetting>().type = toolData[tempId].type;
                activeTools[i].GetComponent<Image>().sprite = toolData[tempId].objectIcon;
            }
            else
            {
                activeTools[i].GetComponent<ItemToolSetting>().id = -1;
                activeTools[i].GetComponent<ItemToolSetting>().objectIcon = null;
                activeTools[i].GetComponent<ItemToolSetting>().objectName = "";
                activeTools[i].GetComponent<ItemToolSetting>().objectDescription = "";
                activeTools[i].GetComponent<ItemToolSetting>().type = "Tools";
                activeTools[i].GetComponent<Image>().sprite = transparentSprite;
            }
        }
    }

    void initiateActiveItems()
    {
        for (int i = 0; i < maxActiveItems; i++)
        {
            int tempId = PlayerPrefs.GetInt("activeItemsId" + i);
            if (tempId >= 0 && tempId < itemData.Length)
            {
                activeItems[i].GetComponent<ItemToolSetting>().id = tempId;
                activeItems[i].GetComponent<ItemToolSetting>().objectIcon.sprite = itemData[tempId].objectIcon;
                activeItems[i].GetComponent<ItemToolSetting>().objectName = itemData[tempId].objectName;
                activeItems[i].GetComponent<ItemToolSetting>().objectDescription = itemData[tempId].objectDescription;
                activeItems[i].GetComponent<ItemToolSetting>().type = itemData[tempId].type;
                activeItems[i].GetComponent<Image>().sprite = itemData[tempId].objectIcon;
            }
            else
            {
                activeItems[i].GetComponent<ItemToolSetting>().id = -1;
                activeItems[i].GetComponent<ItemToolSetting>().objectIcon = null;
                activeItems[i].GetComponent<ItemToolSetting>().objectName = "";
                activeItems[i].GetComponent<ItemToolSetting>().objectDescription = "";
                activeItems[i].GetComponent<ItemToolSetting>().type = "Items";
                activeItems[i].GetComponent<Image>().sprite = transparentSprite;
            }
        }
    }

    void initiateActiveShelf()
    {
        for (int i = 0; i < maxActiveShelf; i++)
        {
            int tempId = PlayerPrefs.GetInt("activeShelfId" + i);

            activeShelfTools[i].GetComponent<ItemToolSetting>().propertiesId = i;
            if (tempId >= 0 && tempId < itemData.Length)
            {
                activeShelfTools[i].GetComponent<ItemToolSetting>().id = tempId;
                activeShelfTools[i].GetComponent<ItemToolSetting>().objectIcon.sprite = itemData[tempId].objectIcon;
                activeShelfTools[i].GetComponent<ItemToolSetting>().objectName = itemData[tempId].objectName;
                activeShelfTools[i].GetComponent<ItemToolSetting>().objectDescription = itemData[tempId].objectDescription;
                activeShelfTools[i].GetComponent<ItemToolSetting>().type = itemData[tempId].type;
                activeShelfTools[i].GetComponent<Image>().sprite = itemData[tempId].objectIcon;
            }
            else
            {
                activeShelfTools[i].GetComponent<ItemToolSetting>().id = -1;
                activeShelfTools[i].GetComponent<ItemToolSetting>().objectIcon = null;
                activeShelfTools[i].GetComponent<ItemToolSetting>().objectName = "";
                activeShelfTools[i].GetComponent<ItemToolSetting>().objectDescription = "";
                activeShelfTools[i].GetComponent<ItemToolSetting>().type = "Items";
                activeShelfTools[i].GetComponent<Image>().sprite = transparentSprite;
            }
        }
    }
}