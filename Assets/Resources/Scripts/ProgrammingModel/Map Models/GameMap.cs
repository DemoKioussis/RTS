using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMap : MonoBehaviour {
	public bool fogOfWar;
	public GameObject fogOfWarPrefab;
	public int resolution;
	public Resource[] resources;
	LayerMask originalLayerMask;

	// Use this for initialization
	void Start () {
		resources = GetComponentsInChildren<Resource> ();
		GameContext.currentGameContext.activeResources = new List<Resource>(resources);

		Vector3 mapSize = GameObject.FindGameObjectWithTag ("MapPlane").GetComponent<Renderer> ().bounds.size;
		Vector3 newScale = mapSize / resolution;
		newScale = new Vector3 (newScale.x, 20, newScale.z);

		for(int j = 0; j < resolution; j++)
			for(int i = 0; i < resolution; i++)
			{
				Instantiate (fogOfWarPrefab, new Vector3(i * newScale.x - (mapSize.x - newScale.x) / 2, 0,j * newScale.z - (mapSize.z - newScale.z) / 2), Quaternion.identity).transform.localScale = newScale;
			}
	}
	
	// Update is called once per frame
	void Update () {
		if (Camera.current != null) {
			if (fogOfWar) {
				Camera.current.cullingMask += 1 << LayerMask.NameToLayer ("FogOfWar");
			} else {
				Camera.current.cullingMask -= 1 << LayerMask.NameToLayer ("FogOfWar");
			}
		}
	}
}
