using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameContext : MonoBehaviour {
	static GameContext currentGameContext;

	// Game mode and gameplay variables go here

	public GameMap map;
	public PrefabDatabase prefabs;

	public Unit[] activeUnits;
	public Building[] activeBuildings;
	public Resource[] activeResources;

	// Use this for initialization
	void Start () {
		
	}
}
