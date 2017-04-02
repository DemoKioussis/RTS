using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitGroupController : MonoBehaviour,Interacts
{

    UnitGroup group;
    void Awake() {
        group = new UnitGroup();
    }


    public bool isEmpty() {
        // reuturn group.isEmpty();
        return false;
    }
    public void interactWith(Interactable i)
    {

    }
}
