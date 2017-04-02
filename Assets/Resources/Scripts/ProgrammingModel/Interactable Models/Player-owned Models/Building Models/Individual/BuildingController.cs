using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingController : MonoBehaviour{
	private Building building;
	private BuildingGroupController group;

	void Awake() {
		building = GetComponent<Building>();
	}
	public void setGroup(BuildingGroupController g)
	{
		if (group != null)
		{
			group.removeBuilding(this);
		}
		group = g;
	}
	public BuildingGroupController getGroup()
	{
		return group;
	}
	public void SetSpawnTo(MapPos p){
		building.SetSpawnPointAs (p.getPosition());
	}
}
