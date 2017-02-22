using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Military : Unit {

	
	public MilitaryStats militaryStats;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	protected override void Interaction(Interactable newInteraction)
	{
		base.Interaction (newInteraction);

		// To do
	}
}
