using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseUnitBehaviour : BaseStateBehaviour
{

    protected bool targetNotNull() {
        return stateMachine.getRTSObject() != null;
    }
    protected bool targetAlive() {
        if (stateMachine.getRTSObject() != null)
            return stateMachine.getRTSObject().isAlive();
        else
            return false;
    }

}
