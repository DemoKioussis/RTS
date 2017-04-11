using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableLink : MonoBehaviour {

    [SerializeField]
    Interactable link;

    public Interactable getInteractable() {
        return link;
    }

}
