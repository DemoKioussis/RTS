using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;
public class UnitGroup :MonoBehaviour{

    public List<Unit> units;
    public float groupMovementSpeed;
    NavMeshAgent agent;

    Interactable interaction;
    Vector3 targetPosition;
    bool hasAction;


    void Awake() {
        units = new List<Unit>();
        agent = GetComponent<NavMeshAgent>();
    }

    void Update() {
        if (hasAction) {
			transform.position = Vector3.MoveTowards (transform.position, targetPosition, groupMovementSpeed * Time.deltaTime);
        }
    }
    public void moveTo(MapPos p) {
        hasAction = true;
        targetPosition = p.getPosition();
    }

    public bool isEmpty() {
        return units.Count == 0;
    }
   


}
