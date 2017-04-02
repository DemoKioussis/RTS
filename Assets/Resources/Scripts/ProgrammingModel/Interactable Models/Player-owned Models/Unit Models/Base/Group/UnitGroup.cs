using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class UnitGroup : InterationManager {

    List<UnitMovementController> units;
    NavMeshAgent agent;

    Interactable interaction;

    void Awake() {
        units = new List<UnitMovementController>();
        agent = GetComponent<NavMeshAgent>();
    }

    public void addUnit(UnitMovementController unit) {
        units.Add(unit);
    }

    public void setInteraction() {

    }
    
}
