using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataManager : MonoBehaviour {

	void Awake(){
		//PlayerPrefs.DeleteAll ();
		//isfirst playing dan data lainnya (yg sekarang ada di method start) nanti nya akan ditaruh dimain menu
		isFirstPlaying ();
	}

	void Start () {
		CoinManager.instance.totalMoney =  PlayerPrefs.GetInt("Money");
		GameBaseManager.instance.pempekPrice = PlayerPrefs.GetInt ("PempekPrice");
		GameBaseManager.instance.customerRespawnTime = PlayerPrefs.GetFloat ("CustomerRespawnTime");
		GameBaseManager.instance.pempekPerCustomer = PlayerPrefs.GetInt ("PempekPerCustomer");
		GameTimeManager.instance.endEventSeasonTime = PlayerPrefs.GetFloat ("endEventSeasonTime");


		CoinManager.instance.updateCoinText ();
		PempekPriceManager.instance.updatePempekPriceText ();
		PempekPerCustomerManager.instance.updatePempekPerCustomerText ();
	}
	
	void Update () {
		
	}

	public static void isFirstPlaying()
	{
		if(PlayerPrefs.GetString("isNotFirstPlaying") != "true")
		{
			//base data manager
			PlayerPrefs.SetInt("Money",0);
			PlayerPrefs.SetInt ("PempekPrice", 1);
			PlayerPrefs.SetFloat ("CustomerRespawnTime", 5.25f);
			PlayerPrefs.SetInt ("PempekPerCustomer", 1);
			//PlayerPrefs.SetFloat ("PercentageSpecialGuest", 0);
			PlayerPrefs.SetFloat ("endSeasonTime", 30);
			PlayerPrefs.SetFloat ("endEventSeasonTime", 5);

			//skill data manager
			int totalSkill = 7;
			for (int i = 0; i < totalSkill; i++) 
			{
				PlayerPrefs.SetInt ("Skill" + i, 0);
			}
				
			PlayerPrefs.SetInt ("SkillMax0", 7);	
			PlayerPrefs.SetInt ("SkillMax1", 10);	
			PlayerPrefs.SetInt ("SkillMax2", 10);	
			PlayerPrefs.SetInt ("SkillMax3", 10);	
			PlayerPrefs.SetInt ("SkillMax4", 7);	
			PlayerPrefs.SetInt ("SkillMax5", 10);	
			PlayerPrefs.SetInt ("SkillMax6", 10);

			//property data manager


			//unlocked customer data manager


			//encyclopedia data manager
		

			PlayerPrefs.SetString ("isNotFirstPlaying", "true");
			PlayerPrefs.Save ();
		}
	}
}