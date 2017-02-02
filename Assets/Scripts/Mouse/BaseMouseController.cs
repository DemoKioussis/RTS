using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMouseController : MonoBehaviour {

    protected Vector3 positionClick,positionCurrent,positionRelease;
    bool firstClick = false;
    bool mouseHeld = false;
    public float doubleClickTime;
    float timeAtFirstClick;
    public float holdCheckTime;
    float holdTime = 0;
    
    [SerializeField] KeyCode mouseButton; //323 for leftClick, 324 for right

    void Update() {
        if (Input.GetKeyDown(mouseButton)){
            holdTime = 0;
            findPosition(ref positionClick);
            mouseClick();
        }
        checkSingleClick();

        if (Input.GetKey(mouseButton)) {
            holdTime += Time.deltaTime;
            if (holdTime > holdCheckTime)
            {
                findPosition(ref positionCurrent);
                mouseHold();
            }
        }
        if (Input.GetKeyUp(mouseButton))
        {
            findPosition(ref positionRelease);
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


    protected virtual void singleClick() {

    }
    protected virtual void doubleClick() {
    }

    protected virtual void mouseHold() {
    }

    protected virtual void mouseRelease()
    {

    }


}
