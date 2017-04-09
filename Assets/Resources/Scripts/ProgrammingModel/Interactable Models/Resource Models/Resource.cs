using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : Interactable {
	
	public int quantity;
	public int startingMinQuantity;
	public int startingMaxQuantity;

	public string type;

	// Use this for initialization
	void Start () {
		type = gameObject.tag;
		quantity = Random.Range (startingMinQuantity, startingMaxQuantity + 1);
	}
	
	// Update is called once per frame
	void Update () {
		if (quantity == 0) {
			Destroy (this.gameObject);
		}
	}

	public virtual void GetQuantity(int q)
	{
		quantity -= q;
	}
    public override INTERACTION_TYPE getInteractionType()
    {
        return INTERACTION_TYPE.RESOURCE;
    }
    public override void buildingInteraction(Building b) { }
    public override void positionInteraction(MapPos p) { }
    public override void unitInteraction(Unit u) { }
    public override void resourceInteraction(Resource r) { }
}
