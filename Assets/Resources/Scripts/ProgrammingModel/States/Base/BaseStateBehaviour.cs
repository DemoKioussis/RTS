using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseStateBehaviour : StateMachineBehaviour {

    protected BaseStateMachine stateMachine;
    public void setStateMachine(BaseStateMachine s) {
        stateMachine = s;
    }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enter();

    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        update();
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        exit();
    }

    protected abstract void update();
    protected abstract void enter();
    protected abstract void exit();

}
