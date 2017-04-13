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
            ((UnitStateMachine)stateMachine).setAttack(false);
            ((UnitStateMachine)stateMachine).setTargetInFireRange(false);
        }
        if (targetFound)
        {
            ((UnitStateMachine)stateMachine).setAttack(true);
            ((UnitStateMachine)stateMachine).setTargetInFireRange(false);
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
            Debug.Log(c);
            
            InteractableLink link = c.GetComponent<InteractableLink>();
            if (link != null)
            {
                RTSObject rts = (RTSObject)link.getInteractable();

                if (rts != null && rts != this.stateMachine.getRTSObject())
                {// && rts.player.playerId != playerID) {
                    ((UnitStateMachine)stateMachine).getUnit().getGroup().InteractWith(rts);
                    targetFound = true;
                    return;
                }
            }
        }

    }

}
