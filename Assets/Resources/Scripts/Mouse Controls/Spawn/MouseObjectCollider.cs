using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseObjectCollider : MonoBehaviour {	

	private ObjectOnCursor cursorObject;

	void Start(){
		cursorObject = gameObject.GetComponent<ObjectOnCursor> ();

	}

	void OnTriggerEnter(Collider col){
		if (col.gameObject.layer != LayerMask.NameToLayer ("Map")) 
		{
			// cursor collided with an object
			cursorObject.hasCollided ();
		} 
		else if (col.gameObject.layer == LayerMask.NameToLayer ("Map")) 
		{
			// cursor collided with the map only
			Debug.Log("Collided with map");
			cursorObject.hasNotCollided();
		}
	}
}
