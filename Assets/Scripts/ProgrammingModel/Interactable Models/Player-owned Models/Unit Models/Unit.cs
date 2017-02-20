using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : RTSObject {

	public UnitStats unitStats;

	// Use this for initialization
	protected virtual void Start () {
		
	}
	
	// Update is called once per frame
	protected virtual void Update () {
		
	}
		
	protected virtual void Interaction(Interactable newInteraction)
	{
		base.Interaction (newInteraction);

		// To do
	}

	protected override float Influence (Vector3 samplePosition)
	{
		return 0;
		// To do
	}
}
