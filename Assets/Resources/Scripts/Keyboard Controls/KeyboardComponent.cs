﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardComponent : MonoBehaviour {

	private SelectionComponent selection;

	private UnitGroup selectedUnitGroup;
	private BuildingGroup selectedBuildingGroup;

	void Awake(){
		selection = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<SelectionComponent> ();
	}

	// Update is called once per frame
	void Update () {
		if (Input.anyKeyDown) {
			// Debug.Log("Any key was pressed");

			selectedUnitGroup = selection.selectedUnitGroup;
			selectedBuildingGroup = selection.selectedBuildingGroup;	

			if (!Input.inputString.Equals("")) 
			{
				// input was not a mouse click

				if (selectedBuildingGroup != null) 
				{
					// function takes the input and finds the correct Building Group function to execute
					selectedBuildingGroup.SetToAwake ();
				}

				if (selectedUnitGroup != null) 
				{
					// function takes the input and finds the correct Building Group function to execute
					UnitKeyControls.ActivateKeyboardAction (selectedUnitGroup, Input.inputString.ToCharArray () [0]);
				}
			}
		}
	}
}
