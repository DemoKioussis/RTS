using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Animator))]
public class BaseStateMachine : MonoBehaviour {

    [SerializeField]
    private Animator stateMachine;
    private int updateOffsetCounter;
    [SerializeField]
    private int updateOffset;
    void Awake() {
        stateMachine = GetComponent<Animator>();
        BaseStateBehaviour[] behaviours = stateMachine.GetBehaviours<BaseStateBehaviour>();
        foreach (BaseStateBehaviour b in behaviours) {
            b.setStateMachine(this);
        }
    }

    void Update()
    {
        if (updateOffsetCounter++ > updateOffset)
        {
            updateOffsetCounter = 0;
            update();
        }
    }
    protected virtual void update() {

    }
    protected void updateParameter(string s, int value) {
        stateMachine.SetInteger(s, value);
    }
    protected void updateParameter(string s, float value)
    {
        stateMachine.SetFloat(s, value);
    }
    protected void updateParameter(string s, bool value)
    {
        stateMachine.SetBool(s, value);
    }


}
