using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : RTSObject {
	
	bool built = false;

	// Update is called once per frame
	void Update () {
		if (!playerSetUp && player != null) {
			player.activeBuildings.Add (this);
			playerSetUp = true;
		}
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
    public override INTERACTION_TYPE getInteractionType()
    {
        return INTERACTION_TYPE.BUILDING;
    }
}
