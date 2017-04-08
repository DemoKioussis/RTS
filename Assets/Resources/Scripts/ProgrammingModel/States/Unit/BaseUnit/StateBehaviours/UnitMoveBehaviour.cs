using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// use this to set what happens when we are in state
public class UnitMoveBehaviour : BaseStateBehaviour
{
    private NavMeshAgent agent;
    private NavMeshPath path;
    float arriveRadius;
    bool arrived;
    public float arriveDelta;

    void Awake() {

    }
    protected override void enter()
    {
        arrived = false;
        if(path==null)
            agent.SetDestination(getTargetPosition());
        else
            agent.SetPath(path);


    }
    protected override void exit()
    {
        path = null;
        agent.ResetPath();
    }
    protected override void update()
    {
        checkArrived();
    }

    public void setAgent(NavMeshAgent a) {
        agent = a;
    }
    public float getRadius() {
        return agent.radius;
    }
    private void checkArrived()
    {
        if (!arrived && distanceTo(getTargetPosition()) < arriveRadius)
        {
            arrived = true;
            ((Unit)stateMachine.getRTSObject()).getGroup().unitArrived();
        }
    }


    public void setArriveRadius(float r) {
        arriveRadius = r;
    }
    private Vector3 getTargetPosition() {
        return Vector3.zero;// stateMachine.getRTSObject().getTargetInteraction().transform.position;
    }
    public float distanceTo(Vector3 p)
    {
        return Vector3.Distance(p, agent.transform.position);
    }
    public void setPath(NavMeshPath p) {
        path = p;
    }
    public void setDestination(Vector3 d) {
        if (path == null)
            agent.SetDestination(d);
        else return;
    }

}
