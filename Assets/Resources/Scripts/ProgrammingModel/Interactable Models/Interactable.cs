using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum INTERACTION_TYPE { POSITION, UNIT, BUILDING, RESOURCE,GROUP }

public abstract class Interactable : MonoBehaviour {

    public int teamId;

    public abstract INTERACTION_TYPE getInteractionType();

    public void InteractWith(Interactable i)
    {
   
        switch (i.getInteractionType())
        {
            case INTERACTION_TYPE.BUILDING:
                buildingInteraction((Building)i);
                break;
			case INTERACTION_TYPE.POSITION:
			{
				positionInteraction ((MapPos)i);
				break;
			}
            case INTERACTION_TYPE.UNIT:
                unitInteraction((Unit)i);
                break;
            case INTERACTION_TYPE.RESOURCE:
                resourceInteraction((Resource)i);
                break;
        }
    }
    public abstract void buildingInteraction(Building b);
    public abstract void positionInteraction(MapPos p);
    public abstract void unitInteraction(Unit u);
    public abstract void resourceInteraction(Resource r);

}
