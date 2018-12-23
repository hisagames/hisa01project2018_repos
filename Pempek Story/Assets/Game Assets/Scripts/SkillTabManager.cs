using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillTabManager : MonoBehaviour
{

	[System.Serializable]
	public class SkillTabData
	{
		public List<string> SkillString;
		public List<string> SkillDescriptionString;
		public List<string> SkillEffectString;
		public List<string> SkillPriceString;
		public List<string> SkillDetailString;
		public int skillLevel;
	}

	[SerializeField]
	List<GameObject> skillTab;

	[SerializeField]
	List<SkillTabData> skillTabData;

	[System.Serializable]
	public enum SkillType
	{
		increasePempekPrice,
		increasePempekPerCustomer,
		increaseCustomerRespawnTime,
		increaseEndEventSeasonTime
	};

	void Start () {
		LoadDataText ();
	}

	void LoadDataText () {
		for (int i = 0; i < skillTab.Count; i++) 
		{
			SkillTabScript skillTabScript = skillTab[i].GetComponent<SkillTabScript> ();

			skillTabData [i].skillLevel = PlayerPrefs.GetInt ("Skill" + i);
			int skillTabLevel = skillTabData [i].skillLevel;
			skillTabScript.SkillText.GetComponent<Text>().text = skillTabData[i].SkillString[skillTabLevel];
			skillTabScript.SkillDescriptionText.GetComponent<Text>().text = skillTabData[i].SkillDescriptionString[skillTabLevel];
			skillTabScript.SkillEffectText.GetComponent<Text>().text = skillTabData[i].SkillEffectString[skillTabLevel];
			skillTabScript.SkillPriceText.GetComponent<Text>().text = skillTabData[i].SkillPriceString[skillTabLevel];
			skillTabScript.SkillDetailText.GetComponent<Text>().text = skillTabData[i].SkillDetailString[skillTabLevel];
		}
	}

	public void buttonSkillIsClicked(int idButton)
	{
		switch (idButton) 
		{
			case 0: //Pempek Price
				if (PlayerPrefs.GetInt ("Skill" + idButton) < PlayerPrefs.GetInt ("SkillMax" + idButton) - 1) 
				{
					long skillUpgradePrice = System.Convert.ToInt64(skillTabData [idButton].SkillPriceString [PlayerPrefs.GetInt ("Skill" + idButton)]);
					if (CoinManager.instance.totalMoney >= skillUpgradePrice) 
					{
						selectingSkillType (SkillType.increasePempekPrice);
						CoinManager.instance.AddMoney((int)(-skillUpgradePrice));
						CoinManager.instance.SaveTotalMoney ();

						PlayerPrefs.SetInt ("Skill" + idButton, PlayerPrefs.GetInt ("Skill" + idButton) + 1);
					}
				}
				break;
			case 1: //Customer Respawn Time
				if (PlayerPrefs.GetInt ("Skill" + idButton) < PlayerPrefs.GetInt ("SkillMax" + idButton) - 1) 
				{
					long skillUpgradePrice = System.Convert.ToInt64(skillTabData [idButton].SkillPriceString [PlayerPrefs.GetInt ("Skill" + idButton)]);
					if (CoinManager.instance.totalMoney >= skillUpgradePrice) 
					{
						selectingSkillType(SkillType.increaseCustomerRespawnTime);
						CoinManager.instance.AddMoney((int)(-skillUpgradePrice));
						CoinManager.instance.SaveTotalMoney ();

						PlayerPrefs.SetInt ("Skill" + idButton, PlayerPrefs.GetInt ("Skill" + idButton) + 1);
					}
				}
				break;
			case 2: //Customer Respawn Time
				if (PlayerPrefs.GetInt ("Skill" + idButton) < PlayerPrefs.GetInt ("SkillMax" + idButton) - 1) 
				{
					long skillUpgradePrice = System.Convert.ToInt64(skillTabData [idButton].SkillPriceString [PlayerPrefs.GetInt ("Skill" + idButton)]);
					if (CoinManager.instance.totalMoney >= skillUpgradePrice) 
					{
						selectingSkillType(SkillType.increaseCustomerRespawnTime);
						CoinManager.instance.AddMoney((int)(-skillUpgradePrice));
						CoinManager.instance.SaveTotalMoney ();

						PlayerPrefs.SetInt ("Skill" + idButton, PlayerPrefs.GetInt ("Skill" + idButton) + 1);
					}
				}
				break;
			case 3: //EndEventSeasonTime
				if (PlayerPrefs.GetInt ("Skill" + idButton) < PlayerPrefs.GetInt ("SkillMax" + idButton) - 1) 
				{
					long skillUpgradePrice = System.Convert.ToInt64(skillTabData [idButton].SkillPriceString [PlayerPrefs.GetInt ("Skill" + idButton)]);
					if (CoinManager.instance.totalMoney >= skillUpgradePrice) 
					{
						selectingSkillType(SkillType.increaseEndEventSeasonTime);
						CoinManager.instance.AddMoney((int)(-skillUpgradePrice));
						CoinManager.instance.SaveTotalMoney ();

						PlayerPrefs.SetInt ("Skill" + idButton, PlayerPrefs.GetInt ("Skill" + idButton) + 1);
					}
				}
				break;
			case 4: //Pempek Price
				if (PlayerPrefs.GetInt ("Skill" + idButton) < PlayerPrefs.GetInt ("SkillMax" + idButton) - 1) 
				{
					long skillUpgradePrice = System.Convert.ToInt64(skillTabData [idButton].SkillPriceString [PlayerPrefs.GetInt ("Skill" + idButton)]);
					if (CoinManager.instance.totalMoney >= skillUpgradePrice) 
					{
						selectingSkillType(SkillType.increasePempekPrice);
						CoinManager.instance.AddMoney((int)(-skillUpgradePrice));
						CoinManager.instance.SaveTotalMoney ();

						PlayerPrefs.SetInt ("Skill" + idButton, PlayerPrefs.GetInt ("Skill" + idButton) + 1);
					}
				}
				break;
			case 5: //EndEventSeasonTime
				if (PlayerPrefs.GetInt ("Skill" + idButton) < PlayerPrefs.GetInt ("SkillMax" + idButton) - 1) 
				{
					long skillUpgradePrice = System.Convert.ToInt64(skillTabData [idButton].SkillPriceString [PlayerPrefs.GetInt ("Skill" + idButton)]);
					if (CoinManager.instance.totalMoney >= skillUpgradePrice) 
					{
						selectingSkillType(SkillType.increaseEndEventSeasonTime);
						CoinManager.instance.AddMoney((int)(-skillUpgradePrice));
						CoinManager.instance.SaveTotalMoney ();

						PlayerPrefs.SetInt ("Skill" + idButton, PlayerPrefs.GetInt ("Skill" + idButton) + 1);
					}
				}
				break;
			case 6: //PempekPerCustomer
				if (PlayerPrefs.GetInt ("Skill" + idButton) < PlayerPrefs.GetInt ("SkillMax" + idButton) - 1) 
				{
					long skillUpgradePrice = System.Convert.ToInt64(skillTabData [idButton].SkillPriceString [PlayerPrefs.GetInt ("Skill" + idButton)]);
					if (CoinManager.instance.totalMoney >= skillUpgradePrice) 
					{
						selectingSkillType(SkillType.increasePempekPerCustomer);
						CoinManager.instance.AddMoney((int)(-skillUpgradePrice));
						CoinManager.instance.SaveTotalMoney ();

						PlayerPrefs.SetInt ("Skill" + idButton, PlayerPrefs.GetInt ("Skill" + idButton) + 1);
					}
				}
				break;
		}
		CoinManager.instance.updateCoinText ();
		LoadDataText ();
		PlayerPrefs.Save ();
	}

	void selectingSkillType(SkillType skillType)
	{
		switch (skillType) 
		{
			case SkillType.increasePempekPrice:
				GameBaseManager.instance.pempekPrice *= 2;
				PlayerPrefs.SetInt ("PempekPrice", GameBaseManager.instance.pempekPrice);
				PempekPriceManager.instance.updatePempekPriceText ();
			break;
			case SkillType.increaseCustomerRespawnTime:
				GameBaseManager.instance.customerRespawnTime -= 0.25f;
				PlayerPrefs.SetFloat ("CustomerRespawnTime", GameBaseManager.instance.customerRespawnTime);
			break;
			case SkillType.increaseEndEventSeasonTime:
				GameTimeManager.instance.endEventSeasonTime += 0.5f;
				PlayerPrefs.SetFloat ("endEventSeasonTime", GameTimeManager.instance.endEventSeasonTime);
			break;
			case SkillType.increasePempekPerCustomer:
				GameBaseManager.instance.pempekPerCustomer += 1;
				PlayerPrefs.SetInt ("PempekPerCustomer", GameBaseManager.instance.pempekPerCustomer);
				PempekPerCustomerManager.instance.updatePempekPerCustomerText ();
			break;
		}
	}
}