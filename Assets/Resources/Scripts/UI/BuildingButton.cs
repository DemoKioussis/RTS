using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingButton : MonoBehaviour {

	private SelectionComponent selection;

	private BuildingGroup selectedBuildingGroup;

	public ButtonManager buttonManager;
	public GameObject popUp;
	public GameObject buildingReference;

	// Use this for initialization
	void Start () {
		selection = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<SelectionComponent> ();
		popUp.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void SetAwake()
	{
		selectedBuildingGroup = selection.selectedBuildingGroup;	

		if (selectedBuildingGroup != null) 
		{
			selectedBuildingGroup.SetToAwake ();
		}
	}

	public void SetSleep()
	{
		selectedBuildingGroup = selection.selectedBuildingGroup;	

		if (selectedBuildingGroup != null) 
		{
			selectedBuildingGroup.SetToSleep ();
		}
	}

	public void CreateResourceChin()
	{
		selectedBuildingGroup = selection.selectedBuildingGroup;	

		if (selectedBuildingGroup != null) 
		{
			selectedBuildingGroup.CreateNewBuilding('1');
		}
	}
		
	public void CreateLongRange()
	{
		selectedBuildingGroup = selection.selectedBuildingGroup;	

		if (selectedBuildingGroup != null)
		{
			selectedBuildingGroup.CreateNewBuilding('2');
		}
	}

	public void CreateShortRange()
	{
		selectedBuildingGroup = selection.selectedBuildingGroup;	

		if (selectedBuildingGroup != null)
		{
			selectedBuildingGroup.CreateNewBuilding('3');
		}
	}

	public void SetClicked()
	{
		GetComponent<Image> ().color = Color.green;
	}

	public void SetUnclick()
	{
		GetComponent<Image> ().color = Color.white;
	}

	public void OnEnter()
	{
		Debug.Log("Cock enter");
		popUp.SetActive (true);
		int pCost = buildingReference.GetComponent<Building> ().stats.paperCost;
		int gCost = buildingReference.GetComponent<Building> ().stats.glueCost;
		//GameContext.currentGameContext.prefabs.buildingPrefabs
		popUp.GetComponentInChildren<Text>().text = "Paper: " + pCost + " Glue: " + gCost;
	}

	public void OnExit()
	{
		Debug.Log("cock exit");
		popUp.SetActive (false);
	}
}
