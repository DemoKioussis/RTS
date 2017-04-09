﻿using System.Collections;
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
    public float movementDelta;
    Vector3 lastPosition;
    Vector3 origionalPosition;
    void Awake() {

    }
    protected override void enter()
    {

        arrived = false;

     //   if(path==null)
        agent.SetDestination(getTargetPosition());
        lastPosition = getTargetPosition();
        origionalPosition = lastPosition;
    //    else
   //         agent.SetPath(path);


    }
    protected override void exit()
    {
        path = null;
        agent.ResetPath();
    }
    protected override void update()
    {
        checkArrived();
        checkMoved();
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

    private void checkMoved() {
        if (Vector3.Distance(lastPosition, getTargetPosition()) > movementDelta) {
            lastPosition = getTargetPosition();
            setDestination(lastPosition);
        }
    }

    public void setArriveRadius(float r) {
        arriveRadius = r;
    }
    public float getArriveRadius() {
        return arriveRadius;
    }
    private Vector3 getTargetPosition() {
        return stateMachine.getRTSObject().getTargetInteraction().getPosition();
    }
    public float distanceTo(Vector3 p)
    {
        return Vector3.Distance(p, agent.transform.position);
    }
    public void setPath(NavMeshPath p) {
        path = p;
        arrived = false;
    }
    public void setDestination(Vector3 d) {
        agent.SetDestination(d);
        arrived = false;
    }

}
