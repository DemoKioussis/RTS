using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ExploredBuilding {
	Building reference;
	MeshFilter meshFilter;
}

public class PlayerMap : MonoBehaviour {
	List<ExploredBuilding> exploredBuildings = new List<ExploredBuilding>();
	List<int> exploredPositionIndices = new List<int>();

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
