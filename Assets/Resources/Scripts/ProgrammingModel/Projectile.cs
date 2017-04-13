using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public MeshRenderer render;
    public Unit u;
    public LineRenderer line;
    void Awake() {
    }
    public void fire() {
        Vector3[] positions = { transform.position, u.getTargetInteraction().transform.position };
        line.enabled = true;
        line.SetPositions(positions);
       // render.enabled = true;
       // render.transform.localScale = new Vector3(0.1f,u.militaryStats.attackRange,0.1f);

    }
    public void reload() {
        line.enabled = false;

    }

}
