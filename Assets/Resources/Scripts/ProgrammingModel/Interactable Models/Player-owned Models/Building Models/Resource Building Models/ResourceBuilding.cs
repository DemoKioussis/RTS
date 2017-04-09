using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceBuilding : Building {

	public float yOffset = 0.25f;

	public Resource resource;
	public ResourceBuildingStats resourceBldgStats;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if (awake) {
			UpdateTime ();

			if(resource != null && resourceReadyToGather())
			{
				if (resource.type == "Glue") 
				{
					resource.GetQuantity (resourceBldgStats.glueReturn);
					player.glueQuantity++;
				} 
				else if (resource.type == "Paper") 
				{
					resource.GetQuantity (resourceBldgStats.paperReturn);
					player.paperQuantity++;
				}
			}
		}
	}
		
	public override float Influence ()
	{
		return base.Influence () + 0; 
		// To do
	}

	public override void AssignResourcePosition(Resource res){
		resource = res;
	}

	public override void ClearResourcePosition(){
		resource = null;
	}

	// function to set building accord to resource where the resource is assigned to building
	public override void SetToResource(){
		resource.building = this;
	}

	// function to set building according to resource in case the building is not assigned a resource
	public override void AssociateToResource(Resource res){
		resource = res;
		resource.building = this;
	}

	public override Vector3 GetPositionOfResource(){
		return resource.transform.position;
	}

	public void DisableResourceCollider(){
		resource.gameObject.GetComponent<Collider> ().enabled = false;
	}
		
	public override void SetSpawnPointAs(Vector3 p){
		Debug.Log ("You cannot set a spawn point for this building");
	}

	// Adds the quantity specified to the resource quantity in PlayerContext
	public void registerResource(System.Type type, int quantity)
	{
		// To do
	}

	public override BUILDING_TYPE getBuildingType(){
		return BUILDING_TYPE.RESOURCE;
	}

	private bool resourceReadyToGather(){
		if (gameTime >= 1.0f && (int)gameTime % (int)resourceBldgStats.gatheringTime == 0) {
			gameTime = 0.0f;
			return true;
		}

		return false;
	}
}
