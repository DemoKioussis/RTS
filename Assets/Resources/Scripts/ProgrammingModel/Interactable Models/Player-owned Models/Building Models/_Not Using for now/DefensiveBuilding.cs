using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DefensiveBuilding : Building {

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	protected override float Influence (Vector3 samplePosition)
	{
		return base.Influence (samplePosition) + 0;
	}

	public override BUILDING_TYPE getBuildingType(){
		return BUILDING_TYPE.DEFENSE;
	}
}
