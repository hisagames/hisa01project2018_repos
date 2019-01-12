using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemToolSetting : MonoBehaviour
{
    public int id;

    [SerializeField]
    Image objectIcon;

    [SerializeField]
    string objectName;

    [SerializeField]
    string objectDescription;

    public GameObject leftNeighbour;
    public GameObject rightNeighbour;
    public GameObject upNeighbour;
    public GameObject downNeighbour;
}