using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIStrategy : Strategy {
	public enum Activity {MAKEPAPER, MAKEGLUE, MAKESHORTRANGE, MAKELONGRANGE};
	int paperQuantityInStock;
	int glueQuantityInStock;
	int shortRangeUnits;
	int longRangeUnits;
	int shortRangeBuildings;
	int longRangeBuildings;
	float paperGatheringRate;
	float glueGatheringRate;
	int population;
	int populationLimit;
	float dangerIndex;

	UnitGroup army = null;

	public class WeighedTask
	{
		public Activity activity;
		public float value;

		public WeighedTask(Activity a, float v)
		{
			activity = a;
			value = v;
		}
	}

	List<WeighedTask> tasks = new List<WeighedTask> ();

	public AIStrategy(PlayerContext player)
	{
		this.player = player;
		int numVertices = GameContext.currentGameContext.map.GetComponentInChildren<MeshFilter> ().mesh.vertices.Length;
		GameContext.currentGameContext.map.GetComponentInChildren<MeshFilter> ().mesh.colors = new Color[numVertices];

		tasks.Add (new WeighedTask (Activity.MAKEPAPER, 1.0f));
		tasks.Add (new WeighedTask (Activity.MAKEGLUE, 1.0f));
		tasks.Add (new WeighedTask (Activity.MAKESHORTRANGE, 1.0f));
		tasks.Add (new WeighedTask (Activity.MAKELONGRANGE, 1.0f));
	}

	public override void RealizeStrategy()
	{
		UpdateInfluenceMap ();

		UpdateStateVariables ();

		UpdateTasks ();

		for (int i = 0; i < tasks.Count; i++) {
			bool wasFulfilled = true;
			if (tasks [i].activity == Activity.MAKEGLUE) {
				wasFulfilled = ManageResource ("Glue");
			} else if (tasks [i].activity == Activity.MAKEPAPER) {
				wasFulfilled = ManageResource ("Paper");
			} else if (tasks [i].activity == Activity.MAKESHORTRANGE) {
				wasFulfilled = ManageShortRange (tasks[i].value);
			} else if (tasks [i].activity == Activity.MAKELONGRANGE) {
				wasFulfilled = ManageLongRange (tasks[i].value);
			}

			if (!wasFulfilled)
				break;
		}

		MilitaryManagement ();
	}

	void UpdateInfluenceMap()
	{
		GameContext game = GameContext.currentGameContext;
		GameMap map = game.map;
		Vector3[] vertices = map.GetComponentInChildren<MeshFilter> ().mesh.vertices;
		Color[] colors = new Color[vertices.Length];

		for (int i = 0; i < vertices.Length; i++) {
			float magnitude = 0.0f;
			for (int j = 0; j < game.activePlayers.Count; j++) {
				if (j != player.playerId) {
					PlayerContext p = game.activePlayers [j];
					for (int k = 0; k < p.activeUnits.Count; k++)
                        if(p.activeUnits[k]!=null) // i added this because p.activeUnits[k] becomes null for dead units 
                            magnitude += p.activeUnits [k].Influence() / (1 + (vertices [i] - p.activeUnits [k].transform.position).sqrMagnitude);
                    for (int k = 0; k < p.activeBuildings.Count; k++)
                        if (p.activeBuildings[k] != null)  // i added this because p.activeBuilings[k] becomes null for dead building 
						    magnitude += p.activeBuildings [k].Influence() / (1 + (vertices [i] - p.activeBuildings [k].transform.position).sqrMagnitude);
				}
			}
			colors [i] = new Color (1.0f, 1.0f, 1.0f) * magnitude / 50.0f;
		}

		GameContext.currentGameContext.map.GetComponentInChildren<MeshFilter> ().mesh.colors = colors;

		float overallLocalInfluence = 0;

		for (int i = 0; i < vertices.Length; i++) {
			for (int k = 0; k < player.activeUnits.Count; k++)
				overallLocalInfluence += player.activeUnits [k].Influence() / (1 + (vertices [i] - player.activeUnits [k].transform.position).sqrMagnitude);
			for (int k = 0; k < player.activeBuildings.Count; k++)
				overallLocalInfluence += player.activeBuildings [k].Influence() / (1 + (vertices [i] - player.activeBuildings [k].transform.position).sqrMagnitude);
		}

		overallLocalInfluence /= 50.0f;

		dangerIndex = 1.0f / (overallLocalInfluence + 1.0f);
	}

	float PaperResourceHeuristic()
	{
		return 101.0f / (Mathf.Pow(paperGatheringRate * 10, 2) + paperQuantityInStock + 1);
	}

	float GlueResourceHeuristic ()
	{
		return 100.0f / (Mathf.Pow(glueGatheringRate * 10, 2) + paperQuantityInStock + 1);
	}

	float ShortRangeHeuristic()
	{
		return (2.0f * (float) (longRangeUnits + 1) / (float) (shortRangeUnits + 1)) / (population * 2 + Mathf.Pow(shortRangeBuildings, 2) + 1) + dangerIndex * 0.1f;
	}

	float LongRangeHeuristic()
	{
		return (1.0f * (float) (shortRangeUnits + 1) / (float) (longRangeUnits + 1)) / (population * 2 + Mathf.Pow(longRangeBuildings, 2) + 1) + dangerIndex * 0.1f;
	}

	void UpdateTasks()
	{
		string stats = "STATS:\n";

		for (int i = 0; i < tasks.Count; i++) {
			
			if (tasks [i].activity == Activity.MAKEPAPER) {
				tasks [i].value = PaperResourceHeuristic ();
				stats += "Paper: ";
			}
			else if (tasks [i].activity == Activity.MAKEGLUE) {
				tasks [i].value = GlueResourceHeuristic ();
				stats += "Glue: ";
			}
			else if (tasks [i].activity == Activity.MAKESHORTRANGE) {
				tasks [i].value = ShortRangeHeuristic ();
				stats += "ShortRange: ";
			}
			else if (tasks [i].activity == Activity.MAKELONGRANGE) {
				tasks [i].value = LongRangeHeuristic ();
				stats += "LongRange: ";
			}

			stats += tasks [i].value + " ";
		}
        if (player.debugAiStats) 
		    Debug.Log (stats);

		tasks.Sort (SortByValue);
	}

	void UpdateStateVariables()
	{
		paperQuantityInStock = player.paperQuantity;
		glueQuantityInStock = player.glueQuantity;
		population = player.population;
		paperGatheringRate = 0;
		glueGatheringRate = 0;

		for (int i = 0; i < player.activeBuildings.Count; i++) {
			if (player.activeBuildings [i].awake) {
				if (player.activeBuildings [i] is ResourceBuilding) {
					ResourceBuilding rB = player.activeBuildings [i] as ResourceBuilding;
					if (rB.resource != null) {
						float rate = 1.0f / rB.resourceBldgStats.gatheringTime;
						if (rB.resource.gameObject.tag == "Paper") {
							paperGatheringRate += rate;
						} else if (rB.resource.gameObject.tag == "Glue") {
							glueGatheringRate += rate;
						}
					}

				} else if (player.activeBuildings [i] is TrainingBuilding) {
					player.activeBuildings [i].awake = false;
					Unit unit = (player.activeBuildings [i] as TrainingBuilding).unit.GetComponent<Unit>();
					if (unit != null) {
						paperGatheringRate -= (float)unit.stats.paperCost / unit.unitStats.trainingTime;
						glueGatheringRate -= (float)unit.stats.glueCost / unit.unitStats.trainingTime;
					}
				}
			}
		}

		shortRangeUnits = 0;
		longRangeUnits = 0;

		for (int i = 0; i < player.activeUnits.Count; i++) {
			if (player.activeUnits [i].tag == "ShortRangeUnit")
				shortRangeUnits++;
			else if (player.activeUnits [i].tag == "LongRangeUnit")
				longRangeUnits++;
		}

		shortRangeBuildings = 0;
		longRangeBuildings = 0;

		for (int i = 0; i < player.activeBuildings.Count; i++) {
			if (player.activeBuildings [i].tag == "ShortRangeBuilding")
				shortRangeBuildings++;
			else if (player.activeBuildings [i].tag == "LongRangeBuilding")
				longRangeBuildings++;
		}

	}

	Resource FindClosestResource(string resourceType)
	{
		List<Resource> resources = GameContext.currentGameContext.activeResources;
		Resource closestResource = null;
		float minDistance = 100000000000000;

		for (int i = 0; i < resources.Count; i++) {
			if (resources [i].gameObject.tag == resourceType && resources[i].building == null) {
				float distance = (resources [i].transform.position - player.industrialCenter.transform.position).magnitude;
				if (distance < minDistance) {
					minDistance = distance;
					closestResource = resources [i];
				}
			}
		}
			
		return closestResource;
	}

	bool ManageResource(string resType)
	{
		// return the Resource, it would be easier for you because I set up a function to set the resource from the building
		Resource resource = FindClosestResource (resType);

		if (resource != null) {
			ResourceBuilding rB = MakeNewBuilding<ResourceBuilding> (new Vector3(resource.transform.position.x, resource.getModel().bounds.size.y /2, resource.transform.position.z));
			if (rB != null) {
				rB.AssociateToResource (resource);
				return true;
			} else
				return false;
		} else
			return false;
	}

	bool ManageShortRange(float importance)
	{
		bool setSomething = false;
		bool buyBuilding = true;
		for (int i = 0; i < player.activeBuildings.Count; i++) {
			if(!player.activeBuildings[i].awake)
			{
				if(player.activeBuildings [i].tag == "ShortRangeBuilding")
				{
					TrainingBuilding b = player.activeBuildings [i].GetComponent<TrainingBuilding> ();
					ShortRangeUnit unit = b.unit.GetComponent<ShortRangeUnit>();

					if (b.progress <= 95 || (b.progress > 95 && b.progress < 105 && unit.CheckCost ())) {
						if (unit != null) {
							b.SetToAwake ();
							setSomething = true;
						}
					} else
						buyBuilding = false;
				}
			}
		}

		if (buyBuilding) {
			if (importance / (shortRangeBuildings + 1) > 0.05f)
				return MakeNewTrainingBuilding ("ShortRangeBuilding");
			else if (importance < 0.3f)
				return false;
			else
				return setSomething;
		} else
			return false;
	}

	bool ManageLongRange(float importance)
	{
		bool setSomething = false;
		bool buyBuilding = true;
		for (int i = 0; i < player.activeBuildings.Count; i++) {
			if(!player.activeBuildings[i].awake)
			{
				if(player.activeBuildings [i].tag == "LongRangeBuilding")
				{
					TrainingBuilding b = player.activeBuildings [i].GetComponent<TrainingBuilding> ();
					LongRangeUnit unit = b.unit.GetComponent<LongRangeUnit>();

					if (b.progress <= 95 || (b.progress > 95 && b.progress < 105 && unit.CheckCost ())) {
						if (unit != null) {
							b.SetToAwake ();
							setSomething = true;
						}
					} else
						buyBuilding = false;
				}
			}
		}

		if (buyBuilding) {
			if (importance / (shortRangeBuildings + 1) > 0.05f)
				return MakeNewTrainingBuilding ("LongRangeBuilding");
			else if (importance < 0.3f)
				return false;
			else
				return setSomething;
		} else
			return false;
	}

	Vector3 GetEmptyArea(Bounds bounds)
	{
		Vector3 dimensions = bounds.size;
		float maxDimension = Mathf.Max (dimensions.x, dimensions.z);

		int resolution = 10;

		float angleDiff = 2 * Mathf.PI / resolution;

		Bounds b = GameObject.FindGameObjectWithTag("MapPlane").GetComponent<Renderer>().bounds;

		List<Building> buildings = player.activeBuildings;

		buildings.Sort (SortByDistanceToTC);

		for(int j = 0; j < buildings.Count; j++)
			for (int i = 0; i < resolution; i++) {
				Vector3 bldg = buildings[j].getModel().bounds.size;
				float initialRadius = Mathf.Max (bldg.x, bldg.z);
				Vector3 pos = (buildings[j].transform.position + new Vector3(Mathf.Cos(angleDiff * i), 0, Mathf.Sin(angleDiff * i)) * initialRadius * j);
				if (pos.x > b.min.x && pos.x < b.max.x && pos.z > b.min.z && pos.z < b.max.z) {
					Collider[] c = Physics.OverlapBox (pos, bldg);
					if (c != null) {
						bool validPos = true;
						for (int k = 0; k < c.Length; k++)
							if (c [k] != null && c [k].gameObject.layer != LayerMask.NameToLayer ("Map")) {
								validPos = false;
								break;
							}
								
						if (validPos)
							return new Vector3(pos.x, bounds.size.y / 2, pos.z);
					}
				}
			}

		return new Vector3 (0, 0, 0);
	}

	T MakeNewBuilding<T>(string tag) where T : Building
	{
		GameObject bldg = null;
		GameObject[] buildings = player.updatedPrefabs.buildingPrefabs;

		for (int i = 0; i < buildings.Length; i++) {
			if (buildings [i].GetComponent<Building>() is T && buildings[i].gameObject.tag == tag) {
				bldg = buildings [i];
				break;
			}
		}

		Vector3 emptyArea = GetEmptyArea(bldg.GetComponent<Building>().getModel().GetComponent<Renderer>().bounds);

		if (emptyArea == new Vector3(0,0,0))
			return null;

		T t = null;

		if (bldg.GetComponent<Building> ().CheckCost ()) {
			t = (T)(bldg.GetComponent<Building>().InstantiatePlayableObject (emptyArea, player.transform).GetComponent<Building> ());
			t.SetToAwake ();
		}

		return t;
	}

	T MakeNewBuilding<T>(Vector3 location) where T : Building
	{
		if (location == new Vector3(0,0,0))
			return null;

		GameObject bldg = null;
		GameObject[] buildings = player.updatedPrefabs.buildingPrefabs;

		for (int i = 0; i < buildings.Length; i++) {
			if (buildings [i].GetComponent<Building>() is T) {
				bldg = buildings [i];
				break;
			}
		}

		T t = null;

		if (bldg != null && bldg.GetComponent<Building> ().CheckCost ()) {
			t = (T)(bldg.GetComponent<Building>().InstantiatePlayableObject (location, player.transform).GetComponent<Building> ());
			t.SetToAwake ();
		}

		return t;
	}

	bool MakeNewTrainingBuilding(string tag)
	{
		TrainingBuilding tB = MakeNewBuilding<TrainingBuilding>(tag);
		
		GameObject[] units = player.updatedPrefabs.unitPrefabs;

		for (int i = 0; i < units.Length; i++) {
			if (units [i].tag == tag) {
				tB.unit = units [i].GetComponent<Unit>();
				return true;
			}
		}

		return false;
	}

	void MilitaryManagement()
	{
		if (army == null && population >= player.minPopForAttack) {
			army = (GameObject.Instantiate (player.armyGroupPrefab, player.transform) as GameObject).GetComponent<UnitGroup>();

			for (int i = 0; i < player.activeUnits.Count; i++) {
				army.Add (player.activeUnits [i]);
			}
		}
	}

	public static int SortByValue(WeighedTask t1, WeighedTask t2)
	{
		if (t1.value < 0 && t2.value > 0)
			return -1;
		else if (t1.value > 0 && t2.value < 0)
			return 1;
		else if (t1.value < 0 && t2.value < 0)
			return - Mathf.Abs (t1.value).CompareTo (Mathf.Abs (t2.value));
		else
			return - t1.value.CompareTo (t2.value);
	}

	public static int SortByDistanceToTC(Building b1, Building b2)
	{
		return (b2.transform.position - b2.player.industrialCenter.transform.position).magnitude.CompareTo ((b1.transform.position - b1.player.industrialCenter.transform.position).magnitude);
	}
}


