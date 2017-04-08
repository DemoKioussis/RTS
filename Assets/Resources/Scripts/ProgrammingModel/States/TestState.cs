using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestState : StateMachineBehaviour {

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //base.OnStateEnter(animator, stateInfo, layerIndex);
        Debug.Log("ENTRERED STATE!");
    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("UPDATE!!");
    }
}
