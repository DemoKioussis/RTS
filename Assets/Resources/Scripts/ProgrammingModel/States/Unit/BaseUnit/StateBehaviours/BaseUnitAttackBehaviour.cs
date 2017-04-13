using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// use this to set what happens when we are in state
public class BaseUnitAttackBehaviour : BaseUnitBehaviour
{


    protected override void enter()
    {
        if (targetNotNull() && ((UnitStateMachine)stateMachine).hasAttackTarget)
        {
            INTERACTION_TYPE interactionType = stateMachine.getRTSObject().getTargetInteraction().getInteractionType();

            if ((interactionType == INTERACTION_TYPE.UNIT || interactionType == INTERACTION_TYPE.BUILDING) && ((RTSObject)(stateMachine.getRTSObject().getTargetInteraction())).isAlive())
            {
                {

                    ((RTSObject)stateMachine.getRTSObject().getTargetInteraction()).takeDamage((((Military)stateMachine.getRTSObject()).militaryStats.attackStrength));
                    ((UnitStateMachine)stateMachine).fire();
                    Debug.DrawLine(stateMachine.getRTSObject().getTargetInteraction().getPosition(), stateMachine.transform.position, Color.green);

                }
            }
            else
            {
                ((UnitStateMachine)stateMachine).loseTarget();
            }
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


}
