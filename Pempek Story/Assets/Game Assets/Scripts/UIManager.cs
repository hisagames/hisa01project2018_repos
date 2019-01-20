using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField]
    GameObject[] UIObject;

    void Awake()
    {
        instance = this;
    }

	void Start ()
    {
        UIManager.instance.HideGameUI(0);
        UIManager.instance.HideGameUI(1);
    }
	
	void Update () {
	    	
	}

    public void ShowGameUI(int id)
    {
        UIObject[id].SetActive(true);
    }
    public void HideGameUI(int id)
    {
        UIObject[id].SetActive(false);
    }
}
