using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour {
	public static CoinManager instance;

	[SerializeField]
	TextMesh coinText;

	public long totalMoney;

	void Awake()
	{
		instance = this;
	}

	void Start () {
		//sementara, nanti dipindahkan pakai yg di GameDataManager.cs
		//PlayerPrefs.SetInt("Money",0);
		//PlayerPrefs.Save ();

		//AutoSaveMoneyEachSeconds
		InvokeRepeating ("SaveTotalMoney", 5f, 10f);
	}

	void Update () {
		
	}

	public void SaveTotalMoney()
	{
		PlayerPrefs.SetInt("Money",(int)totalMoney);
		PlayerPrefs.Save ();
	}

	public void AddMoney(int addedMoney)
	{
		totalMoney += addedMoney;
		updateCoinText ();
	}

	public void updateCoinText()
	{
		string coinString = "";
		long tempTotalMoney = totalMoney;

		if (tempTotalMoney >= 1000000000) {
			coinString += tempTotalMoney/1000000000 + " M ";
			tempTotalMoney = tempTotalMoney % 1000000000;
		}

		if (tempTotalMoney >= 1000000) {
			coinString += tempTotalMoney/1000000 + " Jt ";
			tempTotalMoney = tempTotalMoney % 1000000;
		}

		if (tempTotalMoney >= 1000) {
			coinString += tempTotalMoney/1000 + " Rb";
			tempTotalMoney = tempTotalMoney % 1000;
		}

		if (totalMoney >= 1000)
			coinText.text = coinString;
		else
			coinText.text = totalMoney + "";
	}
}
