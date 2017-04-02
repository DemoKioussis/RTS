using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectOnCursor : MonoBehaviour {

	public GameObject prefab; // for testing

	private GameObject gameObjectToSpawn;

	private bool objectIsColliding;

	private Renderer gameObjectRenderer;
	private Color initialColor;
	private Color transparentColor;

	private RaycastHit hit;

	private BoxCollider boxCollider;

	public float transparentFactor = 0.5f;

	public Color colorOfNoCollision;
	public Color colorOfCollision;

	public float yOffset = 0.0f; // the actual offset height of the mouse above the map

	void Start()
	{
		// set the colors
		colorOfNoCollision = new Color (0, 1, 0, transparentFactor);
		colorOfCollision = new Color(1, 0, 0, transparentFactor);

		boxCollider = GetComponent<BoxCollider> ();

		// Testing
		//
		gameObjectToSpawn = (GameObject) Instantiate (prefab, transform.position, transform.rotation, transform);

		// get the renderer of the object
		gameObjectRenderer = gameObjectToSpawn.GetComponent<Renderer> ();

		// get the color
		initialColor = gameObjectRenderer.material.color;

		// set the transparency of the object
		objectIsColliding = false;

		// set the size of the collider
		boxCollider.size = gameObjectToSpawn.transform.localScale;
		//
		// end of test
	}

	void Update()
	{
		hit = Utils.GetPositionFromMouseClick (1 << LayerMask.NameToLayer("Map"));

		if(hit.collider != null){
			transform.position = hit.point + new Vector3(0, yOffset, 0); // update the position of the mouse
		}

		if (gameObjectToSpawn != null) {
			// set the color of the object depending on collision information
			if (objectIsColliding) 
			{
				SetColor(colorOfCollision);
			} 
			else 
			{
				SetColor(colorOfNoCollision);
			}

			if (Input.GetButton("LeftClick"))
			{
				if (!objectIsColliding && hit.collider != null) {
					SetGameObjectTo (hit.point + new Vector3(0, yOffset, 0));
				}
			}

		}
	}

	public void hasCollided(){
		objectIsColliding = true;
	}

	public void hasNotCollided(){
		objectIsColliding = false;
	}

	// Function to set the color of the object on the cursor
	public void SetColor(Color color){
		gameObjectRenderer.material.color = color;
	}

	public void SpawnObjectOnCursor(GameObject spawnObject){
		gameObjectToSpawn = (GameObject) Instantiate(spawnObject, transform.position, transform.rotation, transform); // get reference to the object

		// get the renderer of the object
		gameObjectRenderer = gameObjectToSpawn.GetComponent<Renderer> ();

		// get the initial property colors of the object
		initialColor = gameObjectRenderer.material.color; 

		// set the transparency of the object
		objectIsColliding = false;

		// set the size of the collider
		boxCollider.size = gameObjectToSpawn.transform.localScale;
	}

	private void SetGameObjectTo(Vector3 position){
		// set the parent of the created object to the scene
		gameObjectToSpawn.transform.parent = this.transform.parent;

		// set the position of the created object to the level
		gameObjectToSpawn.transform.position = position; 

		// reset the game object's color to its initial property
		gameObjectRenderer.material.color = initialColor;

		// set the scale of the mouse component
		transform.localScale = new Vector3(0,0,0);

		gameObjectToSpawn = null;
		objectIsColliding = false;
	}
}
