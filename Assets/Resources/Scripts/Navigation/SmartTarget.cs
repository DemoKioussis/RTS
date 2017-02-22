using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartTarget : MonoBehaviour {

    private int followers = 0;
    public GameObject beginAnimation; // this will be something that dissapears after a second, to mark where it was clicked
    private bool firstFrame;
    void Start() {
        firstFrame = true;
        if(beginAnimation!=null)
            Instantiate(beginAnimation, transform.position, Quaternion.identity);
    }

    void Update() {
        Debug.Log("Num Of Followers: " + followers);
        if (followers == 0 && !firstFrame) {
            Destroy(this.gameObject);
        }
        firstFrame = false;
    }
    public void addFollower() {
        followers++;
    }

    public void addFollower(int num) {
        followers += num;
    }
    public void removeFollower() {
        followers--;
    }
    public void removeFollower(int num) {
        followers -= num;
    }

    public Vector3 getPosition() {
        return transform.position;
    }
}
