using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RTSObjectGroup : MonoBehaviour {

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

	public abstract void InteractWith (Interactable i);

}
