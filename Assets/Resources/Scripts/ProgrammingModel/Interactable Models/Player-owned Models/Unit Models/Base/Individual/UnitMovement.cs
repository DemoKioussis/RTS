using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent (typeof(Unit))]
[RequireComponent(typeof(NavMeshAgent))]

public class UnitMovement : MonoBehaviour {
    public float arriveDelta;
    private NavMeshAgent agent;
    private Unit unit;
    private Vector3 target;
    float arriveRadius;
    bool arrived;
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        unit = GetComponent<Unit>();
        unit.movement = this;
        arrived = false;
    }


    void Update() {
        checkArrived();

    }
    public void checkArrived() {
        if (!arrived && distanceTo(target) < arriveRadius)
        {
            arrived = true;
            unit.getGroup().unitArrived();
        }
    }
    public void moveTo(Vector3 p)
    {
        agent.SetDestination(p);
    }
    public void setPath(NavMeshPath p)
    {
        agent.SetPath(p);
    }
    public void setDestination(Vector3 p)
    {
        agent.SetDestination(p);
        target = p;
        arrived = false;
    }
    public void setArriveRadius(float s)
    {
        arriveRadius = s;
    }
    public void stopMovement()
    {
        agent.Stop();
        agent.ResetPath();
    }
    public float distanceTo(Vector3 p)
    {
        return Vector3.Distance(p, transform.position);
    }

    public float getRadius()
    {
        return agent.radius;
    }
}
    
