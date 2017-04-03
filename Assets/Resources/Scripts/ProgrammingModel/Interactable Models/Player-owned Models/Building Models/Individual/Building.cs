using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BUILDING_TYPE {RESOURCE, TRAINING, TOWNCENTER, ATTACK, DEFENSE}

public abstract class Building : RTSObject {

	bool built = false;
	Vector3 spawnPoint;

	public GameObject flagPrefab;
	GameObject flagReference;

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
		return 0;
		// To do
	}

	protected override void Heal(int hp)
	{
		base.Heal (hp);

		if (!built && stats.hitpoints == stats.maxHitpoints)
			built = true;
	}

	protected virtual void AddStub(Stub stub)
	{
		// To do
	}

	protected Vector3 GetSpawnPoint(){
		return spawnPoint;
	}

	protected void SetFlagReference(GameObject flag){
		flagReference = flag;
	}

	protected GameObject GetFlagReference(){
		return flagReference;
	}

	public virtual void SetSpawnPointAs(Vector3 spawnPosition){
		spawnPoint = spawnPosition;
	}

    public override INTERACTION_TYPE getInteractionType()
    {
        return INTERACTION_TYPE.BUILDING;
    }

	public abstract BUILDING_TYPE getBuildingType();
}
