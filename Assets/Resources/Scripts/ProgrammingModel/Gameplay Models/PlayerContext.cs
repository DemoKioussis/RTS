using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerContext : MonoBehaviour {
	public bool fogOfWar = true;
	public bool explored = false;


	public int playerId;
	public int teamId;

	public PrefabDatabase updatedPrefabs;

	public PlayerMap playerMap;
	public Strategy strategy;

	public int glueQuantity;
	public int paperQuantity;

	public Unit[] activeUnits;
	public Building[] activeBuildings;

	// Update is called once per frame
	void Update () {

	}
}
