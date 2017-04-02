using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameContext : MonoBehaviour {

	public static GameContext currentGameContext;

	public int playerCount;
	public GameObject playerPrefab;

	List<PlayerContext> allPlayers = new List<PlayerContext>();
	List<PlayerContext> activePlayers = new List<PlayerContext> ();

	// Game mode and gameplay variables go here
	public GameObject mapPrefab;
	public PrefabDatabase prefabs;

	public Unit[] activeUnits;
	public Building[] activeBuildings;
	public Resource[] activeResources;

	public GameMap map;
	public List<Vector3> initialPlayerPositions = new List<Vector3> ();
	// Use this for initialization
	void Start () {

		currentGameContext = this;

		Instantiate (mapPrefab, transform);

		map = GetComponentInChildren<GameMap> ();

		AcquirePrefabs ();

		for (int i = 0; i < playerCount; i++) {
			allPlayers.Add (Instantiate (playerPrefab).GetComponent<PlayerContext>());
			activePlayers.Add (allPlayers [allPlayers.Count - 1]);
		}
//		SetSpawnables ();
	}

	void AcquirePrefabs()
	{
		Object[] unitP = Resources.LoadAll ("Prefabs/Units", typeof(GameObject));
		prefabs.unitPrefabs = new GameObject[unitP.Length];

		for (int i = 0; i < unitP.Length; i++)
			prefabs.unitPrefabs [i] = unitP[i] as GameObject;

		Object[] buildingP = Resources.LoadAll ("Prefabs/Buildings", typeof(GameObject));
		prefabs.buildingPrefabs = new GameObject[buildingP.Length];

		for (int i = 0; i < buildingP.Length; i++)
			prefabs.buildingPrefabs [i] = buildingP[i] as GameObject;

		Object[] resourceP = Resources.LoadAll ("Prefabs/GameResources", typeof(GameObject));
		prefabs.resourcePrefabs = new GameObject[resourceP.Length];

		for (int i = 0; i < resourceP.Length; i++)
			prefabs.resourcePrefabs [i] = resourceP[i] as GameObject;
	}

/*	void SetSpawnables()
	{
		GameObject[] unitPrefabs = prefabs.unitPrefabs;

		for(int i = 0; i < unitPrefabs.Length; i++)
			spawnPrefabs.Add(unitPrefabs[i]);
	}*/
}
