using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIStrategy : Strategy {
	public enum Activity {MAKEPAPER, MAKEGLUE, MAKESHORTRANGE, MAKELONGRANGE};

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
						magnitude += p.activeUnits [k].stats.hitpoints / (1 + (vertices [i] - p.activeUnits [k].transform.position).sqrMagnitude);
					for (int k = 0; k < p.activeBuildings.Count; k++)
						magnitude += p.activeBuildings [k].stats.hitpoints / (1 + (vertices [i] - p.activeBuildings [k].transform.position).sqrMagnitude);
				}
			}
			colors [i] = new Color (1.0f, 1.0f, 1.0f) * magnitude / 50.0f;
		}

		GameContext.currentGameContext.map.GetComponent<MeshFilter> ().mesh.colors = colors;
	}

	float PaperResourceHeuristic()
	{
		int paperQuantityInStock = player.paperQuantity;
		int paperGatheringRate, paperExpenditureRate;

		for (int i = 0; i < player.activeBuildings.Count; i++) {
//			if(player.activeBuildings[i] is ResourceBuilding &&)
		}

		return 0;
	}

	float GlueResourceHeuristic ()
	{
		return 0;
	}

	float ShortRangeHeuristic()
	{
		return 0;
	}

	float LongRangeHeuristic()
	{
		return 0;
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

	public static int SortByValue(WeighedTask t1, WeighedTask t2)
	{
		return t1.value.CompareTo (t2.value);
	}
}


