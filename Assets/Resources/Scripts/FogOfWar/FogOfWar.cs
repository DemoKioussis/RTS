﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogOfWar : MonoBehaviour {

	void OnTriggerEnter(Collider c)
	{
		RTSObject rts = c.transform.GetComponentInParent<RTSObject> ();
		if (rts != null) {
			PlayerContext p = rts.player;
			if (p != null && p.strategy is PlayerStrategy) {
//				Building b = c.GetComponentInParent<Building> ();
//				if (b == null || b != null && !b.isBeingPlaced) {
					Destroy (gameObject);
//				}
			}
			else
				Destroy (c.gameObject);
		}
	}
}
