using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : RTSObject {
	
	bool built = false;
	Vector3 spawnPoint;

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

	public virtual void SetSpawnPointAs(Vector3 p){
		Debug.Log ("Set a spawn point");
		spawnPoint = p;
	}

    public override INTERACTION_TYPE getInteractionType()
    {
        return INTERACTION_TYPE.BUILDING;
    }
}
