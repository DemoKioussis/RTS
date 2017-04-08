using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestState2 : StateMachineBehaviour {

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //base.OnStateEnter(animator, stateInfo, layerIndex);
        Debug.Log("ENTRERED STATE 2222!");
    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("UPDATE 2222 2!!");
    }
}
