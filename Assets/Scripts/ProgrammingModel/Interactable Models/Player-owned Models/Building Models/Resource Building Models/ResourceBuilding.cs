using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceBuilding : Building {

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

	protected virtual float Influence (Vector3 samplePosition)
	{
		return base.Influence (samplePosition) + 0; 
		// To do
	}
}
