using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FlyCoinAnimated : MonoBehaviour {
	
	void Start ()
	{
		gameObject.SetActive (false);
		gameObject.GetComponent<TextMesh> ().text = "+" + (GameBaseManager.instance.pempekPrice 
			* GameBaseManager.instance.pempekPerCustomer);
	}

	void Update()
	{
		if (gameObject.activeInHierarchy)
			transform.DOMoveY (transform.position.y + 0.2f, 0.15f, false);
	}

	public void FlyingAnimated () {
		gameObject.SetActive(true);
		//transform.Translate (new Vector3 (0, -0.1f, 0));
		Invoke ("destroyInTime", 0.15f);
	}

	void destroyInTime()
	{
		Destroy (gameObject);
	}
}
