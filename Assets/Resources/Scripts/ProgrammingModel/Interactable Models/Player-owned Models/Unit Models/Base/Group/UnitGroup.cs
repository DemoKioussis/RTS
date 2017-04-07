using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class UnitGroup : RTSObjectGroup {
	
    public List<Unit> units;
    public bool arrived= false;
    public float arriveRadius;
    public float stopDist;
    NavMeshAgent agent;
    NavMeshPath path;
    Interactable interaction;
    Vector3 targetPosition;
    Vector3 center;

    void Awake() {
        units = new List<Unit>();
        agent = GetComponent<NavMeshAgent>();
    }

    void FixedUpdate() {
        center = getCenter();
        if (Vector3.Distance(targetPosition, center) < arriveRadius && !arrived)
        {
            stopAgents();
            arrived = true;
        }
    }

    void OnDrawGizmos() {
#if UNITY_EDITOR
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(center + Vector3.up*0.5f ,arriveRadius);
		if (path != null)
		{
		for (int i = 0; i < path.corners.Length - 1; i++)
		Debug.DrawLine(path.corners[i], path.corners[i + 1], Color.red);
		Gizmos.color = Color.blue;
		Gizmos.DrawSphere(center + Vector3.up * 0.5f, stopDist);
		}



#endif
    }
    public void moveTo(MapPos p) {
        arrived = false;
        interaction = p;
        center = getCenter();
        path = new NavMeshPath();
        targetPosition = p.getPosition();
        NavMesh.CalculatePath(center, p.getPosition(), NavMesh.AllAreas, path);

        foreach (Unit u in units) {
            u.setPath(path);
            u.setStoppingDistance(0);

        }
    }

    public bool isEmpty() {
        return units.Count == 0;
    }

    private Vector3 getCenter() {
        Vector3 center = new Vector3(0, 0, 0);

        foreach (Unit u in units) {
            center += u.transform.position;
        }
        center = center / units.Count;
        return center;
    }

    private void stopAgents() {
        Unit closest = units[0];
        foreach (Unit u in units) {
            if (u.distanceTo(targetPosition) < closest.distanceTo(targetPosition)) {
                closest = u;

            }
        }

        stopDist = Mathf.Sqrt(units.Count) * closest.getRadius() / 1.8f;
        foreach (UnitController u in units)
        {
            u.setDestination(u.transform.position);
            u.setStoppingDistance(stopDist);
        }
    }


}
