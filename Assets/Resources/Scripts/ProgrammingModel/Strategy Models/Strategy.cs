using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Strategy {

	protected PlayerContext player;

	public abstract void RealizeStrategy ();
}
