using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PempekPriceManager : MonoBehaviour {

	public static PempekPriceManager instance;

	[SerializeField]
	TextMesh pempekPriceText;

	void Awake() {
		instance = this;
	}

	void Start () {
		updatePempekPriceText ();
	}

	void Update () {
		
	}

	public void increasePempekPrice(int addedPempekPrice)
	{
		GameBaseManager.instance.pempekPrice += addedPempekPrice;
		updatePempekPriceText ();
	}

	public void updatePempekPriceText()
	{
		pempekPriceText.text = "harga pempek : " + GameBaseManager.instance.pempekPrice;
	}
}
