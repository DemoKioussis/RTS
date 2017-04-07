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

	Queue<Interactable> targets = new Queue<Interactable>();

	// Behaviour DFA object

	Activity currentActivity;

	protected virtual void Interaction (Interactable newTarget)
	{
		targets.Enqueue (newTarget);
	}

	protected abstract float Influence (Vector3 samplePosition);

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
		output.GetComponent<RTSObject> ().ReplaceStatsReferences (playableObject.GetComponent<RTSObject> ());
		output.GetComponent<Renderer> ().enabled = true;

		return output;
	}

	public void CannotBePlaced(){
		objectIsColliding = true;
	}

	public void CanBePlaced(){
		objectIsColliding = false;
	}
}
