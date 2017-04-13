using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewRangeCollider : MonoBehaviour {
    public SphereCollider col;
    RTSObject rtsObject;
    void Awake() {
        rtsObject = GetComponentInParent<RTSObject>();
        col = GetComponent<SphereCollider>();
        rtsObject.viewRangeCollider = this;
        updateCollider();
    }

    public void updateCollider() {
        if (col != null)
        {
            col.radius = rtsObject.stats.viewRange;
        }

    }
}
