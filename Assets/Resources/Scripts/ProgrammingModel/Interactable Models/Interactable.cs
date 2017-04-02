using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum INTERACTION_TYPE { POSITION, UNIT, BUILDING, RESOURCE }

public abstract class Interactable : MonoBehaviour {

    public int teamId;

    public abstract INTERACTION_TYPE getInteractionType();
}
