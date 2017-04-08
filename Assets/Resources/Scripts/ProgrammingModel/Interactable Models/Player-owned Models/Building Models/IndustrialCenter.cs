using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndustrialCenter : ResourceBuilding {

	private GameObject[] buildings;

	private CursorComponent cursor;

	void Awake(){
		buildings = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerContext> ().updatedPrefabs.buildingPrefabs;
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

		if (index >= 0 && index < buildings.Length) {
			cursor.SpawnObjectOnCursor (buildings [index]);
		}
	}

	public void CancelAction(){
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
