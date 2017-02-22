using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndustrialCenter : ResourceBuilding {
	
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
	}

/*	public override void SpawnUnit(Unit unit)
	{
		// To do
	}*/

	public void registerResource(System.Type type, int quantity)
	{
		// To do
	}
}
