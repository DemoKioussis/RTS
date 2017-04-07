using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : RTSObject {
	public UnitStats unitStats;
	Vector3 patrolAnchor;

	// Update is called once per frame
	void Update () {
		if (!playerSetUp && player != null) {
			player.activeUnits.Add (this);
			playerSetUp = true;
		}
	}

	protected override void InteractWith(Interactable target)
	{
	}

	protected override float Influence (Vector3 samplePosition)
	{
		return 0;
		// To do
	}

    public override INTERACTION_TYPE getInteractionType()
    {
        return INTERACTION_TYPE.UNIT;
    }

	public override void ReplaceStatsReferences(RTSObject otherObject)
	{
		base.ReplaceStatsReferences (otherObject);

		if (otherObject is Unit)
			unitStats = ((Unit)otherObject).unitStats;
	}
}
