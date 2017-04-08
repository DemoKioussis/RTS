using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BUILDING_TYPE {RESOURCE, TRAINING, TOWNCENTER, ATTACK, DEFENSE}

public abstract class Building : RTSObject {

	protected bool built = false;
	public bool awake = true;
	protected Vector3 resourcePosition;

	private Vector3 spawnPoint;

	public GameObject flagPrefab;
	private GameObject flagReference;

	// Update is called once per frame
	void Update () {
		if (!playerSetUp && player != null) {
			player.activeBuildings.Add (this);
			playerSetUp = true;
		}
	}

	protected override float Influence (Vector3 samplePosition)
	{
		return 0;
		// To do
	}

	protected override void Heal(int hp)
	{
		base.Heal (hp);

		if (!built && stats.hitpoints == stats.maxHitpoints)
			built = true;
	}

	protected virtual void AddStub(Stub stub)
	{
		// To do
	}

	protected Vector3 GetSpawnPoint(){
		return spawnPoint;
	}

	protected void SetFlagReference(GameObject flag){
		flagReference = flag;
	}

	protected GameObject GetFlagReference(){
		return flagReference;
	}

	public virtual void SetToAwake(){ 
		// Debug.Log ("Building status cannot be changed");
	}

	public virtual void SetToSleep(){ 
		// Debug.Log ("Building status cannot be changed"); 
	}

	public virtual void SetPositionOfBuilding(Vector3 pos){
		// Debug.Log ("Building position cannot be set");
	}

	public Vector3 GetPositionOfResource(){
		return resourcePosition;
	}

	public virtual void SetSpawnPointAs(Vector3 spawnPosition){
		spawnPoint = spawnPosition;
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
