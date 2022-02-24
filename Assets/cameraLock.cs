using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraLock : MonoBehaviour
{

    public GameObject player;
    public GameObject background;
    public float offset;
    public float x;
    public float y;
    private float smoothTime = 0.9f;
    private Vector3 velocity = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        x = player.transform.position.x;
        y = player.transform.position.y;
        transform.position = new Vector3(x, y + offset, -15);
        transform.position = Vector3.SmoothDamp(new Vector3(transform.position.x, transform.position.y, -15), new Vector3(x, y, -15), ref velocity, smoothTime);
    }
}
