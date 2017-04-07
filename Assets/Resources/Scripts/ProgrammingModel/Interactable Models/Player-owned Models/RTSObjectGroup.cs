using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RTSObjectGroup : MonoBehaviour {

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

	public virtual void InteractWith(Interactable i)
	{
		foreach (RTSObject obj in rtsObjects)
		{
			//if(obj.GetGroup()!=this)
				//SetUnitGroup(u);
		}
		switch (i.getInteractionType()) {
		case INTERACTION_TYPE.BUILDING:
			break;
		case INTERACTION_TYPE.POSITION:
			MoveGroupToPosition((MapPos)i);
			break;
		case INTERACTION_TYPE.UNIT:
			break;
		case INTERACTION_TYPE.RESOURCE:
			break;
		}
	}

}
