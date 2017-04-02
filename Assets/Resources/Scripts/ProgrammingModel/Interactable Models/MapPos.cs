using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPos : Interactable {


    Vector3 position;

    public void setPosition(Vector3 p) {
        position = p;
    }

    public Vector3 getPosition() {
        return position;
    }
    public override INTERACTION_TYPE getInterationType() {
        return INTERACTION_TYPE.POSITION;
    }

}
