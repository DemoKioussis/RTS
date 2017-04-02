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
    public void add(Unit u) {
        group.units.Add(u);
    }
    public void remove(Unit u) {
        group.units.Remove(u);
    }
    public bool isEmpty() {
      return group.isEmpty();
        
    }
    public void interactWith(Interactable i)
    {
        foreach(Unit u in group.units)
            u.transform.parent = transform;

        switch (i.getInterationType()) {
            case INTERACTION_TYPE.BUILDING:
                break;
            case INTERACTION_TYPE.POSITION:
                group.setTargetPosition(((MapPos)i).getPosition());
                break;
            case INTERACTION_TYPE.UNIT:
                break;
            case INTERACTION_TYPE.RESOURCE:
                break;
        }

    }
}
