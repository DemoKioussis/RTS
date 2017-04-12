using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour {

	public GameObject buttonAwake;
	public GameObject buttonSleep;

	private SelectionComponent selection;

	private BuildingGroup selectedBuildingGroup;

	// Use this for initialization
	void Start () {
		selection = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<SelectionComponent> ();
		buttonAwake.SetActive (false);
		buttonSleep.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		selectedBuildingGroup = selection.selectedBuildingGroup;

		if (selectedBuildingGroup != null && !selectedBuildingGroup.IsEmpty()) {
			buttonAwake.SetActive (true);
			buttonSleep.SetActive (true);
		} else {
			buttonAwake.SetActive (false);
			buttonSleep.SetActive (false);
		}
	}
}
