using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActiveDefenseBuilding : DefensiveBuilding {

	MilitaryStats militaryStats;

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
		
	public override BUILDING_TYPE getBuildingType(){
		return BUILDING_TYPE.ATTACK;
	}
}
