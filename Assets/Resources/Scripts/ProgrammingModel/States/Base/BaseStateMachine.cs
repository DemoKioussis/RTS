using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Animator))]
public class BaseStateMachine : MonoBehaviour {

    [SerializeField]
    protected Animator stateMachine;
    private int updateOffsetCounter;
    [SerializeField]
    private int updateOffset;
    public float updateSpeed;
    [SerializeField]
    private RTSObject rtsObject;
    protected bool alive;
    public BaseStateBehaviour[] behaviours;
    virtual public void Awake() {
        stateMachine = GetComponent<Animator>();
        behaviours = stateMachine.GetBehaviours<BaseStateBehaviour>();
        foreach (BaseStateBehaviour b in behaviours) {
            b.setStateMachine(this);
            b.setUpdateOffset(updateOffset);
        }
        setInitialState();
        setAlive(true);
    }

    void Update()
    {

            update();
        
    }
    protected virtual void update() {

    }

    [ContextMenu("Set States")]
    protected virtual void setInitialState() { }
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
    public RTSObject getRTSObject() {
        return rtsObject;
    }
 

    public virtual void setAlive(bool a)
    {
        if (alive != a)
        {
            alive = a;
            updateParameter("alive", a);
        }
    }



}
