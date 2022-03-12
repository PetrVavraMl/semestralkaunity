using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colliderGround : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.name == "tilemap_collider")
        {
            movementScript.isInAir = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name == "tilemap_collider")
        {
            movementScript.isInAir = true;
        }
    }


}
