using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingGroup : RTSObjectGroup {

	public void SetToAwake(){
		foreach (Building bldg in rtsObjects) {
			bldg.SetToAwake();
		}
	}

	public void SetToSleep(){
		foreach (Building bldg in rtsObjects) {
			bldg.SetToSleep();
		}
	}

	public void CreateNewBuilding(char keyInput){
		IndustrialCenter townCenter;
		foreach (Building bldg in rtsObjects) {
			townCenter = bldg.GetComponent<IndustrialCenter> ();
			if (townCenter != null) {
				townCenter.CreateNewBuilding (keyInput);
			}
		}
	}

	public void CancelAction(){
		IndustrialCenter townCenter;
		foreach (Building bldg in rtsObjects) {
			townCenter = bldg.GetComponent<IndustrialCenter> ();
			if (townCenter != null) {
				townCenter.CancelAction ();
			}
		}
	}

    public override void buildingInteraction(Building b) { }
    public override void positionInteraction(MapPos p) {
		Debug.Log (rtsObjects.Count);
        foreach (Building bldg in rtsObjects)
        {
			bldg.SetSpawnPointAs (p.getPosition());
        }
    }
    public override void unitInteraction(Unit u) { }
    public override void resourceInteraction(Resource r) { }
}
