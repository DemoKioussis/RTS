﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Unit : RTSObject {
	
	public UnitStats unitStats;
	Vector3 patrolAnchor;
    private UnitGroupController group;
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

    public void setGroup(UnitGroupController g) {
        if (group != null) {
            group.removeUnit(this);
        }
        group = g;
    }
    public UnitGroupController getGroup() {
        return group;
    }
    public override INTERACTION_TYPE getInteractionType()
    {
        return INTERACTION_TYPE.UNIT;
    }
}
