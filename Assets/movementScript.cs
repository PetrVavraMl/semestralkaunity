using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movementScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector2 vectorMove;
    public float speed = 10;
    public int jumpHeight = 70000;
    public bool isInAir;
    public Vector2 currentInputVector;
    public RuntimeAnimatorController controllerRun;
    public RuntimeAnimatorController controllerJump;
    public RuntimeAnimatorController controllerIdle;
    SpriteRenderer spriteRenderer;
    Animator animator;
    //public Vector2 vectorMove;
    Rigidbody2D rigidBody;

    void Start()
    {
        vectorMove = transform.position;
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.freezeRotation = true;
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {


        //klepe se geometrie okolo = FEATURE:) 
        if (Input.GetKeyDown(KeyCode.Space) && isInAir == false)
        {
            animator.runtimeAnimatorController = controllerJump;
            vectorMove.y = vectorMove.y + jumpHeight * 2;
            //transform.position = vectorMove;
            rigidBody.AddForce(transform.TransformDirection(Vector3.up) * jumpHeight);
            //transform.position = Vector2.Lerp(transform.position, vectorMove,(float)0.02);

            isInAir = true;

        }
        //if (Input.GetKey(KeyCode.S))
        //{
        //    vectorMove.y = vectorMove.y - (float)speed;
        //    transform.position = vectorMove;
        //}
    }
    private void FixedUpdate()
    {
        vectorMove = transform.position;
        if (Input.GetKey(KeyCode.A))
        {

            spriteRenderer.flipX = true;

            if (isInAir)
            {
                animator.runtimeAnimatorController = controllerJump;
            }
            else
            {
                animator.runtimeAnimatorController = controllerRun;
            }
            vectorMove.x = vectorMove.x - (speed * Time.deltaTime);
            //GetComponent<Rigidbody2D>().AddForce(transform.TransformDirection(Vector3.left) * (2));
            transform.position = vectorMove;
            //transform.position += (Vector3.left *speed * Time.deltaTime);  možná lepší øešení fakt nwm
            //transform.position = Vector2.Lerp(transform.position, vectorMove, .2f); nechapu

        }
        else

        if (Input.GetKey(KeyCode.D))
        {
            spriteRenderer.flipX = false;
            if (isInAir)
            {
                animator.runtimeAnimatorController = controllerJump;
            }
            else
            {
                animator.runtimeAnimatorController = controllerRun;
            }
            vectorMove.x = vectorMove.x + (speed * Time.deltaTime);
            //GetComponent<Rigidbody2D>().AddForce(transform.TransformDirection(Vector3.right) * (2));
            transform.position = vectorMove;
        }
        else
        {
            if (isInAir)
            {
                animator.runtimeAnimatorController = controllerJump;
            }
            else
            {
                animator.runtimeAnimatorController = controllerIdle;
            }


        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //pøi kolizi se zemí nebo s koliderem tilemapy nastav jeVeVzduchu na false
        if (collision.gameObject.name == "ground_collider" || collision.gameObject.name == "tilemap_collider")
        {
            isInAir = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        //pøi kolizi s objektem diamant ho smaž
        if (collision.gameObject.name == "gem1")
        {
            Destroy(collision.gameObject);
        }

        string objectName = collision.gameObject.name.Substring(0, 4);
        Debug.Log("Nazev objektu: " + objectName);
        if (objectName.Equals("coin"))
        {
            Debug.Log("Je to mince");
            Destroy(collision.gameObject);
        }
    }


}


