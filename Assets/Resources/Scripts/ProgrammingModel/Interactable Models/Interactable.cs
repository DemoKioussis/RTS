using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public abstract class Interactable : MonoBehaviour {
    public enum TYPE { POSITION, UNIT, BUILDING }

    public int teamId;

    public abstract TYPE getInterationType();
}
