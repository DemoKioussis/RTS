using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogOfWar : MonoBehaviour {

	void OnTriggerEnter(Collider c)
	{
		PlayerContext p = c.transform.GetComponentInParent<RTSObject> ().player;
		if (p.strategy is PlayerStrategy)
			Destroy (gameObject);
		else
			Destroy (c.gameObject);
	}
}
