using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSelection : MonoBehaviour {

    public float speedX, speedY, speedZ;


	void Update () {
        transform.Rotate(speedX * Time.deltaTime, speedY * Time.deltaTime, speedZ * Time.deltaTime);		
	}
}
