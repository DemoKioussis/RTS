using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPos : Interactable {

	MapStats stats;

    Vector3 position;
    public void setPosition(Vector3 p) {
        position = p;
    }
	public override Stats GetStats ()
	{
		return stats;
	}
    public override Vector3 getPosition()
    {
        return position;
    }
    public override INTERACTION_TYPE getInteractionType() {
        return INTERACTION_TYPE.POSITION;
    }
    public override void buildingInteraction(Building b) { }
    public override void positionInteraction(MapPos p) { }
    public override void unitInteraction(Unit u) { }
    public override void resourceInteraction(Resource r) { }

}
