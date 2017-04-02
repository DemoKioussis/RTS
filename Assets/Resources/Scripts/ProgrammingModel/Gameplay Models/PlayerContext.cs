using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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

	public List<Unit> activeUnits = new List<Unit>();
	public List<Building> activeBuildings = new List<Building>();

	void Start()
	{
		PrefabDatabase originalPrefabs = GameContext.currentGameContext.prefabs;

		updatedPrefabs = new PrefabDatabase ();
		updatedPrefabs.unitPrefabs = new GameObject[originalPrefabs.unitPrefabs.Length];
		updatedPrefabs.buildingPrefabs = new GameObject[originalPrefabs.buildingPrefabs.Length];

		for (int i = 0; i < originalPrefabs.unitPrefabs.Length; i++) {
			updatedPrefabs.unitPrefabs [i] = Instantiate(originalPrefabs.unitPrefabs [i], transform);
			updatedPrefabs.unitPrefabs [i].GetComponent<Renderer> ().enabled = false;
		}
			
		for (int i = 0; i < originalPrefabs.buildingPrefabs.Length; i++) {
			updatedPrefabs.buildingPrefabs [i] = Instantiate(originalPrefabs.buildingPrefabs [i], transform);
			updatedPrefabs.buildingPrefabs [i].GetComponent<Renderer> ().enabled = false;
		}

		List<Vector3> spawnPoints = new List<Vector3> ();

		for(int i = 0; i < GameContext.currentGameContext.map.transform.childCount; i++)
		{
			Transform t = GameContext.currentGameContext.map.transform.GetChild (i);
			if (t.name == "SpawnPoint")
				spawnPoints.Add (t.position);
		}

		int index = Random.Range (0, spawnPoints.Count);

		GameObject townCenter = null;

		for (int i = 0; i < updatedPrefabs.buildingPrefabs.Length; i++)
			if (updatedPrefabs.buildingPrefabs [i].tag == "TownCenter") {
				townCenter = updatedPrefabs.buildingPrefabs [i];
				break;
			}

		Debug.Log (spawnPoints);

		if (townCenter != null) {
			Instantiate (townCenter, spawnPoints[index], Quaternion.identity, transform);
		}
	}

	// Update is called once per frame
	void Update () {

	}

	GameObject InstantiatePlayableObject(GameObject playableObject)
	{
		GameObject output = Instantiate (playableObject, transform);
		output.GetComponent<RTSObject> ().ReplaceStatsReferences (playableObject.GetComponent<RTSObject> ());
		return output;
	}
}
