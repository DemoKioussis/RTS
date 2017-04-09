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
    Unit unit;
   
    public bool attack, hasTarget, targetInFireRange, hasFired, atTarget,lookForTarget;

    private bool reloading;
    override public void Awake() {
        base.Awake();
        unit = (Unit)getRTSObject();
        moveBehaviour = stateMachine.GetBehaviour<UnitMoveBehaviour>();
        attackBehaviour = stateMachine.GetBehaviour<BaseUnitAttackBehaviour>();
        idleBehaviour = stateMachine.GetBehaviour<BaseUnitIdleBehaviour>();

        moveBehaviour.setAgent(GetComponentInParent<NavMeshAgent>());
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
            setHasTarget(true);
            // attackDistance
            if (Vector3.Distance(transform.position, target.transform.position) < ((Military)unit).militaryStats.maxAttackRange)
            {
                setTargetInFireRange(true);
            }
            else
                setTargetInFireRange(false);

            if (Vector3.Distance(transform.position, target.transform.position) > moveBehaviour.getArriveRadius())
                setAtTarget(false);
        }
        else
            setHasTarget(false);

        if (!hasTarget && attack) {
            setLookForTarget(true);
        }



    }


    public void reload() {
        Invoke("reloadAction", ((Military)unit).militaryStats.attackRate);
    }

    private void reloadAction() {
        setHasFired(false);
    }
    protected override void setInitialState() {
        updateParameter("Attack", attack);
        updateParameter("HasTarget", hasTarget);
        updateParameter("TargetInFireRange", targetInFireRange);
        updateParameter("HasFired", hasFired);
        updateParameter("AtTarget", atTarget);
    }
    public void setAttack(bool b)
    {
        if (b != attack)
        {
            attack = b;
            updateParameter("Attack", b);
        }
    }
    public void setHasTarget(bool b)
    {
        if (b != hasTarget)
        {
            hasTarget = b;
            updateParameter("HasTarget", b);
        }
    }
    public void setTargetInFireRange(bool b)
    {
        if (b != targetInFireRange)
        {
            targetInFireRange = b;
            updateParameter("TargetInFireRange", b);
        }
    }
    public void setHasFired(bool b)
    {
        if (b != hasFired)
        {
            hasFired = b;
            updateParameter("HasFired", b);
        }
    }
    public void setAtTarget(bool b)
    {
        if (b != atTarget)
        {
            atTarget = b;
            updateParameter("AtTarget", b);
        }
    }
    public void setLookForTarget(bool b)
    {
        if (b != lookForTarget)
        {
            lookForTarget = b;
            updateParameter("LookForTarget", b);
        }
    }

}
