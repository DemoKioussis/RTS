using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour {

    Vector3 positionClick,positionCurrent,positionRelease;
    
    bool firstClick = false;
    bool mouseHeld = false;
    public float doubleClickTime;
    float timeAtFirstClick;
    void Update() {
        if (Input.GetKeyDown(KeyCode.Mouse0)){
            mouseClick();
        }
        checkSingleClick();

        if (Input.GetKey(KeyCode.Mouse0)) {
            mouseHold();
        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            mouseRelease();
        }

    }

    void findPosition(ref Vector3 p)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            p = hit.point;
        }
    }
    void mouseClick() {
        findPosition(ref positionClick);

        if (!firstClick)
        {
            firstClick = true;
            timeAtFirstClick = Time.time;
        }
        else if (firstClick && Time.time - timeAtFirstClick < doubleClickTime)
        {
            firstClick = false;
            timeAtFirstClick = 0;
            doubleClick();
        }
    }
    void checkSingleClick() {
        if (firstClick && Time.time - timeAtFirstClick > doubleClickTime)
        {
            firstClick = false;
            singleClick();
        }
    }
    void singleClick() {

    }
    void doubleClick() {
    }

    void mouseHold() {
        findPosition(ref positionCurrent);
    }

    void mouseRelease()
    {
        findPosition(ref positionRelease);

    }

}
