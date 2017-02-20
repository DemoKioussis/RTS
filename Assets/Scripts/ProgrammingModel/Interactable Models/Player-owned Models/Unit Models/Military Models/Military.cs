using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Military : Unit {

	public MilitaryStats militaryStats;

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
}
