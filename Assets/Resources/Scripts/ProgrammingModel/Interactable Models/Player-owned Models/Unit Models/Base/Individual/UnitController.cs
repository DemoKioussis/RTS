using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Unit))]
public class UnitController : MonoBehaviour {

    public float arriveDistance;
    private NavMeshAgent agent;
    private Unit unit;
    private UnitGroupController group;
    private bool destinationReached = true;
    void Awake() {
        agent = GetComponent<NavMeshAgent>();
        unit = GetComponent<Unit>();
    }
    void Update() {
        if (!destinationReached) {
            if (agent.remainingDistance < arriveDistance) {
                agent.Stop();
            }
        }
    }
    public void setGroup(UnitGroupController g)
    {
        if (group != null)
        {
            group.removeUnit(this);
        }
        group = g;
    }
    public UnitGroupController getGroup()
    {
        return group;
    }
    public void moveTo(Vector3 p) {
        agent.SetDestination(p);
        destinationReached = false;
    }
    public void setPath(NavMeshPath p) {
        agent.SetPath(p);
    }
}
