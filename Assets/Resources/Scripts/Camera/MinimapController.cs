using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapController : MonoBehaviour {

	public CameraControls playerCamera; 
	public Vector2 minimapCoordinates;
	public Collider collider;

	// Use this for initialization
	void Start () {
	}

	void Update(){
		if (collider.bounds.Contains (Input.mousePosition)) {
			Vector2 mouseInMap = new Vector2 (Input.mousePosition.x - collider.gameObject.transform.position.x,
				                     Input.mousePosition.y - collider.gameObject.transform.position.y);
			//mouseInMap *= (16 / 9);
			if (Input.GetMouseButton(0)){
				if (playerCamera == null) {
					playerCamera = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CameraControls> ();
				}
				playerCamera.updatePosition ();
			}
		}

		minimapCoordinates = Input.mousePosition;
	}

	public void UpdateMinimapAndPlayerCamera(){
		if (playerCamera == null) {
			playerCamera = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CameraControls> ();
		}
	}



}
