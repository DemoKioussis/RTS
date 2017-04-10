using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BUILDING_TYPE {RESOURCE, TRAINING, TOWNCENTER, ATTACK, DEFENSE}

public abstract class Building : RTSObject {

	protected bool spawnPointSet = false;
	protected float gameTime = 0.0f;

	public bool awake = false;

	protected MapPos spawnPoint;

	public GameObject flagPrefab;
	private GameObject flagReference;

	// Update is called once per frame
	void Update () {
		if (!playerSetUp && player != null) {
			player.activeBuildings.Add (this);
			playerSetUp = true;
		}
	}

	public override float Influence ()
	{
		return stats.hitpoints;
		// To do
	}

	protected override void Heal(int hp)
	{
		// nothing
	}

	protected virtual void AddStub(Stub stub)
	{
		// To do
	}

	protected MapPos GetSpawnPoint(){
		return spawnPoint;
	}

	protected void SetFlagReference(GameObject flag){
		flagReference = flag;
	}

	protected GameObject GetFlagReference(){
		return flagReference;
	}

	protected void UpdateTime(){
		gameTime += Time.deltaTime;
	}

	public void SetToAwake(){ 
		awake = true;
	}

	public virtual void SetToSleep(){ 
		// Debug.Log ("Building status cannot be changed"); 
	}

	public virtual void ToggleAwake()
	{
		awake ^= true;
	}

	// Mouse - selection functions
	public virtual void AssignResourcePosition(Resource res){
		// Debug.Log ("Building position cannot be set");
	}

	public virtual void ClearResourcePosition(){
		// Debug.Log ("Building position cannot be cleared");
	}

	// Mouse - building/resource association & placement
	public virtual void SetToResource(){

	}

	// AI - building/resource association
	public virtual void AssociateToResource(Resource res){

	}

	public virtual Vector3 GetPositionOfResource(){
		// Debug.Log ("Returning empty vector");
		return new Vector3(0,0,0);
	}

	public virtual void SetSpawnPointAs(Vector3 spawnPosition){
		spawnPoint.setPosition(spawnPosition);
	}

	public override void Destroy(){
		player.activeBuildings.Remove (this);
		GameContext.currentGameContext.activeBuildings.Remove (this);
		Destroy (this.gameObject);
	}

    public override INTERACTION_TYPE getInteractionType()
    {
        return INTERACTION_TYPE.BUILDING;
    }

	public abstract BUILDING_TYPE getBuildingType();
    public override void buildingInteraction(Building b) { }
    public override void positionInteraction(MapPos p) { }
    public override void unitInteraction(Unit u) { }
    public override void resourceInteraction(Resource r) { }
}
