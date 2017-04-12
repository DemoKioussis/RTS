using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class RTSObject : Interactable {

	public enum Activity {NONE, MOVE, GATHER, BUILD, REPAIR, ATTACK, HEAL, PATROL};

	public Stats stats;
	protected bool playerSetUp = false;
	public PlayerContext player = null;

	public bool objectIsColliding;
	public bool isBeingPlaced;
    private bool alive;
	public Interactable interactable;
	public GameObject selectionCircle;
    MeshRenderer model;
    BaseStateMachine stateDFA;
    Interactable targetInteraction;
    void Awake()
	{
		interactable = GetComponent<Interactable>();
        model = GetComponentInChildren<MeshRenderer>();
        stateDFA = GetComponentInChildren<BaseStateMachine>();
    }

	Activity currentActivity;

	public abstract float Influence ();

    public void setTarget(Interactable t) {
        targetInteraction = t;
    }

	public override Stats GetStats ()
	{
		return stats;
	}

	public virtual bool CheckCost(){
		return player.glueQuantity >= stats.glueCost && player.paperQuantity >= stats.paperCost;
	}

    public override void InteractWith(Interactable i)
    {
        setTarget(i);
        base.InteractWith(i);
    }
    public Interactable getTargetInteraction() {
        return targetInteraction;
    }

    public void takeDamage(float d)
    {
        Debug.Log("I TOOK "+d+" DAMAGE!");
        stats.hitpoints -= d;
        if (stats.hitpoints <= 0) {
            die();
        }
    }
    private void die() {
        if (alive) {
            Debug.Log("Oh no! I died");
            alive = false;
            GameObject.Destroy(this.gameObject, 3);
                }

    }
    protected virtual void Heal(int hp)
	{
		stats.hitpoints += hp;

		if (stats.hitpoints > stats.maxHitpoints)
			stats.hitpoints = stats.maxHitpoints;
	}

	public virtual void ReplaceStatsReferences (RTSObject otherObject)
	{
		stats = otherObject.stats;
	}
		
	public virtual GameObject InstantiatePlayableObject(Vector3 position, Transform parent)
	{
		GameObject output = Instantiate (gameObject, position, Quaternion.identity, parent);
		output.GetComponent<RTSObject> ().player = player;
		output.GetComponent<RTSObject> ().player.Buy (output.GetComponent<RTSObject> ());
		//output.GetComponent<RTSObject> ().ReplaceStatsReferences (playableObject.GetComponent<RTSObject> ());
		output.GetComponent<RTSObject> ().getModel().enabled = true;

		return output;
	}

	public static void RemovePlayableObject(RTSObject rtsObject) {
		if (rtsObject.GetComponent<Unit> ()) {
			Unit unit = rtsObject.GetComponent<Unit> ();
			unit.player.activeUnits.Remove (unit);
		} else if (rtsObject.GetComponent<Building> ()) {
			Building bldg = rtsObject.GetComponent<Building> ();
			bldg.player.activeBuildings.Remove (bldg);
		}
	}

	public static bool compareRTSObject(RTSObject current, RTSObject next){
		if(current == null || next == null){
			return false;
		}

		// check if it is a building
		Building bldg1 = current.GetComponent<Building> ();
		Building bldg2 = next.GetComponent<Building> ();

		if (bldg1.getBuildingType () == bldg2.getBuildingType ()) {
			// same building type

			if (bldg1.GetComponent<TrainingBuilding> () != null) {
				// both are training buldings, check if they have the same unitIndex
				return bldg1.GetComponent<TrainingBuilding>().unitIndex != bldg2.GetComponent<TrainingBuilding>().unitIndex;
			}
				
			return false;
		}

		return true;
	}

	public void CannotBePlaced(){
		objectIsColliding = true;
	}

	public void CanBePlaced(){
		objectIsColliding = false;
	}

	public virtual void Destroy() {}

    public MeshRenderer getModel() {
		if (model == null) {
			model = GetComponentInChildren<MeshRenderer>();
		}
        return model;
    }
    public virtual BaseStateMachine getStateMachine() {
        return stateDFA;
    }
    public bool isAlive() {
        return alive;
    }
}
