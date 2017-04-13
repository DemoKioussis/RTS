using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewRangeCollider : MonoBehaviour {
    SphereCollider col;
    RTSObject rtsObject;
    void Awake() {
        rtsObject = GetComponentInParent<RTSObject>();
        col = GetComponent<SphereCollider>();
        rtsObject.viewRangeCollider = this;
        
    }

    public void updateCollider() {
        if (col != null)
        {
            col.radius = rtsObject.stats.viewRange;
        }

    }
}
