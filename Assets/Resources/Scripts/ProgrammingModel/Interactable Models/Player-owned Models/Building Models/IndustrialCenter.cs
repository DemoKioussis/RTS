using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndustrialCenter : ResourceBuilding {
	
	// Update is called once per frame
	void Update () {
	}

	protected override void InteractWith(Interactable target)
	{
	}

	protected override float Influence (Vector3 samplePosition)
	{
		return base.Influence (samplePosition) + 0;
	}

/*	public override void SpawnUnit(Unit unit)
	{
		// To do
	}*/

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
