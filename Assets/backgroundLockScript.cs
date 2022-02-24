using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backgroundLockScript : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject camera;
    float x;
    float y;
    void Start()
    {
        camera = GameObject.Find("Camera");
    }

    // Update is called once per frame
    void Update()
    {
        x = camera.transform.position.x;
        y = camera.transform.position.y;
        transform.position = new Vector3(x, y, 5);
    }
}
