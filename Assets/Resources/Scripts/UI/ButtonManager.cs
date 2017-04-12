using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour {

	public GameObject buttonAwake;
	public GameObject buttonSleep;

	public GameObject shortRange;
	public GameObject longRange;
	public GameObject resource;

	private SelectionComponent selection;

	private BuildingGroup selectedBuildingGroup;

	// Use this for initialization
	void Start () {
		selection = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<SelectionComponent> ();
		buttonAwake.SetActive (false);
		buttonSleep.SetActive (false);
		buttonAwake.GetComponent<BuildingButton>().buttonManager = this;
		buttonSleep.GetComponent<BuildingButton>().buttonManager = this;
	}
	
	// Update is called once per frame
	void Update () {
		selectedBuildingGroup = selection.selectedBuildingGroup;

		if (selectedBuildingGroup != null && !selectedBuildingGroup.IsEmpty()
			&& selectedBuildingGroup.rtsObjects[0].GetComponent<Building>().getBuildingType() != BUILDING_TYPE.TOWNCENTER) {
			buttonAwake.SetActive (true);
			buttonSleep.SetActive (true);
			if (selectedBuildingGroup.rtsObjects [0].GetComponent<Building> ().awake) {
				buttonAwake.GetComponent<BuildingButton> ().SetClicked ();
				buttonSleep.GetComponent<BuildingButton> ().SetUnclick ();
			} else {
				buttonAwake.GetComponent<BuildingButton> ().SetUnclick ();
				buttonSleep.GetComponent<BuildingButton> ().SetClicked ();
			}

		} else {
			buttonAwake.SetActive (false);
			buttonSleep.SetActive (false);
		}

		if (selectedBuildingGroup != null && !selectedBuildingGroup.IsEmpty ()
		    && selectedBuildingGroup.rtsObjects [0].GetComponent<Building> ().getBuildingType () == BUILDING_TYPE.TOWNCENTER) {
			shortRange.SetActive (true);
			longRange.SetActive (true);
			resource.SetActive (true);
		} else {
			shortRange.SetActive (false);
			longRange.SetActive (false);
			resource.SetActive (false);
		}
	}
}
