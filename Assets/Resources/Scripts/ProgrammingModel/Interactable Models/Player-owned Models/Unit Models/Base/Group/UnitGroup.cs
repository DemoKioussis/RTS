using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class UnitGroup : RTSObjectGroup {
	
    public float arriveRadius;
    public float arrivePercent;
    NavMeshPath path;
    Interactable currentInteraction;
    Vector3 targetPosition;
    Vector3 center;
    public MapPos emptyMapPos;
    private bool isDefending = false;
    private int arrivalCount = 0;

    void Update() {
        center = getCenter();

    }

    void OnDrawGizmos() {
        if(currentInteraction!=null)
            Gizmos.DrawWireSphere(currentInteraction.getPosition(), arriveRadius);
    }
    public Interactable getInteraction() {
        return currentInteraction;
    }

    public bool isActivated() {
        return getInteraction() != null;
    }
    public void unitArrived() {

        arrivalCount++;
        if (arrivalCount > rtsObjects.Count * arrivePercent) {
            stopAgents();
        }
    }
    private void stopAgents() {
        foreach (Unit u in rtsObjects) {
            ((UnitStateMachine)u.getStateMachine()).arrived();
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


    public override void buildingInteraction(Building b) {
        preInteract(b);
        setArriveRadius(2);
        foreach (Unit myUnit in rtsObjects)
        {
            myUnit.InteractWith(b);
            ((UnitStateMachine)(myUnit.getStateMachine())).getMoveBehaviour().setArriveRadius(arriveRadius);
        }
    }
    public override void positionInteraction(MapPos p) {
        preInteract(p);
        setPositionTarget(p);
       
    }
    public override void unitInteraction(Unit u) {
        preInteract(u);
        setArriveRadius(2);
        foreach (Unit myUnit in rtsObjects)
        {
            myUnit.InteractWith(u);
            ((UnitStateMachine)(myUnit.getStateMachine())).getMoveBehaviour().setArriveRadius(arriveRadius);
        }

    }
    public override void resourceInteraction(Resource r) { }

    private void setPositionTarget(Interactable p) {
        Interactable temp = Instantiate(emptyMapPos, p.getPosition(), Quaternion.identity);
        ((MapPos)temp).setPosition(p.getPosition());
        temp.transform.parent = transform;
        currentInteraction = temp;

        setArriveRadius(1);

        path = new NavMeshPath();
        targetPosition = p.getPosition();
        NavMesh.CalculatePath(center, p.getPosition(), NavMesh.AllAreas, path);
        foreach (Unit u in rtsObjects)
        {
            UnitStateMachine s = (UnitStateMachine)u.getStateMachine();
            s.getMoveBehaviour().setPath(path);
            s.getMoveBehaviour().setArriveRadius(arriveRadius);
            u.InteractWith(temp);
        }
    }

    private void setArriveRadius(float scale) {
        if (rtsObjects.Count > 0)
        {
            Unit u = (Unit)rtsObjects[0];
            UnitStateMachine s = (UnitStateMachine)u.getStateMachine();
            arriveRadius = Mathf.Sqrt(rtsObjects.Count * s.getMoveBehaviour().getRadius());
            arriveRadius *= scale;
        }
        center = getCenter();
        arrivalCount = 0;
    }
    public void setDefensive() {
        if (isDefending)
        {
            isDefending = false;
            Debug.Log("Unsetting Defence Mode");
            foreach (Unit u in rtsObjects)
            {
                u.stopDefend();
            }
        }
        else
        {
            isDefending = true;
            Debug.Log("Setting Defence Mode");
            foreach (Unit u in rtsObjects)
            {
                u.defend();
            }
        }
    }

}
