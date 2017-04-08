using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndustrialCenter : ResourceBuilding {

	public List<GameObject> buildingPrefabs;

	private CursorComponent cursor;

	void Awake(){

		cursor = GameObject.FindGameObjectWithTag ("InputManager").GetComponent<CursorComponent>();
	}

	// Update is called once per frame
	void Update () {
	}


	protected override float Influence (Vector3 samplePosition)
	{
		return base.Influence (samplePosition) + 0;
	}

/*	public override void SpawnUnit(Unit unit)
	{
		// To do
	}*/

	public void CreateNewBuilding(char keyInput){
		int index = int.Parse (keyInput + "");

		if (index >= 0 && index < buildingPrefabs.Count) {
			cursor.SpawnObjectOnCursor (buildingPrefabs [index]);
		}
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
