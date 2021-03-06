﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShelfSetting : MonoBehaviour
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

    public GameObject[] activeTools;
    public GameObject[] activeItems;

    [SerializeField]
    Sprite transparentSprite;

    void Start()
    {
        initiateActiveTools();
        initiateActiveItems();
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
}