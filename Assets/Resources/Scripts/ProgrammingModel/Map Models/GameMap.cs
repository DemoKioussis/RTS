using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMap : MonoBehaviour {
	public bool fogOfWar;

	public Resource[] resources;

	// Use this for initialization
	void Start () {
		resources = GetComponentsInChildren<Resource> ();
		GameContext.currentGameContext.activeResources = new List<Resource>(resources);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
