using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingBuilding : Building {

	List<Stub> stubPrefabs = new List<Stub>();

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

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

	GameObject InstantiatePlayableObject(GameObject playableObject)
	{
		GameObject output = Instantiate (playableObject, transform);
		output.GetComponent<RTSObject> ().ReplaceStatsReferences (playableObject.GetComponent<RTSObject> ());
		return output;
	}
}
