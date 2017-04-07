using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStrategy : Strategy {

	public PlayerStrategy(PlayerContext player)
	{
		this.player = player;
	}

	public override void RealizeStrategy()
	{
		Debug.Log ("Player " + player.playerId + " made a move!");
	}
}
