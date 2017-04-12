using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingButton : MonoBehaviour {

	private SelectionComponent selection;

	private BuildingGroup selectedBuildingGroup;

	public ButtonManager buttonManager;

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
			buttonManager.buttonAwake.GetComponent<BuildingButton> ().SetClicked ();
			buttonManager.buttonSleep.GetComponent<BuildingButton> ().SetUnclick ();
		}
	}

	public void SetSleep()
	{
		selectedBuildingGroup = selection.selectedBuildingGroup;	

		if (selectedBuildingGroup != null) 
		{
			selectedBuildingGroup.SetToSleep ();
			buttonManager.buttonAwake.GetComponent<BuildingButton> ().SetUnclick ();
			buttonManager.buttonSleep.GetComponent<BuildingButton> ().SetClicked ();
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
}
