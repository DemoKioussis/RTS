using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseBuildingCollider : MonoBehaviour {	

	private BuildingCursor cursorObject;

	void Start(){
		cursorObject = gameObject.GetComponent<BuildingCursor> ();

	}

	void OnTriggerEnter(Collider col){
		// check the type of the building
		BUILDING_TYPE type = col.gameObject.GetComponent<Building>().getBuildingType();

		if (type.Equals (BUILDING_TYPE.RESOURCE) && (col.gameObject.layer == LayerMask.NameToLayer ("Tree") || col.gameObject.layer == LayerMask.NameToLayer ("Glue"))){
			cursorObject.CanBePlaced ();
		}
		else if (type.Equals(BUILDING_TYPE.TRAINING) && col.gameObject.layer != LayerMask.NameToLayer ("Map")) {
			// cursor collided with an object
			cursorObject.CannotBePlaced ();
		} 
	}

	void OnTriggerExit(Collider col){
		BUILDING_TYPE type = col.gameObject.GetComponent<Building>().getBuildingType();

		if (type.Equals (BUILDING_TYPE.RESOURCE) && (col.gameObject.layer == LayerMask.NameToLayer ("Tree") || col.gameObject.layer == LayerMask.NameToLayer ("Glue"))){
			cursorObject.CannotBePlaced ();
		}

		if (type.Equals(BUILDING_TYPE.TRAINING) && col.gameObject.layer != LayerMask.NameToLayer ("Map")) 
		{
			cursorObject.CanBePlaced();
		}
	}
}
