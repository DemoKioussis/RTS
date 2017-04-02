using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;
public class UnitGroup : Interacts {

    List<UnitMovementController> units;
    NavMeshAgent agent;

    Interactable interaction;

    void Awake() {
        units = new List<UnitMovementController>();
       // agent = <NavMeshAgent>();
    }

    public void addUnit(UnitMovementController unit) {
        units.Add(unit);
    }

    public void interactWith(Interactable i) {

    }


}
