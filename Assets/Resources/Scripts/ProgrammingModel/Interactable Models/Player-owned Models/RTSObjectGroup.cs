using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RTSObjectGroup : Interactable {

	public List<RTSObject> rtsObjects = new List<RTSObject>();

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
	}

	public void Remove(RTSObject obj)
	{
		rtsObjects.Remove (obj);
        if (IsEmpty()) {
            GameObject.Destroy(this.gameObject, 0);
        }
	}

	public bool IsEmpty() {
		return rtsObjects.Count == 0;
	}

	public void MoveGroupToPosition (MapPos m)
	{
		Debug.Log ("Move Group to " + m);
	}

	public void MoveTo (MapPos p)
	{
		Debug.Log("Move to " + p);
	}
		
    public override INTERACTION_TYPE getInteractionType() {
        return INTERACTION_TYPE.GROUP;
    }
    public override void buildingInteraction(Building b) { }
    public override void positionInteraction(MapPos p) { }
    public override void unitInteraction(Unit u) { }
    public override void resourceInteraction(Resource r) { }


}
