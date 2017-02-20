using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContext : MonoBehaviour {
	public int teamNumber;

	public PrefabDatabase updatedPrefabs;

	public PlayerMap playerMap;
	public Strategy strategy;

	public Unit[] activeUnits;
	public Building[] activeBuildings;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
