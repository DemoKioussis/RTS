using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// use this to change parameters
public class BaseUnitStateMachine : BaseStateMachine {

    public bool defend, enemyInLOS, patrol, onTarget, reloading;
    public RTSObject target;

}
