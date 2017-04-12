using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : RTSObject {
	public UnitStats unitStats;
	Vector3 patrolAnchor;
    UnitGroup group;

    // Update is called once per frame

	void Update () {
/*		if (!playerSetUp && player != null) {
			player.activeUnits.Add (this);
			playerSetUp = true;
		}*/
	}
    void OnDrawGizmos()
    {
        if ( getTargetInteraction()!=null && getTargetInteraction().getInteractionType() == INTERACTION_TYPE.POSITION)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(transform.position, getTargetInteraction().getPosition());
        }
    }
    public void setGroup(UnitGroup g)
    {
        if (group != g) {
            if (group != null) {
                group.Remove(this);
            }
            transform.parent = g.transform;
            group = g;
        }

    }
    public UnitGroup getGroup()
    {
        return group;
    }

    protected override void die()
    {

        if (isAlive())
        {
            GameObject.Destroy(this.gameObject, 0.2f);
            group.Remove(this);
            player.activeUnits.Remove(this);
            base.die();
        }
    }
    public override float Influence ()
	{
		return stats.hitpoints;
		// To do
	}

	public override bool CheckCost(){
		return base.CheckCost () && player.population < player.populationLimit;
	}

	public override GameObject InstantiatePlayableObject(Vector3 position, Transform parent)
	{
		GameObject output = base.InstantiatePlayableObject (position, parent);

		Unit unit = output.GetComponent<Unit> ();
		unit.player.activeUnits.Add (unit);

		return output;
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
        ((UnitStateMachine)getStateMachine()).getAttackBehaviour().stopAttack();

    }
    public override void unitInteraction(Unit u) {
        ((UnitStateMachine)getStateMachine()).getAttackBehaviour().setAttackTarget();
    }
    public override void resourceInteraction(Resource r) { }

	void OnDestroy()
	{
		if (player != null)
			player.TakeLosses (unitStats.populationCost);
	}
}
