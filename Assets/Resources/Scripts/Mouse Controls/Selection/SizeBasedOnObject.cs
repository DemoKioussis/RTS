using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeBasedOnObject : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetSize(Bounds bounds)
	{
		GetComponent<Projector> ().orthographicSize = ((bounds.size.x + bounds.size.y + bounds.size.z) / 3.0f);
	}
}
