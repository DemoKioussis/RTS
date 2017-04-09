using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMap : MonoBehaviour {

	public Resource[] resources;

	// Use this for initialization
	void Start () {
		resources = GetComponentsInChildren<Resource> ();
		GameContext.currentGameContext.activeResources = resources;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
