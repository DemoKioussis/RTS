using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerContext : MonoBehaviour {
    public bool debugAiStats;
	public bool fogOfWar = true;
	public bool explored = false;

	public int playerId;
	public int teamId;
	public Color playerColor;

	public PrefabDatabase updatedPrefabs;

	public PlayerMap playerMap;
	public GameObject inputManager;
	public GameObject selectionManager;
	public GameObject UIManager;
	public Strategy strategy;

	public int glueQuantity;
	public int paperQuantity;
	public int population;
	public int populationLimit;

	public List<Unit> activeUnits = new List<Unit>();
	public List<Building> activeBuildings = new List<Building>();
	public IndustrialCenter industrialCenter = null;

	public float spawnProbability;

	void Start()
	{
		PrefabDatabase originalPrefabs = GameContext.currentGameContext.prefabs;

		updatedPrefabs = new PrefabDatabase ();
		updatedPrefabs.unitPrefabs = new GameObject[originalPrefabs.unitPrefabs.Length];
		updatedPrefabs.buildingPrefabs = new GameObject[originalPrefabs.buildingPrefabs.Length];

		for (int i = 0; i < originalPrefabs.unitPrefabs.Length; i++) {
			updatedPrefabs.unitPrefabs [i] = Instantiate(originalPrefabs.unitPrefabs [i], transform);
			updatedPrefabs.unitPrefabs [i].GetComponent<Unit> ().player = this;
			updatedPrefabs.unitPrefabs [i].GetComponent<RTSObject> ().getModel().enabled = false;
			updatedPrefabs.unitPrefabs [i].GetComponent<RTSObject> ().getModel().material.SetColor ("_Color", playerColor);
		}
			
		for (int i = 0; i < originalPrefabs.buildingPrefabs.Length; i++) {
			updatedPrefabs.buildingPrefabs [i] = Instantiate(originalPrefabs.buildingPrefabs [i], transform);
			updatedPrefabs.buildingPrefabs [i].GetComponent<Building> ().player = this;
			Debug.Log (updatedPrefabs.buildingPrefabs [i].GetComponent<RTSObject> ().getModel());
			updatedPrefabs.buildingPrefabs [i].GetComponent<RTSObject> ().getModel().enabled = false;
			updatedPrefabs.buildingPrefabs [i].GetComponent<RTSObject> ().getModel().material.SetColor ("_Color", playerColor);
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

		if (townCenter != null) {
			industrialCenter = townCenter.GetComponent<RTSObject>().InstantiatePlayableObject (new Vector3(spawnPoints[index].x, 0, spawnPoints[index].z), transform).GetComponent<IndustrialCenter>();
			industrialCenter.buildings = updatedPrefabs.buildingPrefabs;
			industrialCenter.SetToAwake ();
		}
	}

	// Update is called once per frame
	void Update () {
		// GameLost ();

		// Update

		// SpawnRandomUnits ();

		strategy.RealizeStrategy ();
	}

	public void Init(int playerId, int teamId, bool isAI, bool fogOfWar, bool explored)
	{
		this.playerId = playerId;
		this.teamId = teamId;
		this.fogOfWar = fogOfWar;
		this.explored = explored;

		if (isAI)
			strategy = new AIStrategy (this);
		else {
			// is a player
			strategy = new PlayerStrategy (this);
			Instantiate (selectionManager, transform);
			Instantiate (inputManager, transform);
			Instantiate (UIManager, transform);
		}

		switch (this.playerId) {
		case 0:
			playerColor = new Color (0, 0, 255);
			break;
		case 1:
			playerColor = new Color (255, 0, 0);
			break;
		}
	}

	void GameLost()
	{
		// To do: Make sure that player is still playable. If not, destroy
	}

	public void Buy(RTSObject entity)
	{
		glueQuantity -= entity.stats.glueCost;
		paperQuantity -= entity.stats.paperCost;

		Unit unit = entity.GetComponent<Unit> ();
		if (unit != null)
			population += unit.unitStats.populationCost;
	}

	public void Sell(RTSObject entity)
	{
		if (entity != null) {
			glueQuantity += entity.stats.glueCost;
			paperQuantity += entity.stats.paperCost;
		}
	}

	public void TakeLosses(int pop)
	{
		population -= pop;
	}
}
