using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseStateBehaviour : StateMachineBehaviour {

    protected BaseStateMachine stateMachine;
    private int updateOffsetCounter;
    int updateOffset;

    public virtual void awake() {

    }
    public void setStateMachine(BaseStateMachine s) {
        stateMachine = s;
    }
    public void setUpdateOffset(int i) {
        updateOffset = i;
    }
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enter();

    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (updateOffsetCounter++ > updateOffset)
        {
            updateOffsetCounter = 0;
            update();

        }

    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        exit();
    }

    protected abstract void update();
    protected abstract void enter();
    protected abstract void exit();

}
