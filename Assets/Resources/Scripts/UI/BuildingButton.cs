using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingButton : MonoBehaviour {

	private SelectionComponent selection;

	private BuildingGroup selectedBuildingGroup;

	// Use this for initialization
	void Start () {
		selection = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<SelectionComponent> ();
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
}
