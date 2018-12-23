using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MenuTabManager : MonoBehaviour 
{
	[System.Serializable]
	public enum MenuTabType 
	{
		None,
		SkillMenuTab, 
		FurnitureMenuTab,
		RecipeMenuTab,
		ShopMenuTab,
		CustomerMenuTab
	};

	[SerializeField]
	GameObject menuTab;

	[SerializeField]
	GameObject respawnerBackground;

	bool isActive;

	[SerializeField]
	GameObject skillMenuTab;

	[SerializeField]
	MenuTabType activeMenuTabType;

	void Start () {
		setActive (false);
	}
	
	void Update () {
		
	}

	public void setActive(bool isActive)
	{
		this.isActive = isActive;

		if (isActive) {
			menuTab.GetComponent<Image> ().DOFade (1f, 0.15f);
		} 
		else {
			menuTab.GetComponent<Image> ().DOFade (0f, 0f);
		}

		menuTab.SetActive (this.isActive);
		respawnerBackground.SetActive (!this.isActive);
	}

	public void setActiveMenuTabType(int idActiveMenuTabType)
	{
		this.activeMenuTabType = (MenuTabType)idActiveMenuTabType;
		skillMenuTab.SetActive(false);

		switch (activeMenuTabType) 
		{
			case MenuTabType.SkillMenuTab:
				skillMenuTab.SetActive (true);
				break;
			case MenuTabType.FurnitureMenuTab:
				break;
			case MenuTabType.RecipeMenuTab:
				break;
			case MenuTabType.ShopMenuTab:
				break;
			case MenuTabType.CustomerMenuTab:
				break;
		}
	}
}