using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyAI : MonoBehaviour
{
    // Start is called before the first frame update
    float x;
    float y;
    void Start()
    {
        GetComponent<Rigidbody2D>().freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {
            //float x = collision.transform.position.x;
            //float y = collision.transform.position.y;
            //transform.position = new Vector2(x - 50, y);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player") && movementScript.isAlive == true)
        {
            x = collision.transform.position.x;
            y = collision.transform.position.y;

            if (transform.position.x > x)
            {
                transform.position = new Vector2(transform.position.x - (float)0.05, transform.position.y);

            }
            else if (transform.position.x < x)
            {
                transform.position = new Vector2(transform.position.x + (float)0.05, transform.position.y);

            }

        }

    }
}
