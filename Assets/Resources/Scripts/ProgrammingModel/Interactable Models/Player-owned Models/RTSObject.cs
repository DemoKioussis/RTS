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
        Debug.Log("Target set");
        targetInteraction = t;
    }

	public bool CheckCost(){
		return player.glueQuantity >= stats.glueCost && player.paperQuantity >= stats.paperCost;
	}

	public void RemovePlayerResourceQuantity(){
		player.glueQuantity -= stats.glueCost;
		player.paperQuantity -= stats.paperCost;
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
		
	public static GameObject InstantiatePlayableObject(GameObject playableObject, Vector3 position, Transform parent)
	{
		GameObject output = Instantiate (playableObject, position, Quaternion.identity, parent);
		output.GetComponent<RTSObject> ().player = playableObject.GetComponent<RTSObject> ().player;
		//output.GetComponent<RTSObject> ().ReplaceStatsReferences (playableObject.GetComponent<RTSObject> ());
		output.GetComponent<RTSObject> ().getModel().enabled = true;

		if (output.GetComponent<Unit> ()) {
			Unit unit = output.GetComponent<Unit> ();
			unit.player.activeUnits.Add (unit);
		} else if (output.GetComponent<Building> ()) {
			Building bldg = output.GetComponent<Building> ();
			bldg.player.activeBuildings.Add (bldg);
		}

		return output;
	}

	public void CannotBePlaced(){
		objectIsColliding = true;
	}

	public void CanBePlaced(){
		objectIsColliding = false;
	}

    public MeshRenderer getModel() {
		if (model == null) {
			model = GetComponentInChildren<MeshRenderer>();
		}
        return model;
    }
    public virtual BaseStateMachine getStateMachine() {
        return stateDFA;
    }
}
