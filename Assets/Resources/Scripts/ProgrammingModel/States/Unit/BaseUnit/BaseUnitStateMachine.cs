using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// use this to change parameters
public class BaseUnitStateMachine : BaseStateMachine {

    Unit u;
    public bool defend, inLOS, patrol, onTarget, reloading, targetNull, targetInRange, attack;
    public RTSObject target;
    public int queueSize;

    void Awake() {
        u = GetComponentInParent<Unit>();
        updateAll();
    }

    // new update function for efficiency's sake
    protected override void update()
    {
        
    }

    [ContextMenu("UPDATE ALL")]
    public void updateAll() {
        setDefend(defend);
        setAttack(attack);
        setInLos(inLOS);
        setOnTarget(onTarget);
        setPatrol(patrol);
        setQueueSize(queueSize);
        setReloading(reloading);
        setTarget(target);
        setTargetInRange(targetInRange);
    }

    public void setDefend(bool b) {
        defend = b;
        updateParameter("defend", b);
    }
    public void setAttack(bool b){
        attack = b;
        updateParameter("attack", b);
    }
    public void setInLos(bool b)
    {
        inLOS = b;
        updateParameter("inLOS", b);
    }
    public void setPatrol(bool b)
    {
        patrol = b;
        updateParameter("patrol", b);
    }
    public void setOnTarget(bool b)
    {
        onTarget = b;
        updateParameter("onTarget", b);
    }
    public void setReloading(bool b)
    {
        reloading = b;
        updateParameter("reloading", b);
    }
    public void setTarget(RTSObject o)
    {
        target = o;
        targetNull = (target == null);
        updateParameter("targetNull", targetNull);
    }
    public void setTargetInRange(bool b)
    {
        targetInRange = b;
        updateParameter("targetInRange", b);
    }
    public void setQueueSize(int i ) {
        queueSize = i;
    }
}
