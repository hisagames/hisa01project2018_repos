using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ItemToolSetting : MonoBehaviour
{
    public int propertiesId; //like bag id
    public int id;
    public Image objectIcon;
    public string objectName;
    public string objectDescription;
    public string type;

    public GameObject leftNeighbour;
    public GameObject rightNeighbour;
    public GameObject upNeighbour;
    public GameObject downNeighbour;
}