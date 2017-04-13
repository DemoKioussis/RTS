using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingGroup : RTSObjectGroup {

    public void removeNullBuilding() {
        for (int i = 0; i < rtsObjects.Count; i++) {
            if (rtsObjects[i] == null) {
                rtsObjects.RemoveAt(i);
                i = 0;
            }
        }
    }
	public void SetToAwake(){
		foreach (Building bldg in rtsObjects) {
            removeNullBuilding();
			bldg.SetToAwake();
		}
	}

	public void SetToSleep(){
		foreach (Building bldg in rtsObjects) {
            removeNullBuilding();
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
        removeNullBuilding();
        foreach (Building bldg in rtsObjects)
        {
			bldg.InteractWith (p);
        }
    }
    public override void unitInteraction(Unit u) { }
    public override void resourceInteraction(Resource r) { }
}
