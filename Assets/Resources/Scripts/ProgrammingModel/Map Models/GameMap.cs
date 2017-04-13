using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMap : MonoBehaviour {
	public bool fogOfWar;
	bool previousFogOfWar;
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
                GameObject t = Instantiate(fogOfWarPrefab, new Vector3(i * newScale.x - (mapSize.x - newScale.x) / 2, 0, j * newScale.z - (mapSize.z - newScale.z) / 2), Quaternion.identity);
                t.transform.localScale = newScale;
                t.transform.parent = transform;
            }

		previousFogOfWar = fogOfWar;
	}
	
	// Update is called once per frame
	void Update () {
		if (previousFogOfWar != fogOfWar) {
			GameObject mainCamObject = GameObject.FindGameObjectWithTag ("MainCamera");
			if (mainCamObject != null) {
				Camera mainCam = mainCamObject.GetComponent<Camera> ();
				if (mainCam != null) {
					mainCam.cullingMask ^= (1 << LayerMask.NameToLayer ("FogOfWar"));
				}
			}

			GameObject miniMapCamObject = GameObject.FindGameObjectWithTag ("MiniMapCamera");
			if (miniMapCamObject != null) {
				Camera miniMapCam = miniMapCamObject.GetComponent<Camera> ();
				if (miniMapCam != null) {
					miniMapCam.cullingMask ^= (1 << LayerMask.NameToLayer ("FogOfWar"));
				}
			}

			previousFogOfWar = fogOfWar;
		}
	}
}
