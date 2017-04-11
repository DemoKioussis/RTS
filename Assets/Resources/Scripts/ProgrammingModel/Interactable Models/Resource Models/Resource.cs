using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : Interactable {
	public ResourceBuilding building;
	public int quantity;
	public int startingMinQuantity;
	public int startingMaxQuantity;

	public string type;

	private int startingQuantity;

	// Use this for initialization
	void Start () {
		type = gameObject.tag;
		quantity = Random.Range (startingMinQuantity, startingMaxQuantity + 1);
		startingQuantity = quantity;
	}

	public virtual void GetQuantity(int q)
	{
		quantity -= q;
		// decrease the scale at every 10 number
		if (quantity % 10 == 0) {
			transform.localScale = transform.localScale * ((float)quantity / startingQuantity); 
		}

		if (quantity == 0) {
			building.Destroy ();
			GameContext.currentGameContext.activeResources.Remove (this);
			Destroy (this.gameObject);
		}
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
