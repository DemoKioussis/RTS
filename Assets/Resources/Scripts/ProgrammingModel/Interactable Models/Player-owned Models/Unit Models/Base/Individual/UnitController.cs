using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class UnitController : MonoBehaviour {

    private NavMeshAgent agent;
    private Unit unit;
    private UnitGroupController group;

    void Awake() {
        agent = GetComponent<NavMeshAgent>();
        unit = GetComponent<Unit>();
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

    }
}
