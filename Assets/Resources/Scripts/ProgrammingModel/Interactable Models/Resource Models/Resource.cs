using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : Interactable {
	public ResourceBuilding building;
	public ResourceStats stats;

	public GameObject resourceDepletedObject;

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
		// decrease the scale at every 100
		if (stats.quantity % 100 == 0) {
			transform.localScale = transform.localScale * ((float)stats.quantity / startingQuantity) + transform.localScale * (10/100) * startingQuantity; 
		}

		if (stats.quantity == 0) {
			building.Destroy ();
			GameContext.currentGameContext.activeResources.Remove (this);

			GameObject depletedObject = (GameObject) Instantiate (resourceDepletedObject, transform.position, transform.rotation, transform.parent);

			if (type == "Glue") {
				// glue is floating with y of 10.0f and local scale is a little different
				// set the rock lower
				depletedObject.transform.localScale = new Vector3 (1, 1, 1);
				float deltaY = transform.position.y;
				depletedObject.transform.position += new Vector3 (0, -deltaY, 0);
			}

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
