using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : Interactable {
	public ResourceBuilding building;
	public ResourceStats stats;

	public GameObject selectionCircle;

	public int startingMinQuantity;
	public int startingMaxQuantity;

	public string type;

	private int startingQuantity;

	// Use this for initialization
	void Start () {
		type = gameObject.tag;
		stats.quantity = Random.Range (startingMinQuantity, startingMaxQuantity + 1);
		startingQuantity = stats.quantity;
		stats.type = type;
	}

	public MeshRenderer getModel(){
		return GetComponent<MeshRenderer> ();
	}

	public virtual void GetQuantity(int q)
	{
		stats.quantity -= q;
		// decrease the scale at every 10 number
		if (stats.quantity % 10 == 0) {
			transform.localScale = transform.localScale * ((float)stats.quantity / startingQuantity); 
		}

		if (stats.quantity == 0) {
			building.Destroy ();
			GameContext.currentGameContext.activeResources.Remove (this);
			Destroy (this.gameObject);
		}
	}

	public override Stats GetStats ()
	{
		return stats;
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
