using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;
public class UnitGroup {

    List<Unit> units;
    NavMeshAgent agent;

    Interactable interaction;

    void Awake() {
        units = new List<Unit>();
       // agent = <NavMeshAgent>();
    }

    public void addUnit(Unit unit) {
        units.Add(unit);
    }

    public bool isEmpty() {
        return units.Count == 0;
    }
   


}
