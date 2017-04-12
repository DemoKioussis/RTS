using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempCamraFix : MonoBehaviour {

    public float speed;

    void Update() {
        float y = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        float x = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        float z = 0;
        if (Input.GetKeyDown(KeyCode.U))
            z = speed;
        if (Input.GetKeyDown(KeyCode.I))
            z = -speed;

        Quaternion rotation = transform.rotation;
        transform.rotation = Quaternion.identity;
        transform.Translate(new Vector3(x, z, y));
        transform.rotation = rotation;
    }
}
