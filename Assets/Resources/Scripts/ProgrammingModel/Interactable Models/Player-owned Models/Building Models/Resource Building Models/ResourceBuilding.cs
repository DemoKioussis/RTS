﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceBuilding : Building {

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

	protected override float Influence (Vector3 samplePosition)
	{
		return base.Influence (samplePosition) + 0; 
		// To do
	}
		
	public override void SetSpawnPointAs(Vector3 p){
		Debug.Log ("You cannot set a spawn point for this building");
	}

	// Adds the quantity specified to the resource quantity in PlayerContext
	public void registerResource(System.Type type, int quantity)
	{
		// To do
	}
}
