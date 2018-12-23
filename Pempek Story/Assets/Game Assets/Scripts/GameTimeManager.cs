using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTimeManager : MonoBehaviour {

	public static GameTimeManager instance;

	[SerializeField]
	Image seasonTimer;

	[SerializeField]
	Image endSeasonImage;

	[SerializeField]
	float time;

	[SerializeField]
	float endSeasonTime;

	[SerializeField]
	float seasonRespawnTime;

	public float endEventSeasonTime;

	bool callInvokeEndSeason;

	void Awake()
	{
		instance = this;
	}

	void Start () {
		time = 0f;
		endSeasonImage.enabled = false;
		callInvokeEndSeason = false;
	}
	
	void Update () {
		time += Time.deltaTime;

		float tempTime = time;

		if (!callInvokeEndSeason) {
			seasonTimer.fillAmount = time / endSeasonTime;
			if (time >= endSeasonTime) {
				time = 0;	
				endSeasonImage.enabled = true;
				callInvokeEndSeason = true;
				Invoke ("hideEndSeasonImage", 5f);
				InvokeRepeating ("seasonRespawningCustomer", 1f, seasonRespawnTime);
			}
		}
		else {
			seasonTimer.fillAmount = time / endEventSeasonTime;
			if (time >= endEventSeasonTime) {
				time = 0;	
				callInvokeEndSeason = false;
				CancelInvoke ("seasonRespawningCustomer");
			}
		}
	}

	bool isEndSeason()
	{
		if (time >= endSeasonTime) {
			return true;
		}
		return false;
	}

	void hideEndSeasonImage()
	{
		endSeasonImage.enabled = false;
	}
	 
	public void seasonRespawningCustomer(){
		CustomerRespawner.instance.instantRespawningCustomer ();
	}
}