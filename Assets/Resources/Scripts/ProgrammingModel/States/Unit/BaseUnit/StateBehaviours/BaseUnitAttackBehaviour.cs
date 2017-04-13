﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// use this to set what happens when we are in state
public class BaseUnitAttackBehaviour : BaseUnitBehaviour
{

  
    protected override void enter()
    {
        if (targetNotNull() && ((RTSObject)(stateMachine.getRTSObject().getTargetInteraction())).isAlive())
        {
            ((RTSObject)stateMachine.getRTSObject().getTargetInteraction()).takeDamage((((Military)stateMachine.getRTSObject()).militaryStats.attackStrength));
            ((UnitStateMachine)stateMachine).setHasFired(true);
            Debug.DrawLine(stateMachine.getRTSObject().getTargetInteraction().getPosition(), stateMachine.transform.position, Color.green);
        }
        else
        {
            ((UnitStateMachine)stateMachine).lostTarget();   
        }
        
    }
    protected override void exit()
    {
        reload();
    }
    protected override void update()
    {

    }

    private void reload() {
        ((UnitStateMachine)stateMachine).reload();
    }

    public void setAttackTarget() {
        ((UnitStateMachine)stateMachine).setAttack(true);
    }
    public void stopAttack() {
        ((UnitStateMachine)stateMachine).setAttack(false);

    }

}
