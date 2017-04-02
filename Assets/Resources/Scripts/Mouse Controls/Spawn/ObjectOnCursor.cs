using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectOnCursor : MonoBehaviour {

	public GameObject prefab; // for testing

	private GameObject gameObjectToSpawn;

	private bool objectIsColliding = false;

	private Renderer gameObjectRenderer;
	private Color initialColor;
	private Color transparentColor;

	public float transparentFactor = 0.1f;

	void Start()
	{
		// Testing
		gameObjectToSpawn = (GameObject) Instantiate (prefab, transform.position, transform.rotation, transform);

		// get the renderer of the object
		gameObjectRenderer = gameObjectToSpawn.GetComponent<Renderer> ();

		// get the color
		initialColor = gameObjectRenderer.material.color;

		// set the transparency of the object
		gameObjectRenderer.material.color = new Color (initialColor.r, initialColor.g, initialColor.b, transparentFactor);
	}

	void Update()
	{
		transform.position = Utils.GetPositionOfMouseOn ("Terrain"); // update the position of the mouse

		if (gameObjectToSpawn != null) {
			
			if (Input.GetButton("LeftClick"))
			{
				RaycastHit hit = Utils.GetPositionFromMouseClick ();
				if (hit.collider != null) {
					SetGameObjectTo (hit.point);
				}
			}

		}
	}

	public void SpawnObjectOnCursor(GameObject spawnObject){
		gameObjectToSpawn = (GameObject) Instantiate(spawnObject, transform.position, transform.rotation, transform); // get reference to the object

		// get the renderer of the object
		gameObjectRenderer = gameObjectToSpawn.GetComponent<Renderer> ();

		// get the initial property colors of the object
		initialColor = gameObjectRenderer.material.color; 

		// set the transparency of the object
		gameObjectRenderer.material.color = new Color (initialColor.r, initialColor.g, initialColor.b, transparentFactor);

	}

	private void SetGameObjectTo(Vector3 position){
		// set the parent of the created object to the scene
		gameObjectToSpawn.transform.parent = this.transform.parent;

		// set the position of the created object to the level
		gameObjectToSpawn.transform.position = position; 

		// reset the game object's color to its initial property
		gameObjectRenderer.material.color = initialColor;

		gameObjectToSpawn = null;
	}



}
