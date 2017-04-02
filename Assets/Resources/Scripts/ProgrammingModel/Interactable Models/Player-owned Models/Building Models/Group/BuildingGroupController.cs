using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingGroupController : MonoBehaviour, Interacts {

	BuildingGroup group;

	void Awake()
	{
		group = GetComponent<BuildingGroup>();

	}
	public void add(BuildingController u) {
		group.buildings.Add(u);
	}
	public void remove(BuildingController u) {
		group.buildings.Remove(u);
	}
	public bool isEmpty() {
		return group.isEmpty();

	}
	public virtual void interactWith(Interactable i)
	{
		foreach (BuildingController u in group.buildings)
		{
			if(u.getGroup()!=this)
				SetBuildingGroup(u);
		}
		switch (i.getInteractionType()) {
		case INTERACTION_TYPE.BUILDING:
			break;
		case INTERACTION_TYPE.POSITION:
			SetBuildingGroupSpawnToPosition((MapPos)i);
			break;
		case INTERACTION_TYPE.UNIT:
			break;
		case INTERACTION_TYPE.RESOURCE:
			break;
		}
	}
	private void SetBuildingGroup(BuildingController u) {
		u.transform.parent = transform;
		u.setGroup(this);
	}

	public void removeBuilding(BuildingController u) {
		remove(u);
		if (isEmpty())
		{
			Destroy(this.gameObject);
		}
	}

	public virtual void SetBuildingGroupSpawnToPosition(MapPos m) {
		group.SetSpawnTo(m);
	}
}
