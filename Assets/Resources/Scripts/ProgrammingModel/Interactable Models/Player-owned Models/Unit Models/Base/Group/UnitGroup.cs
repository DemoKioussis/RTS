using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class UnitGroup : RTSObjectGroup {
	
    public float arriveRadius;
    public float arrivePercent;
    protected NavMeshPath path;
    protected Interactable currentInteraction;
    protected Vector3 targetPosition;
    protected Vector3 center;
    public MapPos emptyMapPos;
    private bool isDefending = false;
    private int arrivalCount = 0;

    public virtual void Update() {
        center = getCenter();

    }

    public void removeNull()
    {
        for (int i = 0; i < rtsObjects.Count; i++)
        {
            if (rtsObjects[i] == null)
            {
                rtsObjects.RemoveAt(i);
                i = 0;
            }
        }
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
        removeNull();
        foreach (Unit u in rtsObjects) {
            if(u!=null)
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
        removeNull();
        foreach (Unit u in rtsObjects) {
            if(u!=null)
                mean += u.transform.position;
        }
        if (rtsObjects.Count > 0)
        {
            mean = mean / rtsObjects.Count;
        }

        return mean;
    }


    public override void buildingInteraction(Building b) {
        preInteract(b);
        setArriveRadius(2);
        removeNull();
        foreach (Unit myUnit in rtsObjects)
        {
            if (myUnit != null)
            {
                myUnit.InteractWith(b);
                ((UnitStateMachine)(myUnit.getStateMachine())).getMoveBehaviour().setArriveRadius(arriveRadius);
            }
        }
    }
    public override void positionInteraction(MapPos p) {
        preInteract(p);
        setPositionTarget(p);
       
    }
    public override void unitInteraction(Unit u) {
        preInteract(u);
        setArriveRadius(2);
        removeNull();
        foreach (Unit myUnit in rtsObjects)
        {
            if (myUnit != null)
            {
                myUnit.InteractWith(u);
                ((UnitStateMachine)(myUnit.getStateMachine())).getMoveBehaviour().setArriveRadius(arriveRadius);
            }
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
        removeNull();
        if (isDefending)
        {
            isDefending = false;
            Debug.Log("Unsetting Defence Mode");
            foreach (Unit u in rtsObjects)
            {
                if(u!=null)
                    u.stopDefend();
            }
        }
        else
        {
            isDefending = true;
            Debug.Log("Setting Defence Mode");
            foreach (Unit u in rtsObjects)
            {
                if(u!=null)
                    u.defend();
            }
        }
    }

}
