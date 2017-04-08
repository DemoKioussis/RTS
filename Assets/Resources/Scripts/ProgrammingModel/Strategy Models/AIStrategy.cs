using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIStrategy : Strategy {

	public AIStrategy(PlayerContext player)
	{
		this.player = player;
		int numVertices = GameContext.currentGameContext.map.GetComponent<MeshFilter> ().mesh.vertices.Length;
		GameContext.currentGameContext.map.GetComponent<MeshFilter> ().mesh.colors = new Color[numVertices];
	}

	public override void RealizeStrategy()
	{
		
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
}
