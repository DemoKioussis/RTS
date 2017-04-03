﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : RTSObject {
	
	public UnitStats unitStats;
	Vector3 patrolAnchor;
    private 
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
		return 0;
		// To do
	}



    public override INTERACTION_TYPE getInteractionType()
    {
        return INTERACTION_TYPE.UNIT;
    }

	public override void ReplaceStatsReferences(RTSObject otherObject)
	{
		base.ReplaceStatsReferences (otherObject);

		if (otherObject is Unit)
			unitStats = ((Unit)otherObject).unitStats;
	}
}
