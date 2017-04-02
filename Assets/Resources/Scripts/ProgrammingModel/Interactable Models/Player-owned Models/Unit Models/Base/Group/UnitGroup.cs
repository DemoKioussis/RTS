﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;
public class UnitGroup :MonoBehaviour{

    public List<UnitController> units;
    public float groupMovementSpeed;
    NavMeshAgent agent;

    Interactable interaction;
    Vector3 targetPosition;
    bool hasAction;


    void Awake() {
        units = new List<UnitController>();
        agent = GetComponent<NavMeshAgent>();
    }

  
    public void moveTo(MapPos p) {
        hasAction = true;
        targetPosition = p.getPosition();

        foreach (UnitController u in units) {
            u.moveTo(p.getPosition());
        }
    }

    public bool isEmpty() {
        return units.Count == 0;
    }
   


}
