using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour {

    public Animator stateMachine;
    public bool set;
    void Update() {
        stateMachine.SetBool("isTest", set);
    }
}
