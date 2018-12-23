using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CustomerMovement : MonoBehaviour {

	[SerializeField]
	GameObject coinAnimated;

	[SerializeField]
	GameObject Outzone;

	[SerializeField]
	Animator animator;

	bool isWalking;
	bool isInBuyArea;
	bool isAlreadyBuyPempek;
	[SerializeField]
	float waitInBuyAreaTime;

	void Start () {
		isWalking = true;
		isInBuyArea = false;
		isAlreadyBuyPempek = false;
		transform.DOMoveX (-4.77f, 2.5f, false);
	}

	void Update () {
		//transform.Translate (new Vector3 (-0.1f, 0, 0));

		if (!isAlreadyBuyPempek) {
			if (isInBuyArea) {
				if (waitInBuyAreaTime > 0f) {
					isWalking = false;
					waitInBuyAreaTime -= Time.deltaTime;
				} else {
					isAlreadyBuyPempek = true;	
					animator.SetBool ("IsAlreadyBuyPempek", true);
					isWalking = true;
				}
			}
		}

		if (isWalking)
			transform.DOPlay ();
		else
			transform.DOPause ();
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (col.tag == "SellerTable") 
		{
			CoinManager.instance.AddMoney (GameBaseManager.instance.pempekPrice * GameBaseManager.instance.pempekPerCustomer);
			coinAnimated.GetComponent<FlyCoinAnimated> ().FlyingAnimated ();
		}
		if (col.tag == "Outzone") 
		{
			Destroy (gameObject);
		}
		if (col.tag == "BuyingZone") 
		{
			isInBuyArea = true;
			animator.SetTrigger ("InBuyArea");
		}
	}
}