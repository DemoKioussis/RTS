using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIStrategy : Strategy {
	public enum Activity {MAKEPAPER, MAKEGLUE, MAKESHORTRANGE, MAKELONGRANGE};
	int paperQuantityInStock;
	int glueQuantityInStock;
	int shortRangeUnits;
	int longRangeUnits;
	float paperGatheringRate;
	float glueGatheringRate;
	int population;
	int populationLimit;
	float dangerIndex;

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
		int numVertices = GameContext.currentGameContext.map.GetComponent<MeshFilter> ().mesh.vertices.Length;
		GameContext.currentGameContext.map.GetComponent<MeshFilter> ().mesh.colors = new Color[numVertices];

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
				wasFulfilled = ManageGlue ();
			} else if (tasks [i].activity == Activity.MAKEPAPER) {
				wasFulfilled = ManagePaper ();
			} else if (tasks [i].activity == Activity.MAKESHORTRANGE) {
				wasFulfilled = ManageShortRange (tasks[i].value);
			} else if (tasks [i].activity == Activity.MAKELONGRANGE) {
				wasFulfilled = ManageLongRange (tasks[i].value);
			}

			if (!wasFulfilled)
				break;
		}
	}

	void UpdateInfluenceMap()
	{
		GameContext game = GameContext.currentGameContext;
		GameMap map = game.map;
		Vector3[] vertices = map.GetComponent<MeshFilter> ().mesh.vertices;
		Color[] colors = new Color[vertices.Length];

		for (int i = 0; i < vertices.Length; i++) {
			float magnitude = 0.0f;
			for (int j = 0; j < game.activePlayers.Count; j++) {
				if (j != player.playerId) {
					PlayerContext p = game.activePlayers [j];
					for (int k = 0; k < p.activeUnits.Count; k++)
						magnitude += p.activeUnits [k].Influence() / (1 + (vertices [i] - p.activeUnits [k].transform.position).sqrMagnitude);
					for (int k = 0; k < p.activeBuildings.Count; k++)
						magnitude += p.activeBuildings [k].Influence() / (1 + (vertices [i] - p.activeBuildings [k].transform.position).sqrMagnitude);
				}
			}
			colors [i] = new Color (1.0f, 1.0f, 1.0f) * magnitude / 50.0f;
		}

		GameContext.currentGameContext.map.GetComponent<MeshFilter> ().mesh.colors = colors;

		float overallLocalInfluence = 0;

		for (int i = 0; i < vertices.Length; i++) {
			for (int k = 0; k < player.activeUnits.Count; k++)
				overallLocalInfluence += player.activeUnits [k].Influence() / (1 + (vertices [i] - player.activeUnits [k].transform.position).sqrMagnitude);
			for (int k = 0; k < player.activeBuildings.Count; k++)
				overallLocalInfluence += player.activeBuildings [k].Influence() / (1 + (vertices [i] - player.activeBuildings [k].transform.position).sqrMagnitude);
		}

		overallLocalInfluence /= 50.0f;

		dangerIndex = 1.0f / overallLocalInfluence;
	}

	float PaperResourceHeuristic()
	{
		return 1.0f / (paperGatheringRate + paperQuantityInStock);
	}

	float GlueResourceHeuristic ()
	{
		return 1.0f / (glueGatheringRate + paperQuantityInStock);
	}

	float ShortRangeHeuristic()
	{
		if (population == 0 || shortRangeUnits == 0 || longRangeUnits == 0)
			return 1.5f;
		else
			return (float)population * ((float) shortRangeUnits / (float) longRangeUnits > 1.5f ? -1.0f : 1.0f) / (float)populationLimit + dangerIndex;
	}

	float LongRangeHeuristic()
	{
		if (population == 0 || shortRangeUnits == 0 || longRangeUnits == 0)
			return 1.0f / 1.5f;
		else
			return (float)population * ((float) longRangeUnits / (float) shortRangeUnits > 1.0f / 1.5f ? -1.0f : 1.0f) / (float)populationLimit + dangerIndex;
	}

	void UpdateTasks()
	{
		for (int i = 0; i < tasks.Count; i++) {
			if (tasks [i].activity == Activity.MAKEPAPER) {
				tasks [i].value *= PaperResourceHeuristic ();
			}
			else if (tasks [i].activity == Activity.MAKEGLUE) {
				tasks [i].value *= GlueResourceHeuristic ();
			}
			else if (tasks [i].activity == Activity.MAKESHORTRANGE) {
				tasks [i].value *= ShortRangeHeuristic ();
			}
			else if (tasks [i].activity == Activity.MAKELONGRANGE) {
				tasks [i].value *= LongRangeHeuristic ();
			}
		}

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
					float rate = 1.0f / rB.resourceBldgStats.gatheringTime;
					if (rB.resource.gameObject.tag == "Paper") {
						paperGatheringRate += rate;
					}
					else if (rB.resource.gameObject.tag == "Glue") {
						glueGatheringRate += rate;
					}

				} else if (player.activeBuildings [i] is TrainingBuilding) {
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
			if (player.activeUnits [i] is ShortRangeUnit)
				shortRangeUnits++;
			else if (player.activeUnits [i] is LongRangeUnit)
				longRangeUnits++;
		}
	}

	Vector3 FindClosestResource(string resourceType)
	{
		Resource[] resources = GameContext.currentGameContext.activeResources;
		Resource closestResource = null;
		float minDistance = 100000000000000;

		for (int i = 0; i < resources.Length; i++)
			if (resources [i].gameObject.tag == resourceType) {
				float distance = (resources [i].transform.position - player.industrialCenter.transform.position).magnitude;
				if (distance < minDistance) {
					minDistance = distance;
					closestResource = resources [i];
				}
			}

		if (closestResource != null)
			return closestResource.transform.position;
		else
			return new Vector3(0, 0, 0);
	}

	bool ManagePaper()
	{
		Vector3 point = FindClosestResource ("Paper");
		return MakeNewBuilding<ResourceBuilding> (out point);
	}

	bool ManageGlue()
	{
		Vector3 point = FindClosestResource ("Glue");
		return MakeNewBuilding<ResourceBuilding> (out point);
	}

	bool ManageShortRange(float value)
	{
		for (int i = 0; i < player.activeBuildings.Count; i++) {
			if(!(value > 0 ^ player.activeBuildings[i].awake))
			{
				TrainingBuilding bldg = player.activeBuildings [i].GetComponent<TrainingBuilding> ();
				ShortRangeUnit unit = bldg.unit.GetComponent<ShortRangeUnit> ();

				if (bldg != null && unit != null)
				{
					player.activeBuildings [i].ToggleAwake();
					return true;
				}
			}	
		}

		if (value > 0)
			return MakeNewTrainingBuilding<ShortRangeUnit>();
		else
			return true;
	}

	bool ManageLongRange(float value)
	{
		for (int i = 0; i < player.activeBuildings.Count; i++) {
			if(!(value > 0 ^ player.activeBuildings[i].awake))
			{
				TrainingBuilding bldg = player.activeBuildings [i].GetComponent<TrainingBuilding> ();
				LongRangeUnit unit = bldg.unit.GetComponent<LongRangeUnit> ();

				if (bldg != null && unit != null)
				{
					player.activeBuildings [i].ToggleAwake();
					return true;
				}
			}
		}

		if (value > 0)
			return MakeNewTrainingBuilding<LongRangeUnit>();
		else
			return true;
	}

	Vector3 GetEmptyArea()
	{
//		Bounds boundingBox = bldg.GetComponent<
		return new Vector3 (0, 0, 0);
	}

	T MakeNewBuilding<T>(out Vector3 emptyArea) where T : Building
	{
		emptyArea = GetEmptyArea();

		GameObject[] buildings = player.updatedPrefabs.buildingPrefabs;

		T t = null;

		if (emptyArea == null)
			return t;

		for (int i = 0; i < buildings.Length; i++) {
			if (buildings [i].GetComponent<Building>() is T) {
				t = (T) RTSObject.InstantiatePlayableObject (buildings[i], emptyArea, player.transform).GetComponent<Building>();
				return t;
			}
		}

		return t;
	}

	bool MakeNewTrainingBuilding<T>() where T : Unit
	{
		Vector3 emptyArea;
		TrainingBuilding tB = MakeNewBuilding<TrainingBuilding>(out emptyArea);
		
		GameObject[] units = player.updatedPrefabs.unitPrefabs;

		for (int i = 0; i < units.Length; i++) {
			if (units [i].GetComponent<Unit>() is T) {
				tB.unit = units [i].GetComponent<Unit>();
				return true;
			}
		}

		return false;
	}

	public static int SortByValue(WeighedTask t1, WeighedTask t2)
	{
		if (t1.value < 0 && t2.value > 0)
			return 1;
		else if (t1.value > 0 && t2.value < 0)
			return -1;
		else if (t1.value < 0 && t2.value < 0)
			return Mathf.Abs (t1.value).CompareTo (Mathf.Abs (t2.value));
		else
			return t1.value.CompareTo (t2.value);
	}
}


