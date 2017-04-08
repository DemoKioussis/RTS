using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingKeyControls : MonoBehaviour {

	// Define your key and functions (which would be located in the Building Group component
	public static void ActivateKeyboardAction(BuildingGroup bldgGroup, char input){
		switch(input){
		case '1':
			bldgGroup.CreateNewBuilding ('1');
			break;
		case '2':
			bldgGroup.CreateNewBuilding ('2');
			break;
		case '3':
			bldgGroup.CreateNewBuilding ('3');
			break;
		case 'a':
			bldgGroup.SetToAwake ();
			break;
		case 's':
			bldgGroup.SetToSleep ();
			break; 
		case 'x':
			bldgGroup.CancelAction ();
			break;
		default:
			break;
		}
	}

}
