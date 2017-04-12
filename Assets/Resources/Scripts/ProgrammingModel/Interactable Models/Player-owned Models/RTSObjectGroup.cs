using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RTSObjectGroup : Interactable {

	public List<RTSObject> rtsObjects = new List<RTSObject>();
    int groupSize = 0;
	void Awake () {
		rtsObjects = new List<RTSObject> ();
	}

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Add(RTSObject obj)
	{
		rtsObjects.Add(obj);
        groupSize++;
	}

	public void Remove(RTSObject obj)
	{
		rtsObjects.Remove (obj);
        groupSize--;
        if (IsEmpty()) {
            GameObject.Destroy(this.gameObject, 0.1f);
        }
	}

	public bool IsEmpty() {
		return groupSize == 0;
	}

	public int Count(){
		return rtsObjects.Count;
	}

	public override Stats GetStats ()
	{
		return rtsObjects[0].GetStats(); // only give the first stat of the group
	}
		
    public override INTERACTION_TYPE getInteractionType() {
        return INTERACTION_TYPE.GROUP;
    }
    public override void buildingInteraction(Building b) { }
    public override void positionInteraction(MapPos p) { }
    public override void unitInteraction(Unit u) { }
    public override void resourceInteraction(Resource r) { }
}
