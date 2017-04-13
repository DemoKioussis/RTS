using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapController : MonoBehaviour {

	public CameraControls playerCamera; 
	public Vector2 minimapCoordinates;

	// Use this for initialization
	void Start () {
		playerCamera = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CameraControls> ();
	}

	void Update(){
		minimapCoordinates = Input.mousePosition;
	}

	public void UpdateMinimapAndPlayerCamera(){

		CalculateMinimapRatio();

		UpdatePlayerCameraPosition ();
	}

	private void CalculateMinimapRatio(){

	}

	private void UpdatePlayerCameraPosition(){
	}
}
