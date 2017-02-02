using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;


[RequireComponent(typeof(NavMeshAgent))]
public class BasicAgentNavigation : MonoBehaviour {

    Transform target;
    private Vector3 currentDestination;
    GameObject clickEffect;
    NavMeshAgent agent;

    void Start() {
        agent = GetComponent<NavMeshAgent>();
    }
	void Update () {
        if (target.position != currentDestination) {
            setDestination();
        }
	}
    public void setTarget(Transform t) {
        target = t;
        setDestination();
    }
    void setDestination() {
        currentDestination = target.position;
        agent.SetDestination(currentDestination);
    }


}
