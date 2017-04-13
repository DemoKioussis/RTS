using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : RTSObject {
    public UnitStats unitStats;
    Vector3 patrolAnchor;
    UnitGroup group;
    public MilitaryStats militaryStats;
    private bool isDefending = false;
    private bool heightAdvantage = false;
    public float minBonuHeight;
    public Transform defendCollider;
    // Update is called once per frame
    protected override void  Awake() {
        base.Awake();
        militaryStats.attackRange = militaryStats.AttackRangeMin;
    }
    void Update() {
        /*		if (!playerSetUp && player != null) {
                    player.activeUnits.Add (this);
                    playerSetUp = true;
                }*/
    }
    void OnDrawGizmos()
    {

        if (getTargetInteraction() != null && getTargetInteraction().getInteractionType() == INTERACTION_TYPE.POSITION)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(transform.position, getTargetInteraction().getPosition());
        }
    }
    void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, militaryStats.attackRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, stats.viewRange);
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
            if (group != null)
            {
                group.Remove(this);
            }
            player.activeUnits.Remove(this);
            base.die();
            GameObject.Destroy(this.gameObject, 0.2f);

        }
    }
    public override float Influence()
    {
        return stats.hitpoints;
        // To do
    }

    public override bool CheckCost() {
        return base.CheckCost() && player.population < player.populationLimit;
    }

    public override GameObject InstantiatePlayableObject(Vector3 position, Transform parent)
    {
        GameObject output = base.InstantiatePlayableObject(position, parent);

        Unit unit = output.GetComponent<Unit>();
        unit.player.activeUnits.Add(unit);

        return output;
    }

    public override void ReplaceStatsReferences(RTSObject otherObject)
    {
        base.ReplaceStatsReferences(otherObject);

        if (otherObject is Unit)
            unitStats = ((Unit)otherObject).unitStats;
    }



    public void defend() {

        ((UnitStateMachine)getStateMachine()).stopAttack();
        ((UnitStateMachine)getStateMachine()).loseTarget();
        ((UnitStateMachine)getStateMachine()).defendPosition();
        isDefending = true;
        updateViewAttackRanges();
        if (defendCollider != null) {
            enableDefendCollider();
            Invoke("disableDefendCollider", 0.3f);
        }


    }
    public void stopDefend() {
        ((UnitStateMachine)getStateMachine()).stopDefend();
        isDefending = false;
        updateViewAttackRanges();

        if (defendCollider != null)
        {
            disableDefendCollider();
        }

    }
    private void enableDefendCollider()
    {
        defendCollider.gameObject.SetActive(true);

    }
    private void disableDefendCollider()
    {
        defendCollider.gameObject.SetActive(false);

    }

    protected virtual void updateViewAttackRanges() {
        if (isDefending && transform.position.y > minBonuHeight)
        {
              stats.viewRange = stats.viewRangeMax;
              militaryStats.attackRange = militaryStats.AttackRangeMax;
        }
        else
        {
            stats.viewRange = stats.viewRangeMin;
            militaryStats.attackRange = militaryStats.AttackRangeMin;
        }
        updateViewRangeCollider();
    }
    public override INTERACTION_TYPE getInteractionType()
    {
        return INTERACTION_TYPE.UNIT;
    }

    public override void buildingInteraction(Building b) {
        if (b.player != player)
        {
            ((UnitStateMachine)getStateMachine()).attackTarget();
        }
        else
            ((UnitStateMachine)getStateMachine()).stopAttack();
    }
    public override void positionInteraction(MapPos p) {

        ((UnitStateMachine)getStateMachine()).moveTo(p);

    }
    public override void unitInteraction(Unit u) {
        if (u.player != player)
        {
            ((UnitStateMachine)getStateMachine()).attackTarget();
        }
        else
            ((UnitStateMachine)getStateMachine()).stopAttack();
    }
    public override void resourceInteraction(Resource r) { }

	void OnDestroy()
	{
		if (player != null)
			player.TakeLosses (unitStats.populationCost);
	}
}
