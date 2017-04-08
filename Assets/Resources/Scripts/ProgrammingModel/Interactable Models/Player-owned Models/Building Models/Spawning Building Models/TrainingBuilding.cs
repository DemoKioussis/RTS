using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingBuilding : Building{

	List<Stub> stubPrefabs = new List<Stub>();

	public Unit unit;
	public PlayerContext player;
	public float yOffset = 0.25f;
	public int unitIndex;

	void Start(){
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerContext>();
		unit = player.updatedPrefabs.unitPrefabs [unitIndex].GetComponent<Unit>();
	}

	// Update is called once per frame
	void Update () 
	{
		if (awake)
		{
			UpdateTime ();
			if (unitReadyToGo ()) 
			{
				// TODO: Instantiate the unit prefab
				// Debug.Log("Building is awake");
				SpawnUnit (unit);
			}
		}
	}


	protected override float Influence (Vector3 samplePosition)
	{
		return base.Influence (samplePosition) + 0;
	}

	public void SpawnUnit(Unit unit)
	{
		GameObject unitObject = InstantiatePlayableObject (unit.gameObject, transform.position, player.transform);
		if (spawnPointSet) 
		{
			unitObject.GetComponent<Unit> ().movement.moveTo (GetSpawnPoint ());
		} 
		else 
		{
			// spawn point is not set
			unitObject.GetComponent<Unit> ().movement.moveTo (new Vector3(transform.position.x, transform.position.y, transform.position.z - 1.0f));
		}
	}
		
	public override void SetToAwake(){
		Debug.Log("Training Building is Awake");
		awake = true;
	}

	public override void SetToSleep(){
		Debug.Log("Training Building is Asleep");
		awake = false;
	}

	// Set the spawn point
	public override void SetSpawnPointAs(Vector3 spawnPosition){
		if(base.GetFlagReference() == null){
			// Debug.Log ("Setting Spawn Point");
			GameObject flag = (GameObject)Instantiate (base.flagPrefab, spawnPosition, transform.rotation);
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
		if (gameTime >= 1.0f && (int)gameTime % (int)unit.unitStats.trainingTime == 0) {
			gameTime = 0.0f;
			return true;
		}

		return false;
	}
}
