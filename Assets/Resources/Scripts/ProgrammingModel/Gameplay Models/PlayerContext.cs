using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;


public class PlayerContext : MonoBehaviour {
	public GameObject armyGroupPrefab;

    public bool debugAiStats;
	public bool fogOfWar = true;
	public bool explored = false;

    public Transform[] buildingTransforms;
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

	public int minPopForAttack;

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

			for (int j = 0; j < updatedPrefabs.unitPrefabs [i].transform.childCount; j++)
				if (updatedPrefabs.unitPrefabs [i].transform.GetChild (j).gameObject.layer == LayerMask.NameToLayer ("MiniMapObject")) {
//					updatedPrefabs.unitPrefabs [i].transform.GetChild (j).GetComponent<RTSObject> ().getModel().material.SetColor ("_Color", playerColor);
					break;
				}
		}
			
		for (int i = 0; i < originalPrefabs.buildingPrefabs.Length; i++) {
			updatedPrefabs.buildingPrefabs [i] = Instantiate(originalPrefabs.buildingPrefabs [i], transform);
			updatedPrefabs.buildingPrefabs [i].GetComponent<Building> ().player = this;
			updatedPrefabs.buildingPrefabs [i].GetComponent<RTSObject> ().getModel().enabled = false;
			updatedPrefabs.buildingPrefabs [i].GetComponent<RTSObject> ().getModel().material.SetColor ("_Color", playerColor);

			for (int j = 0; j < updatedPrefabs.buildingPrefabs [i].transform.childCount; j++)
				if (updatedPrefabs.buildingPrefabs [i].transform.GetChild (j).gameObject.layer == LayerMask.NameToLayer ("MiniMapObject")) {
//					updatedPrefabs.buildingPrefabs [i].transform.GetChild (j).GetComponent<RTSObject> ().getModel().material.SetColor ("_Color", playerColor * 0.2f);
					break;
				}
		}

		GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag ("SpawnPoint");

		int index = Random.Range (0, spawnPoints.Length);

		GameObject townCenter = null;

		for (int i = 0; i < updatedPrefabs.buildingPrefabs.Length; i++)
			if (updatedPrefabs.buildingPrefabs [i].tag == "TownCenter") {
				townCenter = updatedPrefabs.buildingPrefabs [i];
				break;
			}


		if (townCenter != null) {
			float yOffset = townCenter.GetComponent<IndustrialCenter> ().getModel ().bounds.size.y / 2;
			industrialCenter = townCenter.GetComponent<RTSObject>().InstantiatePlayableObject (new Vector3(spawnPoints[index].transform.position.x, yOffset, spawnPoints[index].transform.position.z), transform).GetComponent<IndustrialCenter>();
			industrialCenter.buildings = updatedPrefabs.buildingPrefabs;
			industrialCenter.SetToAwake ();
			DestroyImmediate (spawnPoints [index].gameObject);
		}
	}

	// Update is called once per frame
	void Update () {
		// Update

		// SpawnRandomUnits ();

		strategy.RealizeStrategy ();

        if (Input.GetKeyDown(KeyCode.P)) {
            glueQuantity += 100000;
            paperQuantity += 100000;
        }

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
			playerColor = new Color (0.3f, 0.3f, 1.0f, 1.0f);
			break;
		case 1:
			playerColor = new Color (1.0f, 0.3f, 0.3f, 1.0f);
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
