using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndustrialCenter : Building {

	public GameObject[] buildings;

	private CursorComponent cursor;

	void Awake(){
		cursor = GameObject.FindGameObjectWithTag ("InputManager").GetComponent<CursorComponent>();
	}

	// Update is called once per frame
	void Update () {
	}


	public override float Influence ()
	{
		return base.Influence () + 0;
	}

/*	public override void SpawnUnit(Unit unit)
	{
		// To do
	}*/

	public void CreateNewBuilding(char keyInput){
		int index = int.Parse (keyInput + "");

		RTSObject newObj = buildings [index].GetComponent<RTSObject> ();

		if (cursor.currentRTSObject != null) {
			// object on cursor
			if (cursor.currentRTSObject.GetComponent<Building> () != null) {
				// object is a building
				if (RTSObject.compareRTSObject (cursor.currentRTSObject, newObj)) {
					this.CancelAction ();
				}
			}
		}

		if (index >= 0 && index < buildings.Length && newObj.CheckCost()) {
			cursor.SpawnObjectOnCursor (buildings [index]);
		}
	}

	public void CancelAction(){
		player.Sell(cursor.currentRTSObject);
		cursor.CancelAction ();
	}

	public override void SetSpawnPointAs(Vector3 spawnPosition){
		Debug.Log ("Cannot set spawn point for this building");
	}

	public void registerResource(System.Type type, int quantity)
	{
		// To do
	}

	public override BUILDING_TYPE getBuildingType(){
		return BUILDING_TYPE.TOWNCENTER;
	}
}
