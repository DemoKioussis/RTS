using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : RTSObject {
	public UnitStats unitStats;
    public UnitMovement movement;
	Vector3 patrolAnchor;
    UnitGroup group;

	// Update is called once per frame
	void Update () {
		if (!playerSetUp && player != null) {
			player.activeUnits.Add (this);
			playerSetUp = true;
		}
	}
    public void setGroup(UnitGroup g)
    {
        if (group != null)
        {
            group.removeUnit(this);
        }
        group = g;
    }
    public UnitGroup getGroup()
    {
        return group;
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
    public override void buildingInteratction(Building b) { }
    public override void positionInteration(MapPos p) { }
    public override void unitInteraction(Unit u) { }
    public override void resourceInteraction(Resource r) { }
}
