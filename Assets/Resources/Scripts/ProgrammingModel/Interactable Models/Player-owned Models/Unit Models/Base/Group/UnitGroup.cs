using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;
public class UnitGroup :MonoBehaviour{

    public List<UnitController> units;
    public float groupMovementSpeed;
    NavMeshAgent agent;
    NavMeshPath path;
    Interactable interaction;
    Vector3 targetPosition;


    void Awake() {
        units = new List<UnitController>();
        agent = GetComponent<NavMeshAgent>();
    }

    void OnDrawGizmos() {
#if UNITY_EDITOR
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(getCenter() + Vector3.up*0.5f ,0.1f);
        for (int i = 0; i < path.corners.Length - 1; i++)
            Debug.DrawLine(path.corners[i], path.corners[i + 1], Color.red);
#endif
    }
    public void moveTo(MapPos p) {
        interaction = p;
        Vector3 center = getCenter();
        path = new NavMeshPath();
        NavMesh.CalculatePath(center, p.getPosition(), NavMesh.AllAreas, path);

        foreach (UnitController u in units) {
            u.setPath(path);
        }
    }

    public bool isEmpty() {
        return units.Count == 0;
    }

    public Vector3 getCenter() {
        Vector3 center = new Vector3(0, 0, 0);

        foreach (UnitController u in units) {
            center += u.transform.position / units.Count;
        }
        return center;

    }


}
