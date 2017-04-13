using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapController : MonoBehaviour {

	private CameraControls playerCamera; 
	private SelectionComponent selection;
	public Vector2 minimapCoordinates;
	public Collider collider;

	// Use this for initialization
	void Start () {
	}

	void Update(){

		if (playerCamera == null) {
			playerCamera = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CameraControls> ();
			selection = playerCamera.gameObject.GetComponent<SelectionComponent> ();
		}

		if (!selection.isSelecting) {
			
			if (collider.bounds.Contains (Input.mousePosition)) {
				Debug.Log (collider.gameObject.transform.position);
				Debug.Log (Input.mousePosition);
				Vector2 mouseInMap = new Vector2 (Input.mousePosition.x - collider.gameObject.transform.position.x,
					                    Input.mousePosition.y - collider.gameObject.transform.position.y);
				if (Input.GetMouseButton (0)) {
					playerCamera.transform.position = new Vector3(mouseInMap.x, playerCamera.transform.position.y, mouseInMap.y);
				}
			}

			minimapCoordinates = Input.mousePosition;
		}
	}

	public void UpdateMinimapAndPlayerCamera(){
		if (playerCamera == null) {
			playerCamera = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CameraControls> ();
		}
	}



}
