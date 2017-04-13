using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapController : MonoBehaviour {

	public CameraControls playerCamera; 
	public Vector2 minimapCoordinates;

	// Use this for initialization
	void Start () {
		
	}

	void Update(){
		minimapCoordinates = Input.mousePosition;
	}

	public void test(){
		Debug.Log ("Hi");
		Debug.Log ("Mouse :" + Input.mousePosition);
		Debug.Log (transform.position);
	}

	public void UpdateMinimapAndPlayerCamera(){
		if (playerCamera == null) {
			playerCamera = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CameraControls> ();
		}

		CalculateMinimapRatio();

		UpdatePlayerCameraPosition ();
	}

	private void CalculateMinimapRatio(){
		
	}

	private void UpdatePlayerCameraPosition(){
	}
}
