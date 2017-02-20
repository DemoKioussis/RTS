using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stub : MonoBehaviour {
	public GameObject referencedObjectPrefab;
	public Building building;
	// in seconds
	public float unlockTime;

	float unlockRate;
	float progress = 0;
	// Use this for initialization
	void Start () {
		unlockRate = 1.0f / unlockTime;
	}
	
	// Update is called once per frame
	void Update () {
		progress += unlockRate * Time.deltaTime;

		if (progress >= 1.0f) {
			if (referencedObjectPrefab.GetComponent<Unit> () != null) {
				building.GetComponent<TrainingBuilding> ().SpawnUnit (referencedObjectPrefab.GetComponent<Unit>());
			} else if (referencedObjectPrefab.GetComponent<Upgrade> () != null) {
				Instantiate (referencedObjectPrefab);
			}
		}
	}
}
