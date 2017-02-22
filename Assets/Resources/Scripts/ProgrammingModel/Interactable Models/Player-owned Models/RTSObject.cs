using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class RTSObject : Interactable {

	public enum Activity {NONE, MOVE, GATHER, BUILD, REPAIR, ATTACK, HEAL, PATROL};

	
	public Stats stats;

	Queue<Interactable> targets = new Queue<Interactable>();

	// Behaviour DFA object

	Activity currentActivity;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

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
}
