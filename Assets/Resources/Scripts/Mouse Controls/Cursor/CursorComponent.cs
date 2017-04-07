using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorComponent : MonoBehaviour {

	public GameObject gObject;

	private RTSObject objectToSpawn;

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
			objectToSpawn = testObject.GetComponent<RTSObject> ();
			gameObjectRenderer = testObject.GetComponent<Renderer> ();
			objectToSpawn.isBeingPlaced = true;
			// get the initial property colors of the object
			initialColor = gameObjectRenderer.material.color; 
		// end of test
		*/

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

		if (objectToSpawn != null) {
			// set the color of the object depending on collision information
			if (objectToSpawn.objectIsColliding) 
			{
				SetColor(colorOfCollision);
			} 
			else 
			{
				SetColor(colorOfNoCollision);
			}

			if (Input.GetButton("LeftClick"))
			{
				if (!objectToSpawn.objectIsColliding && hit.collider != null) {
					SetGameObjectTo (hit.point + new Vector3(0, yOffset, 0));
				}
			}
		}
	}

	// Function to set the color of the object on the cursor
	public void SetColor(Color color){
		gameObjectRenderer.material.color = color;
	}

	public void SetBuildingObject(GameObject rtsObject){
		objectToSpawn = rtsObject.GetComponent<RTSObject>();
	}

	public void SpawnObjectOnCursor(GameObject rtsObject){
		objectToSpawn = ((GameObject) Instantiate(rtsObject, transform.position, transform.rotation, transform)).GetComponent<RTSObject>(); // get reference to the object

		objectToSpawn.isBeingPlaced = true;

		// get the renderer of the object
		gameObjectRenderer = objectToSpawn.GetComponent<Renderer> ();

		// get the initial property colors of the object
		initialColor = gameObjectRenderer.material.color; 

		// set color of the object
		gameObjectRenderer.material.color = colorOfNoCollision;

		objectToSpawn.CannotBePlaced();
	}

	private void SetGameObjectTo(Vector3 position){
		// object is no longer being placed
		objectToSpawn.isBeingPlaced = false;

		// set the parent of the created object to the scene
		objectToSpawn.transform.parent = this.transform.parent;

		// set the position of the created object to the level
		objectToSpawn.transform.position = position; 

		// reset the game object's color to its initial property
		gameObjectRenderer.material.color = initialColor;

		objectToSpawn = null;
	}
}
