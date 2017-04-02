using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingGroup : MonoBehaviour {

	public List<BuildingController> buildings;
	Interactable interaction;
	Vector3 targetPosition;


	void Awake() {
		buildings = new List<BuildingController>();
	}

	public void SetSpawnTo(MapPos p) {
		foreach (BuildingController u in buildings) {
			u.SetSpawnTo(p);
		}
	}

	public bool isEmpty() {
		return buildings.Count == 0;
	}
}
