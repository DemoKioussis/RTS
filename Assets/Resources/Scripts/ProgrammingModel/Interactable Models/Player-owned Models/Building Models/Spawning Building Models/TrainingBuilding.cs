using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingBuilding : Building{

	List<Stub> stubPrefabs = new List<Stub>();

	public Unit unit;
	public float yOffset = 0.25f;
	public int unitIndex;

	public float progress;

	public GameObject mapPosPrefab;

	void Awake(){
		spawnPoint = ((GameObject)Instantiate (mapPosPrefab, transform)).GetComponent<MapPos>();
	}

	void Start(){
		unit = player.updatedPrefabs.unitPrefabs [unitIndex].GetComponent<Unit>();
	}

	// Update is called once per frame
	void Update () 
	{
		if (awake)
		{
			UpdateTime (); // increase game time
			if (unitReadyToGo () && unit.CheckCost()) 
			{
				// Debug.Log("Building is awake");
				SpawnUnit (unit);
			}
		}
		// hack for debugging
		if (Input.GetKeyUp(KeyCode.O)) {
			SpawnUnit (unit);
		}
	}


	public override float Influence ()
	{
		return base.Influence () + 0;
	}

	public void SpawnUnit(Unit unit)
	{
		float z = transform.position.z - getModel ().bounds.size.z / 2 - 0.5f - 1f;
		Vector3 vec = new Vector3 (transform.position.x, transform.position.y, z);
		GameObject unitObject = unit.InstantiatePlayableObject (vec, player.transform);

		if (spawnPointSet) 
		{
			unitObject.GetComponent<Unit> ().InteractWith (GetSpawnPoint());
		} 
		else 
		{
			SetSpawnPointAs(vec - new Vector3(0, 0, 2 * getModel().bounds.size.z + 0.5f + 1f));
			// spawn point is not set
			unitObject.GetComponent<Unit> ().InteractWith (GetSpawnPoint());
			spawnPointSet = true;
		}
	}

	public override void SetToSleep(){
		Debug.Log("Training Building is Asleep");
		awake = false;
	}

	// Set the spawn point
	public override void SetSpawnPointAs(MapPos spawnPosition){
		if(base.GetFlagReference() == null){
			// Debug.Log ("Setting Spawn Point");
			GameObject flag = (GameObject)Instantiate (base.flagPrefab, spawnPosition.getPosition(), transform.rotation);
			base.SetFlagReference(flag);
			base.SetSpawnPointAs (spawnPosition);
			spawnPointSet = true; // spawn point is set

			Destroy (flag, 3); // destroy the flag object after 3 seconds
		}
	}

	public override BUILDING_TYPE getBuildingType(){
		return BUILDING_TYPE.TRAINING;
	}

	private bool unitReadyToGo(){
		progress = (float)gameTime * 100 / (float)unit.unitStats.trainingTime;

		if (gameTime >= 1.0f && (int)gameTime % (int)unit.unitStats.trainingTime == 0) {
			gameTime = 0.0f;
			return true;
		}

		return false;
	}
}
