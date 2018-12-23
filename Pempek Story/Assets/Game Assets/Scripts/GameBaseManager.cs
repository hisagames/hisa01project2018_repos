using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBaseManager : MonoBehaviour {
	
	public static GameBaseManager instance;

	public List<GameObject> allCustomerPrefab;
	public List<bool> customerUnlockedStatus;

	public float customerRespawnTime;
	public int pempekPerCustomer;
	public int pempekPrice;

	public int customerQueue;

	float maxGameTime;
	float gameTime;

	public GameObject customerRespawner;

	void Awake()
	{
		instance = this;
	}

	void Start () {
		GameObject temp = Instantiate (customerRespawner);
		temp.SetActive (true);
		//customerRespawner = temp;
	}
		
	public void instanceRespawingCustomer(){
		CustomerRespawner.instance.instantRespawningCustomer ();
	}
}