using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderGround : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (
            collision.gameObject.name == "tilemap_collider"
            || collision.gameObject.name == "platform1"
            || collision.gameObject.name == "platform2"
            )
        {
            MovementScript.isInAir = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (
            collision.gameObject.name == "tilemap_collider"
            || collision.gameObject.name == "platform1"
            || collision.gameObject.name == "platform2"
            )
        {
            MovementScript.isInAir = true;
        }
    }


}
