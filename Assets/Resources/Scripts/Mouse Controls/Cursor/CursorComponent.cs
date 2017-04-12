using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorComponent : MonoBehaviour {

	public RTSObject currentRTSObject;

	private PlayerContext player;
	private Renderer gameObjectRenderer;
	private Color initialColor;
	private Color transparentColor;

	private RaycastHit hit;

	public float transparentFactor = 0.5f;

	public Color colorOfNoCollision;
	public Color colorOfCollision;

	public float yOffset = 0.0f; // the actual offset height of the mouse above the map

	void Start()
	{
		/* 
		//testing
			GameObject testObject = (GameObject)Instantiate (gObject, transform.position, transform.rotation, transform);
			currentRTSObject = testObject.GetComponent<RTSObject> ();
			gameObjectRenderer = testObject.GetComponent<Renderer> ();
			currentRTSObject.isBeingPlaced = true;
			// get the initial property colors of the object
			initialColor = gameObjectRenderer.material.color; 
		// end of test
		*/

		player = GetComponentInParent<PlayerContext> ();

		// set the colors
		colorOfNoCollision = new Color (0, 1, 0, transparentFactor);
		colorOfCollision = new Color(1, 0, 0, transparentFactor);
		//
		// end of test
	}

	void Update()
	{
		hit = Utils.GetPositionFromMouseClick (1 << LayerMask.NameToLayer("Map"));

		if(hit.collider != null){
			transform.position = hit.point + new Vector3(0, yOffset, 0); // update the position of the mouse
		}

		if (currentRTSObject != null) {
			// set the color of the object depending on collision information
			if (currentRTSObject.objectIsColliding) 
			{
				SetColor(colorOfCollision);
			} 
			else 
			{
				SetColor(colorOfNoCollision);
			}

			if (Input.GetButton("LeftClick"))
			{
				if (!currentRTSObject.objectIsColliding && hit.collider != null) 
				{
					SetGameObjectTo (hit.point + new Vector3 (0, yOffset, 0));
				}
				else if (currentRTSObject.objectIsColliding) 
				{
					CancelAction ();
				}
			}
		}
	}

	// Function to set the color of the object on the cursor
	public void SetColor(Color color){
		gameObjectRenderer.material.color = color;
	}

	public void SetBuildingObject(GameObject rtsObject){
		currentRTSObject = rtsObject.GetComponent<RTSObject>();
	}

	public void SpawnObjectOnCursor(GameObject nextRTSObject){
		if (currentRTSObject == null) {
			// User is not currently placing an object
			GameObject tempObj = nextRTSObject.GetComponent<RTSObject>().InstantiatePlayableObject(transform.position, transform); // get reference to the object

			currentRTSObject = tempObj.GetComponent<RTSObject> ();

			// get the renderer of the object
			gameObjectRenderer = currentRTSObject.getModel();

			// get the initial property colors of the object
			initialColor = gameObjectRenderer.material.color; 

			// set color of the object
			gameObjectRenderer.material.color = colorOfNoCollision;

			currentRTSObject.CanBePlaced ();

			currentRTSObject.isBeingPlaced = true;

		}
	}

	public void CancelAction(){
		if (currentRTSObject != null) {
			player.Sell (currentRTSObject);

			// Remove from the list of active objects
			RTSObject.RemovePlayableObject (currentRTSObject);

			// Destroy it in the game
			Destroy (currentRTSObject.gameObject);

			// set the reference to null
			currentRTSObject = null;
		}
	}

	private void SetGameObjectTo(Vector3 position){
		
		// object is no longer being placed
		currentRTSObject.isBeingPlaced = false;
		currentRTSObject.objectIsColliding = false;

		// set the parent of the created object to the scene
		currentRTSObject.transform.parent = this.transform.parent;

		Building tempBldg = currentRTSObject.GetComponent<Building> ();

		// set the position of the created object to the level
		if (tempBldg != null) 
		{
			if (tempBldg.getBuildingType () == BUILDING_TYPE.RESOURCE) 
			{
				// object is a resource building
				tempBldg.SetToResource ();
			}
			tempBldg.SetToAwake ();
		} 
		else 
		{
			currentRTSObject.transform.position = position; 
		}

		// reset the game object's color to its initial property
		gameObjectRenderer.material.color = initialColor;

		currentRTSObject = null;
	}
}
