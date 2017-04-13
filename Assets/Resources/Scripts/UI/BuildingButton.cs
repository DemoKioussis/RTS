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
	public PlayerContext player;

	public bool enoughResources;

	AudioSource source;
	public AudioClip cannotSFX;

	// Use this for initialization
	void Start () {
		source = GetComponent<AudioSource> ();
		selection = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<SelectionComponent> ();

		if (popUp != null)
			popUp.SetActive (false);

		List<PlayerContext> players = GameContext.currentGameContext.activePlayers;
		player = null;
		foreach (PlayerContext p in players) {
			if (p.strategy.GetType () == typeof(PlayerStrategy)) {
				player = p;
			}

		}
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
		if (!enoughResources)
			source.PlayOneShot (cannotSFX, 1.0f);

		selectedBuildingGroup = selection.selectedBuildingGroup;	

		if (selectedBuildingGroup != null) 
		{
			selectedBuildingGroup.CreateNewBuilding('1');
		}
	}
		
	public void CreateLongRange()
	{
		if (!enoughResources)
			source.PlayOneShot (cannotSFX, 1.0f);

		selectedBuildingGroup = selection.selectedBuildingGroup;	

		if (selectedBuildingGroup != null)
		{
			selectedBuildingGroup.CreateNewBuilding('2');
		}
	}

	public void CreateShortRange()
	{
		if (!enoughResources)
			source.PlayOneShot (cannotSFX, 1.0f);

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
		popUp.SetActive (true);
		int pCost = buildingReference.GetComponent<Building> ().stats.paperCost;
		int gCost = buildingReference.GetComponent<Building> ().stats.glueCost;
		//GameContext.currentGameContext.prefabs.buildingPrefabs
		popUp.GetComponentInChildren<Text>().text = "Paper: " + pCost + " Glue: " + gCost;

		if (CheckBuildingCost (buildingReference.GetComponent<Building> ())) {
			GetComponent<Image> ().color = Color.green;
			enoughResources = true;
		} else {
			GetComponent<Image> ().color = Color.red;
			enoughResources = false;
		}

	}

	public void OnExit()
	{
		popUp.SetActive (false);
		GetComponent<Image> ().color = Color.white;
	}

	bool CheckBuildingCost(Building building)
	{
		return player.glueQuantity >= building.stats.glueCost && player.paperQuantity >= building.stats.paperCost;
	}
}
