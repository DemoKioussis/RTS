﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingCollider : MonoBehaviour {	

	private Building buildingObject;

	void Start(){
		buildingObject = gameObject.GetComponent<Building> ();

	}

	void OnTriggerEnter(Collider col)
	{
		if (buildingObject != null) {

			if (buildingObject.isBeingPlaced) 
			{
				// check the type of the building
				BUILDING_TYPE type = buildingObject.getBuildingType ();

				if (type.Equals (BUILDING_TYPE.RESOURCE) && (col.gameObject.layer == LayerMask.NameToLayer ("Tree") || col.gameObject.layer == LayerMask.NameToLayer ("Glue")))
				{
					buildingObject.CanBePlaced ();
				} 
				else if (type.Equals (BUILDING_TYPE.TRAINING) && col.gameObject.layer != LayerMask.NameToLayer ("Map")) 
				{
					// cursor collided with an object
					buildingObject.CannotBePlaced ();
				} 
			}
		}
	}

	void OnTriggerExit(Collider col)
	{
		if (buildingObject != null) 
		{
			BUILDING_TYPE type = buildingObject.getBuildingType ();

			if (buildingObject.isBeingPlaced) 
			{
				if (type.Equals (BUILDING_TYPE.RESOURCE) && (col.gameObject.layer == LayerMask.NameToLayer ("Tree") || col.gameObject.layer == LayerMask.NameToLayer ("Glue")))
				{
					buildingObject.CannotBePlaced ();
				} 
				else if (type.Equals (BUILDING_TYPE.TRAINING) && col.gameObject.layer != LayerMask.NameToLayer ("Map")) 
				{
					buildingObject.CanBePlaced ();
				}
			}
		}
	}
}