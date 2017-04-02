using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitGroupController : MonoBehaviour, Interacts
{

    UnitGroup group;

    void Awake()
    {
        group = GetComponent<UnitGroup>();

    }
    public void add(UnitController u) {
        group.units.Add(u);
    }
    public void remove(UnitController u) {
        group.units.Remove(u);
    }
    public bool isEmpty() {
      return group.isEmpty();
        
    }
    public void interactWith(Interactable i)
    {
        foreach (UnitController u in group.units)
        {
            if(u.getGroup()!=this)
                setUnitGroup(u);
        }
        switch (i.getInteractionType()) {
            case INTERACTION_TYPE.BUILDING:
                break;
            case INTERACTION_TYPE.POSITION:
                moveGroupToPosition((MapPos)i);
                break;
            case INTERACTION_TYPE.UNIT:
                break;
            case INTERACTION_TYPE.RESOURCE:
                break;
        }

    }
    private void setUnitGroup(UnitController u) {
        u.transform.parent = transform;
        u.setGroup(this);
    }

    public void removeUnit(UnitController u) {
        remove(u);
		if (isEmpty())
		{
			Destroy(this.gameObject);
		}
    }

    private void moveGroupToPosition(MapPos m) {
        group.moveTo(m);
    }
}
