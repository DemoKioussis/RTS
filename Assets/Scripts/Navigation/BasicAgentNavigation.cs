using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;


[RequireComponent(typeof(NavMeshAgent))]
public class BasicAgentNavigation : MonoBehaviour {

    SmartTarget target;
    private Vector3 currentDestination;
    GameObject clickEffect;
    NavMeshAgent agent;

    void Start() {
        agent = GetComponent<NavMeshAgent>();
    }
	void Update () {
        if (target!=null && target.getPosition() != currentDestination) {
            setDestination();
        }
	}
    public void setTarget(SmartTarget t) {
        if (target != null)
        {
            target.removeFollower();
        }
        target = t;
        t.addFollower();
        setDestination();
    }
    void setDestination() {
        currentDestination = target.getPosition();
        agent.SetDestination(currentDestination);
    }


}
