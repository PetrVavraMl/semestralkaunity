using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyAI : MonoBehaviour
{
    // Start is called before the first frame update
    float x;
    float y;
    public float jumpHeight;
    public int health;
    public bool isInAir;
    
    void Start()
    {
        jumpHeight = 50;
        isInAir = false;
        health = 100;
        GetComponent<Rigidbody2D>().freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void TakeDamage(int damage,Collider2D collider)
    {
        health -= damage;
        GetComponent<Rigidbody2D>().AddForce(transform.TransformDirection(Vector3.up) * 100);
        float x1 = collider.transform.position.x;
        float y1 = collider.transform.position.y;
        if (transform.position.x < x1)
        {
            //transform.position = new Vector3(transform.position.x -5, transform.position.y, 0);
            //Vector2 endPos = new Vector2(transform.position.x - 2, transform.position.y);  
            //StartCoroutine(LerpPosition(2,this,endPos))
            GetComponent<Rigidbody2D>().AddForce(transform.TransformDirection(Vector3.left) * 150);
        }
        if (transform.position.x > x1) {
            GetComponent<Rigidbody2D>().AddForce(transform.TransformDirection(Vector3.right) * 150);
        }

        Debug.Log("ENEMY DAMAGE TAKEN");
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // dod�lat animaci smrti
        Debug.Log("enemy is DEAD");
        Destroy(this.gameObject);
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
            RaycastHit2D rcHitLeft = Physics2D.Raycast(transform.position,Vector2.left);
            RaycastHit2D rcHitRight = Physics2D.Raycast(transform.position, Vector2.right);
            if (rcHitLeft != null && isInAir == false) {
                float distance = Mathf.Abs(transform.position.x - rcHitLeft.point.x);
                if (distance <= 10) {
                    GetComponent<Rigidbody2D>().AddForce(transform.TransformDirection(Vector3.up) * jumpHeight);
                }
            }
            if (rcHitRight != null && isInAir == false)
            {
                float distance = Mathf.Abs(transform.position.x - rcHitRight.point.x);
                if (distance <= 10)
                {
                    GetComponent<Rigidbody2D>().AddForce(transform.TransformDirection(Vector3.up) * jumpHeight);
                }
            }
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
    IEnumerator LerpPosition(float duration, GameObject collisionObjekt,Vector2 endPosition)
    {
        float time = 0;
        Vector2 startPosition = collisionObjekt.transform.position;
        while (time < duration)
        {
            collisionObjekt.transform.position = Vector2.Lerp(startPosition, endPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        collisionObjekt.transform.position = transform.position;
        Destroy(collisionObjekt);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.name == "ground_collider" || collision.gameObject.name == "tilemap_collider")
        {
            isInAir = false;
        }

    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name == "ground_collider" || collision.gameObject.name == "tilemap_collider")
        {
            isInAir = true;
        }

    }
}
