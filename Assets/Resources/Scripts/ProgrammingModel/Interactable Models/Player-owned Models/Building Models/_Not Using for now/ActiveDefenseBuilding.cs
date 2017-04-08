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


	public override float Influence ()
	{
		return base.Influence () + 0;
		// To do
	}
		
	public override BUILDING_TYPE getBuildingType(){
		return BUILDING_TYPE.ATTACK;
	}
}
