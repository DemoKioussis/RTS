using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : Interactable {
	
	public int quantity;
	public int startingMinQuantity;
	public int startingMaxQuantity;

	// Use this for initialization
	void Start () {
		quantity = Random.Range (startingMinQuantity, startingMaxQuantity + 1);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	protected virtual void GetQuantity(int consumeQuantity)
	{
		// To do
	}
    public override INTERACTION_TYPE getInteractionType()
    {
        return INTERACTION_TYPE.RESOURCE;
    }
    public override void buildingInteratction(Building b) { }
    public override void positionInteration(MapPos p) { }
    public override void unitInteraction(Unit u) { }
    public override void resourceInteraction(Resource r) { }
}
