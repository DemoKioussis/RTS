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
                    fire();
                    

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

    private void fire() {
        ((Unit)stateMachine.getRTSObject()).projectile.fire();

        ((RTSObject)stateMachine.getRTSObject().getTargetInteraction()).takeDamage((((Military)stateMachine.getRTSObject()).militaryStats.attackStrength));
        ((UnitStateMachine)stateMachine).fire();
        Debug.DrawLine(stateMachine.getRTSObject().getTargetInteraction().getPosition(), stateMachine.transform.position, Color.green);
        stateMachine.gameObject.GetComponentInParent<Rigidbody>().AddForce(new Vector3(0, 100, 0));
    }
    private void reload() {
        ((UnitStateMachine)stateMachine).reload();
        ((Unit)stateMachine.getRTSObject()).projectile.reload();
    }


}
