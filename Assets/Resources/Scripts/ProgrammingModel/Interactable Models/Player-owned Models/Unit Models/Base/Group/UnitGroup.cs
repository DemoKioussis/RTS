using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;
public class UnitGroup :MonoBehaviour{

    public List<Unit> units;
    NavMeshAgent agent;

    Interactable interaction;

    void Awake() {
        units = new List<Unit>();
        agent = GetComponent<NavMeshAgent>();
    }

    public void setTargetPosition(Vector3 p) {

    }

    public bool isEmpty() {
        return units.Count == 0;
    }
   


}
