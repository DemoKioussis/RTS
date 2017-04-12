using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// use this to set what happens when we are in state
public class UnitFindTargetBehaviour : BaseStateBehaviour
{
    int playerID;

    void Start() {
        playerID = stateMachine.getRTSObject().player.playerId;
    }
    
    protected override void enter()
    {
        Collider[] hitColliders = Physics.OverlapSphere(stateMachine.transform.position, ((Unit)stateMachine.getRTSObject()).stats.viewRange);
        foreach (Collider c in hitColliders) {
            RTSObject rts = c.GetComponent<RTSObject>();
            if (rts != null) {// && rts.player.playerId != playerID) {
                ((UnitStateMachine)stateMachine).getUnit().getGroup().InteractWith(rts);
                break;
            }
        }        
    }

    protected override void exit()
    {
        
    }
    protected override void update()
    {

    }

}
