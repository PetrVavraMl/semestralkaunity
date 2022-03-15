using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    // Start is called before the first frame update
    float x;
    float y;
    public float jumpHeight;
    public int health;
    public bool isInAir;
    ParticleSystem particleBlood;
    ParticleSystem particleDeath;
    public Animator animator;
    bool canMove;
    public BoxCollider2D colliderPlayerMain;
    public BoxCollider2D colliderPlayerBottom;
    public bool isAlive;




    void Start()
    {
        //najde objekty particle systémù - pouští se pomocí metody Die() a TakeDamage() 
        Transform trBlood = transform.Find("ParticleHit");
        Transform trDeath = transform.Find("ParticleDeath");
        particleDeath = trDeath.GetComponent<ParticleSystem>();
        particleBlood = trBlood.GetComponent<ParticleSystem>();
        canMove = true;

        jumpHeight = 50;
        isAlive = true;
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
        //TODO death animace
        health -= damage;
        particleBlood.Play();
        GetComponent<Rigidbody2D>().AddForce(transform.up * 0.2f, ForceMode2D.Impulse);
        float x1 = collider.transform.position.x;
        float y1 = collider.transform.position.y;
        Vector2 difference = (transform.position - collider.transform.position).normalized;
        Vector2 force = difference * 2;
        GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);
        //if (transform.position.x < x1)
        //{
           
            //transform.position = new Vector3(transform.position.x -5, transform.position.y, 0);
            //Vector2 endPos = new Vector2(transform.position.x - 2, transform.position.y);  
            //StartCoroutine(LerpPosition(2,this,endPos))
            //GetComponent<Rigidbody2D>().AddForce(transform.TransformDirection(Vector3.left) * 150);
        //}
        //if (transform.position.x > x1) {
        //    Vector2 difference = (transform.position - collider.transform.position).normalized;
        //    Vector2 force = difference * 5;
        //    GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);
        //    //GetComponent<Rigidbody2D>().AddForce(transform.TransformDirection(Vector3.right) * 150);
        //}

        Debug.Log("ENEMY DAMAGE TAKEN");
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("enemy is DEAD");
        StartCoroutine(AnimationDieWait(0.7f));
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
        if (collision.gameObject.name.Equals("Player") && MovementScript.isAlive == true && canMove)
        {
            RaycastHit2D rcHitLeft = Physics2D.Raycast(transform.position,Vector2.left);
            RaycastHit2D rcHitRight = Physics2D.Raycast(transform.position, Vector2.right);
            if (rcHitLeft != null && isInAir == false) {
                float distance = Mathf.Abs(transform.position.x - rcHitLeft.point.x);
                if (distance <= 0.2) {
                    GetComponent<Rigidbody2D>().AddForce(transform.TransformDirection(Vector3.up) * jumpHeight);
                }
            }
            if (rcHitRight != null && isInAir == false)
            {
                float distance = Mathf.Abs(transform.position.x - rcHitRight.point.x);
                if (distance <= 0.2)
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

   
    public IEnumerator AnimationDieWait(float duration)
    {
        
        animator.SetBool("isAlive",false);
        animator.SetTrigger("OnDeath");
        if (isAlive)
        {
            particleDeath.Play();
        }
        isAlive = false;

        Transform trBody = transform.Find("ColliderBody");
        Transform trBottom = transform.Find("ColliderGround");
        //Transform colGround = transform.Find("ColliderGround");

        BoxCollider2D collMain = trBody.GetComponent<BoxCollider2D>();
        BoxCollider2D collBottom = trBottom.GetComponent<BoxCollider2D>();
        Physics2D.IgnoreCollision(colliderPlayerMain, collMain);
        Physics2D.IgnoreCollision(colliderPlayerBottom, collMain);

        Physics2D.IgnoreCollision(colliderPlayerMain, collBottom);
        Physics2D.IgnoreCollision(colliderPlayerBottom, collBottom);



        //colGround.GetComponent<BoxCollider2D>().isTrigger = true;
        canMove = false;
        float time = 0;
        while (time < duration)
        {
            time += Time.deltaTime;
            yield return null;
        }
        //Destroy(this.gameObject);


    }
}
