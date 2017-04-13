using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public MeshRenderer render;
    public Unit u;
    void Awake() {
    }
    public void fire() {
        Debug.Log("FIRE!");
        render.enabled = true;
        render.transform.localScale = new Vector3(0.1f,u.militaryStats.attackRange,0.1f);

    }
    public void reload() {
        render.enabled = false;

    }

}
