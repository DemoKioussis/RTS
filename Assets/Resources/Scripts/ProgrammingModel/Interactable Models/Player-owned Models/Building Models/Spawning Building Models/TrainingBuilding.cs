using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingBuilding : Building{

	List<Stub> stubPrefabs = new List<Stub>();

	public GameObject unit;
	public bool awake;

	public float yOffset = 0.25f;

	// Use this for initialization
	void Start () {
		awake = true;
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.S)) {
			Debug.Log ("Building is awake: " + awake);
			awake = !awake;
		}

		if (Input.GetButton ("LeftClick")) {
			RaycastHit hit = Utils.GetPositionFromMouseClick (1 << LayerMask.NameToLayer("Map"));

			if (hit.collider != null) {
				SetSpawnPointAs (hit.point + new Vector3(0, yOffset, 0));
			}
		}

		/*
		// test spawning units
		if (Input.GetKey (KeyCode.I) && awake) {
			GameObject obj = (GameObject) InstantiatePlayableObject (unit);
			obj.GetComponent<UnitController>().setDestination(base.GetSpawnPoint());
		}
		*/
	}

	protected override void Interaction(Interactable newInteraction)
	{
		base.Interaction (newInteraction);

		// To do
	}

	protected override float Influence (Vector3 samplePosition)
	{
		return base.Influence (samplePosition) + 0;
	}

	public virtual void SpawnUnit(Unit unit)
	{
		// To do
	}

	// Set the spawn point
	public override void SetSpawnPointAs(Vector3 spawnPosition){
		if(base.GetFlagReference() == null){
			GameObject flag = (GameObject)Instantiate (base.flagPrefab, spawnPosition, transform.rotation);
			base.SetFlagReference(flag);
			base.SetSpawnPointAs (spawnPosition);

			Destroy (flag, 3); // destroy the flag object after 3 seconds
		}
	}

	GameObject InstantiatePlayableObject(GameObject playableObject)
	{
		GameObject output = Instantiate (playableObject, transform);
		output.GetComponent<RTSObject> ().ReplaceStatsReferences (playableObject.GetComponent<RTSObject> ());
		return output;
	}

	public override BUILDING_TYPE getBuildingType(){
		return BUILDING_TYPE.TRAINING;
	}
}
