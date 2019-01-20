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

    //MASIH TERDAPAT 2 TASK YANG PERLU DI FIX KAN
    //-Saat movement pointer di shelf menu masih mengarah pada object item, fix kan.
    //-Menu Shelf menu hanya terbuka saat player menekan tombol z sambil trigger pada shelf menu
}