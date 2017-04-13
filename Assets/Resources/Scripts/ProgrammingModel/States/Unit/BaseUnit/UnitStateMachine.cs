using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// use this to change parameters
public class UnitStateMachine : BaseStateMachine {

    UnitMoveBehaviour moveBehaviour;
    BaseUnitAttackBehaviour attackBehaviour;
    BaseUnitIdleBehaviour idleBehaviour;
    UnitFindTargetBehaviour findTargetBehaviour;
    Unit unit;
   
    public bool attack, hasAttackTarget,hasDestinationTarget, targetInFireRange, hasFired, atTarget,lookForTarget,defend;

    private bool reloading;
    override public void Awake() {
        base.Awake();
        unit = (Unit)getRTSObject();
        moveBehaviour = stateMachine.GetBehaviour<UnitMoveBehaviour>();
        moveBehaviour.setAgent(GetComponentInParent<NavMeshAgent>());
        moveBehaviour.awake();
        attackBehaviour = stateMachine.GetBehaviour<BaseUnitAttackBehaviour>();
        idleBehaviour = stateMachine.GetBehaviour<BaseUnitIdleBehaviour>();
        findTargetBehaviour = stateMachine.GetBehaviour<UnitFindTargetBehaviour>();


    }

    public UnitMoveBehaviour getMoveBehaviour() {
        return moveBehaviour;
    }
    public BaseUnitAttackBehaviour getAttackBehaviour()
    {
        return attackBehaviour;
    }
    public BaseUnitIdleBehaviour getIdleBehaviour()
    {
        return idleBehaviour;
    }
   
    // new update function for efficiency's sake
    protected override void update()
    {
        Interactable target = unit.getTargetInteraction();
        if (target != null)
        {
            if (target.getInteractionType() == INTERACTION_TYPE.POSITION)
                setHasDestinationTarget(true);
            else if (((RTSObject)target).isAlive())
            {
                if (((RTSObject)target).player != unit.player)
                {
                    setHasAttackTarget(true);
                }
                else
                    setHasDestinationTarget(true);

                if (hasAttackTarget && Vector3.Distance(transform.position, target.transform.position) < (unit).militaryStats.attackRange)
                {
                    setTargetInFireRange(true);
                }
                else
                    setTargetInFireRange(false);

                if (Vector3.Distance(transform.position, target.transform.position) > moveBehaviour.getArriveRadius())
                    setAtTarget(false);
            }
            else
            {
                loseTarget();
            }



        }

    }
    public Unit getUnit()
    {
        return unit;
    }

    public void reload() {
        if (unit != null && unit.isAlive())
        {
            Invoke("reloadAction", ((Military)unit).militaryStats.attackRate);
            if(unit.projectile!=null)
                unit.projectile.reload();
        }

    }

    private void reloadAction() {
        setHasFired(false);
    }
    public void fire() {
        setHasFired(true);
        unit.projectile.fire();
    }
    public void loseTarget() {
        setHasAttackTarget(false);
        setHasDestinationTarget(false);
        setAtTarget(false);
        
    }


    public void moveTo(Interactable p) {
        loseTarget();
        getMoveBehaviour().setDestination(p.getPosition());
        setAttack(false);
        unit.stopDefend();
        setDefend(false);
    }

    public void attackTarget()
    {
        setAttack(true);

    }
    public void stopAttack()
    {
        setAttack(false);
    }

    public void targetIsInFireRange() {
        setTargetInFireRange(true);
    }
    public void targetIsNotInRange()
    {
        setTargetInFireRange(false);
    }

    public void arrived() {
        setAtTarget(true);
    }

    public void defendPosition() {
        setDefend(true);
    }

    public void stopDefend() {
        setDefend(false);
    }

    protected override void setInitialState() {
        updateParameter("Attack", attack);
        updateParameter("HasAttackTarget", hasAttackTarget);
        updateParameter("HasDestinationTarget", hasDestinationTarget);
        updateParameter("TargetInFireRange", targetInFireRange);
        updateParameter("HasFired", hasFired);
        updateParameter("AtTarget", atTarget);
    }
    private void setAttack(bool b)
    {
        if (b != attack)
        {
            attack = b;
            updateParameter("Attack", b);
        }
    }
    private void setHasAttackTarget(bool b)
    {
        if (b != hasAttackTarget)
        {
            hasAttackTarget = b;
            updateParameter("HasAttackTarget", b);
        }
    }
    private void setHasDestinationTarget(bool b)
    {
        if (b != hasDestinationTarget)
        {
            hasDestinationTarget = b;
            updateParameter("HasDestinationTarget", b);
        }
    }
    private void setTargetInFireRange(bool b)
    {
        if (b != targetInFireRange)
        {
            targetInFireRange = b;
            updateParameter("TargetInFireRange", b);
        }
    }
    private void setHasFired(bool b)
    {
        if (b != hasFired)
        {
            hasFired = b;
            updateParameter("HasFired", b);
        }
    }
    private void setAtTarget(bool b)
    {
        if (b != atTarget)
        {
            atTarget = b;
            updateParameter("AtTarget", b);
        }
    }
    private void setLookForTarget(bool b)
    {
        if (b != lookForTarget)
        {
            lookForTarget = b;
            updateParameter("LookForTarget", b);
        }
    }
    private void setDefend(bool b) {
        if (b != defend)
        {
            defend = b;
            updateParameter("Defend", b);
        }
    }

}
