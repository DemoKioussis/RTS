using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingController : MonoBehaviour, Interacts {
	private Building building;
	private BuildingGroupController group;

	void Awake() {
		building = GetComponent<Unit>();
	}
	public void setGroup(BuildingGroupController g)
	{
		if (group != null)
		{
			group.removeBuilding(this);
		}
		group = g;
	}
	public UnitGroupController getGroup()
	{
		return group;
	}
}
