using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class UnitGroup : RTSObjectGroup {
	
    public float arriveRadius;
    public float arrivePercent;
    NavMeshAgent agent;
    NavMeshPath path;
    Interactable currentInteraction;
    Vector3 targetPosition;
    Vector3 center;
    private int arrivalCount = 0;
    void Awake() {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update() {
        center = getCenter();
        
    }


    public Interactable getInteraction() {
        return currentInteraction;
    }

    public void unitArrived() {
        arrivalCount++;
        if (arrivalCount > rtsObjects.Count * arrivePercent) {
            stopAgents();
        }
    }
    private void stopAgents() {
        foreach (Unit u in rtsObjects) {
            ((UnitStateMachine)u.getStateMachine()).setAtTarget(true);
        }
    }
    void preInteract(Interactable i) {
        currentInteraction = i;

        foreach (Unit u in rtsObjects) {
            u.setGroup(this);
        }


    }

    private Vector3 getCenter() {
        Vector3 mean = new Vector3(0, 0, 0);

        foreach (Unit u in rtsObjects) {
            mean += u.transform.position;
        }
        mean = mean / rtsObjects.Count;
        return mean;
    }


    public override void buildingInteraction(Building b) { }
    public override void positionInteraction(MapPos p) {
        preInteract(p);
        if (rtsObjects.Count > 0)
        {
            Unit u = (Unit)rtsObjects[0];
            UnitStateMachine s = (UnitStateMachine)u.getStateMachine();
            arriveRadius = Mathf.Sqrt(rtsObjects.Count * s.getMoveBehaviour().getRadius());
        }
        center = getCenter();
        arrivalCount = 0;
        path = new NavMeshPath();
        targetPosition = p.getPosition();
        NavMesh.CalculatePath(center, p.getPosition(), NavMesh.AllAreas, path);
        foreach (Unit u in rtsObjects)
        {

            UnitStateMachine s = (UnitStateMachine)u.getStateMachine();
            s.getMoveBehaviour().setPath(path);
            s.getMoveBehaviour().setArriveRadius(arriveRadius);

            u.InteractWith(p);
           
            
        }
    }
    public override void unitInteraction(Unit u) { }
    public override void resourceInteraction(Resource r) { }

}
