using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingBuildingGroup : MonoBehaviour{

	public List<TrainingBuildingController> buildings;
	public float groupMovementSpeed;

	Interactable interaction;
	Vector3 targetPosition;


	void Awake() {
		buildings = new List<UnitController>();
	}


	public void SetSpawnPointAs(MapPos p) {
		//  agent.SetPath(p);
		foreach (TrainingBuildingController u in buildings) {
			u.SetSpawnPointAs(p.getPosition());
		}
	}

	public bool isEmpty() {
		return buildings.Count == 0;
	}
}
