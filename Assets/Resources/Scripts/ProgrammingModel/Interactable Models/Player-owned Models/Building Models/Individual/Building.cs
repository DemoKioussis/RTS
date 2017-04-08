using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BUILDING_TYPE {RESOURCE, TRAINING, TOWNCENTER, ATTACK, DEFENSE}

public abstract class Building : RTSObject {

	protected bool built = false;
	protected bool awake = true;

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

	}

	public virtual void SetToSleep(){

	}

	public virtual void SetSpawnPointAs(Vector3 spawnPosition){
		Debug.Log ("Setting spawn point");
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
