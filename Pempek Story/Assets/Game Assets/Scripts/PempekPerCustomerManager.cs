using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PempekPerCustomerManager : MonoBehaviour {

	public static PempekPerCustomerManager instance;

	[SerializeField]
	TextMesh pempekPerCustomerText;

	void Awake() 
	{
		instance = this;
	}

	void Start () {
		updatePempekPerCustomerText ();
	}

	void Update () {

	}

	public void increasePempekPerCustomer(int addedPempekPerCustomer)
	{
		GameBaseManager.instance.pempekPerCustomer += addedPempekPerCustomer;
		updatePempekPerCustomerText ();
	}

	public void updatePempekPerCustomerText()
	{
		pempekPerCustomerText.text = "pempek/orang : " + GameBaseManager.instance.pempekPerCustomer;
	}
}
