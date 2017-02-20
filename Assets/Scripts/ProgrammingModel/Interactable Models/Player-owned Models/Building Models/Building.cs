﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : RTSObject {

	bool built = false;

	public Queue<Stub> stubQueue;

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

	protected virtual void Heal(int hp)
	{
		base.Heal (hp);

		if (!built && stats.hitpoints == stats.maxHitpoints)
			built = true;
	}

	protected virtual void AddStub(Stub stub)
	{
		// To do
	}
}
