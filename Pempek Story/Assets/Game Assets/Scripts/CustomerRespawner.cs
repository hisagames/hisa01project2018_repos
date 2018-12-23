using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerRespawner : MonoBehaviour
{
    public static CustomerRespawner instance;

	float FuzzyTimePercentage = 50f;
	bool respawningCustomerStatus = true;

	public List<GameObject> customerPrefab;

    [SerializeField]
    float[] instantiatePosX;
    [SerializeField]
    float minPosY;
    [SerializeField]
    float maxPosY;

    void Awake()
	{
		instance = this;
	}

	void Start () {
		GameBaseManager.instance.customerQueue = 0;

		customerPrefab = new List<GameObject> (); 
		int totalUnlockedCustomer = 0;

		for (int i = 0; i < GameBaseManager.instance.allCustomerPrefab.Count; i++) {
			if (GameBaseManager.instance.customerUnlockedStatus [i]) {
				customerPrefab.Add (GameBaseManager.instance.allCustomerPrefab [i]);
			}
		}

		StartCoroutine (respawningCustomers ());
		//InvokeRepeating ("respawningCustomer", 0f, customerRespawnTime);
	}

	void Update () {
	}

	void StartDelay()
	{
	}

	IEnumerator respawningCustomers() {
		yield return new WaitForSeconds (3f);
		while (respawningCustomerStatus) {
			float customerRespawnTime = GameBaseManager.instance.customerRespawnTime;
			float waitTime = Random.Range (customerRespawnTime - (customerRespawnTime * FuzzyTimePercentage / 100), 
				customerRespawnTime + (customerRespawnTime * FuzzyTimePercentage / 100));

			int prefabCount = customerPrefab.Count;
            //for (int i = 0; i < GameBaseManager.instance.orderedPempekInTimes; i++) {

            GameObject tempInstatiatePrefab = customerPrefab[Random.Range(0, prefabCount)];
            GameObject tempAfterInstantiate = Instantiate(tempInstatiatePrefab, new Vector3(instantiatePosX[Random.Range(0, instantiatePosX.Length)],
                Random.Range(minPosY, maxPosY), tempInstatiatePrefab.transform.position.z), new Quaternion());

            if (tempAfterInstantiate.transform.position.x < 0)
                tempAfterInstantiate.GetComponent<NPCMovement>().walkFromLeftToRight = true;
            else
                tempAfterInstantiate.GetComponent<NPCMovement>().walkFromLeftToRight = false;

            GameBaseManager.instance.customerQueue++;
			//}
			yield return new WaitForSeconds (waitTime);
		}
	}

	public void instantRespawningCustomer() {
		int prefabCount = customerPrefab.Count;
		//for (int i = 0; i < GameBaseManager.instance.orderedPempekInTimes; i++) {
		Instantiate (customerPrefab [Random.Range (0, prefabCount)]);
		GameBaseManager.instance.customerQueue++;
	}
}
