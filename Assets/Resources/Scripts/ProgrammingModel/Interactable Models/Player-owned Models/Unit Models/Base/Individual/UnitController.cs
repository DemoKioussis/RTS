using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frameusing System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using UnityEngine.AI;
	[RequireComponent(typeof(NavMeshAgent))]
	[RequireComponent(typeof(Unit))]
	public class UnitController : MonoBehaviour {

		public float arriveDistance;
		private NavMeshAgent agent;
		private Unit unit;
		private UnitGroupController group;
		void Awake() {
			agent = GetComponent<NavMeshAgent>();
			unit = GetComponent<Unit>();
		}

		public void setGroup(UnitGroupController g)
		{
			if (group != null)
			{
				group.removeUnit(this);
			}
			group = g;
		}
		public UnitGroupController getGroup()
		{
			return group;
		}
		public void moveTo(Vector3 p) {
			agent.SetDestination(p);
		}
		public void setPath(NavMeshPath p) {
			agent.SetPath(p);
		}
		public void setDestination(Vector3 p)
		{
			agent.SetDestination(p);
		}
		public void stopMovement() {
			agent.Stop();
			agent.ResetPath();
		}
		public float distanceTo(Vector3 p) {
			return Vector3.Distance(p, transform.position);
		}
		public float getStoppingDistance() {
			return agent.stoppingDistance;
		}
		public void setStoppingDistance(float s)
		{
			agent.stoppingDistance = s;
		}
		public float getRadius() {
			return agent.radius;
		}
	}

	void Update () {
		
	}
}
