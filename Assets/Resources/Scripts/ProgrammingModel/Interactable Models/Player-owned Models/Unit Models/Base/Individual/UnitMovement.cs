using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent (typeof(Unit))]
[RequireComponent(typeof(NavMeshAgent))]

public class UnitMovement : MonoBehaviour {
    public float arriveDistance;
    private NavMeshAgent agent;
    private Unit unit;
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        unit = GetComponent<Unit>();
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
    public float getStoppingDistance()
    {
        return agent.stoppingDistance;
    }
    public void setStoppingDistance(float s)
    {
        agent.stoppingDistance = s;
    }
    public float getRadius()
    {
        return agent.radius;
    }
}
    
