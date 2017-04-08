using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Animator))]
public class BaseStateMachine : MonoBehaviour {

    protected Animator stateMachine;
    public bool test;

    void Awake() {
        stateMachine = GetComponent<Animator>();
        BaseStateBehaviour[] behaviours = stateMachine.GetBehaviours<BaseStateBehaviour>();
        foreach (BaseStateBehaviour b in behaviours) {
            b.setStateMachine(this);
        }
    }


    private void updateParameter(string s, int value) {
        stateMachine.SetInteger(s, value);
    }
    private void updateParameter(string s, float value)
    {
        stateMachine.SetFloat(s, value);
    }
    private void updateParameter(string s, bool value)
    {
        stateMachine.SetBool(s, value);
    }


}
