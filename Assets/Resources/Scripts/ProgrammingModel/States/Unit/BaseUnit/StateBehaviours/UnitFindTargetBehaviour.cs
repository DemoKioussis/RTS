using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// use this to set what happens when we are in state
public class UnitFindTargetBehaviour : BaseUnitBehaviour
{
    int playerID;
    public LayerMask mask;
    public int searchOffset;
    bool targetFound;
    void Start() {
        playerID = stateMachine.getRTSObject().player.playerId;
    }

    public override void awake() {
        setUpdateOffset(searchOffset);
    }
    protected override void enter()
    {
        lookForTarget();
  
    }

    protected override void exit()
    {
        if (!targetFound) {
            ((UnitStateMachine)stateMachine).stopAttack();
            ((UnitStateMachine)stateMachine).targetIsNotInRange();
        }
        if (targetFound)
        {
            ((UnitStateMachine)stateMachine).attackTarget();
            if(Vector3.Distance(stateMachine.getRTSObject().targetInteraction.transform.position,stateMachine.transform.position) <= stateMachine.getRTSObject().stats.viewRange)
                ((UnitStateMachine)stateMachine).targetIsInFireRange();
        }

    }
    protected override void update()
    {
        lookForTarget();
    }

    void lookForTarget() {
        Collider[] hitColliders = Physics.OverlapSphere(stateMachine.transform.position, ((Unit)stateMachine.getRTSObject()).stats.viewRange, mask);

        foreach (Collider c in hitColliders)
        {
            
            InteractableLink link = c.GetComponent<InteractableLink>();
            if (link != null)
            {
                RTSObject rts = (RTSObject)link.getInteractable();

                if (rts != null && rts.player.playerId != playerID) {
                    ((UnitStateMachine)stateMachine).getUnit().getGroup().InteractWith(rts);
                    targetFound = true;
                    return;
                }
            }
        }

    }

}
