using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : RTSObject {
	public UnitStats unitStats;
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
            if (group != g) { 
                group.Remove(this);
                transform.parent = g.transform;
                group = g;
            }
        }
        else {
            transform.parent = g.transform;
            group = g;
        }

    }
    public UnitGroup getGroup()
    {
        return group;
    }

    public override float Influence ()
	{
		return stats.hitpoints;
		// To do
	}


	public override void ReplaceStatsReferences(RTSObject otherObject)
	{
		base.ReplaceStatsReferences (otherObject);

		if (otherObject is Unit)
			unitStats = ((Unit)otherObject).unitStats;
	}

  
    public override INTERACTION_TYPE getInteractionType()
    {
        return INTERACTION_TYPE.UNIT;
    }

    public override void buildingInteraction(Building b) { }
    public override void positionInteraction(MapPos p) {

        ((UnitStateMachine)getStateMachine()).getMoveBehaviour().setDestination(p.getPosition());

    }
    public override void unitInteraction(Unit u) { }
    public override void resourceInteraction(Resource r) { }
}
