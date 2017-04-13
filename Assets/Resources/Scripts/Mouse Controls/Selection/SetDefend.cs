using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetDefend : MonoBehaviour {

    SelectionComponent selection;
    void Awake() {
        selection = GetComponent<SelectionComponent>();
    }
    void Update() {
        if (Input.GetKeyDown(KeyCode.RightShift))
        {

            Debug.Log("PRESSED DEFENCE!");
            if (selection.selectedUnitGroup!= null)
                selection.selectedUnitGroup.setDefensive();
        }
    }
}
