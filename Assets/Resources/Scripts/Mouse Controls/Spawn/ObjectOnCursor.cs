using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectOnCursor : MonoBehaviour {

	public GameObject stub;
	public GameObject gameLevel;

	void Update()
	{
		if (Input.GetButton("LeftClick"))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast (ray)) {
				Debug.Log("Click on plane");
				Instantiate (stub, transform.position, transform.rotation);
			}
		}
	}
}
