using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class movementScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector2 vectorMove;
    public float speed = 10;
    public int jumpHeight = 700;
    public bool isInAir;
    public Vector2 currentInputVector;
    public RuntimeAnimatorController controllerRun;
    public RuntimeAnimatorController controllerJump;
    public RuntimeAnimatorController controllerIdle;
    public RuntimeAnimatorController controllerHit;
    public RuntimeAnimatorController controllerDie;
    SpriteRenderer spriteRenderer;
    Animator animator;
    //public Vector2 vectorMove;
    Rigidbody2D rigidBody;
    public int healthPoints = 100;
    public bool isInvincible = false;
    public float invincibilityDurationSeconds = (float)1;
    public Text scoreText;
    public Text healthText;
    public int score = 0;
    public ParticleSystem particleRun;
    public GameObject particleSystemRun;
    public ParticleSystem particleRunRight;
    public GameObject particleSystemRunRight;
    public int offset = 1;
    public bool isParticlePlaying = false;
    public static bool isAlive = true;



    void Start()
    {
        vectorMove = transform.position;
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.freezeRotation = true;
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        healthText = GameObject.Find("HealthText").GetComponent<Text>();
        particleRun = GameObject.Find("ParticleRun").GetComponent<ParticleSystem>();
        particleSystemRun = GameObject.Find("ParticleRun");
        particleRunRight = GameObject.Find("ParticleRunRight").GetComponent<ParticleSystem>();
        particleSystemRunRight = GameObject.Find("ParticleRunRight");
        Debug.Log("RESTART");
        isAlive = true;


    }

    // Update is called once per frame
    void Update()
    {

        if (healthPoints <= 0)
        {
            isAlive = false;



        }

        //klepe se geometrie okolo = FEATURE:) 
        if (Input.GetKeyDown(KeyCode.Space) && isInAir == false && isAlive)
        {
            animator.runtimeAnimatorController = controllerJump;
            vectorMove.y = vectorMove.y + jumpHeight * 2;
            //transform.position = vectorMove;
            rigidBody.AddForce(transform.TransformDirection(Vector3.up) * jumpHeight);
            //transform.position = Vector2.Lerp(transform.position, vectorMove,(float)0.02);

            //stará mechanika isInAir
            //isInAir = true;

        }

        //resetování scény
        if (Input.GetKeyDown(KeyCode.P))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
        //pokud není hráè mrtvý mùže se spustit skript, pokud je mrtvý spustí se animace smrti
        if (isAlive)
        {


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
                    particleSystemRunRight.transform.position = new Vector2(transform.position.x, transform.position.y - offset);
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
                    particleSystemRun.transform.position = new Vector2(transform.position.x, transform.position.y - offset);
                    //particleRun.GetComponent<ParticleSystemRenderer>().flip = new Vector3(0,0,0);
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

            //zapnutí a vypnutí animace particlù pøi bìhu
            if (Input.GetKey(KeyCode.A))
            {
                particleRunRight.enableEmission = true;
            }
            else
            {
                particleRunRight.enableEmission = false;
            }

            if (Input.GetKey(KeyCode.D))
            {
                particleRun.enableEmission = true;
            }
            else
            {
                particleRun.enableEmission = false;
            }
        }
        else
        {
            animator.runtimeAnimatorController = controllerDie;
            particleRunRight.enableEmission = false;
            particleRun.enableEmission = false;
        }





    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //pøi kolizi se zemí nebo s koliderem tilemapy nastav jeVeVzduchu na false


        if (collision.gameObject.name.Equals("Enemy") && isAlive)
        {
            //odhození hráèe pøi kolizi s nepøítelem
            GameObject enemyObject = collision.gameObject;
            rigidBody.AddForce(transform.TransformDirection(Vector3.up) * 400);

            if (enemyObject.transform.position.x > transform.position.x)
            {
                rigidBody.AddForce(transform.TransformDirection(Vector3.left) * 300);

            }
            if (enemyObject.transform.position.x < transform.position.x)
            {
                rigidBody.AddForce(transform.TransformDirection(Vector3.right) * 300);

            }

            LoseHealth(10);
            healthText.text = "Health: " + healthPoints;


        }
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
    private void OnTriggerEnter2D(Collider2D collision)
    {

        //pøi kolizi s objektem diamant ho smaž
        if (collision.gameObject.name == "gem1")
        {
            score += 25;
            scoreText.text = "Score: " + score;
            //spus coroutine pro vyhlazení pohybu gemu k hráèi - coroutina na konci objekt smaže
            StartCoroutine(LerpPosition(0.2f, collision.gameObject));
        }

        //pøi kolizi s mincí ji smaž
        string objectName = collision.gameObject.name.Substring(0, 4);
        Debug.Log("Nazev objektu: " + objectName);
        if (objectName.Equals("coin"))
        {
            Debug.Log("Je to mince");
            score += 10;
            scoreText.text = "Score: " + score;
            //spus coroutine pro vyhlazení pohybu mince k hráèi - coroutina na konci objekt smaže
            StartCoroutine(LerpPosition(0.3f, collision.gameObject));






        }
    }



    private void LoseHealth(int ammount)
    {
        if (!isInvincible)
        {
            Debug.Log("HIT -10 HP");
            healthPoints -= ammount;
            StartCoroutine(BecomeInvincible());
        }

    }

    private IEnumerator BecomeInvincible()
    {
        Debug.Log("HRAC JE NESMRTELNY");

        isInvincible = true;
        animator.runtimeAnimatorController = controllerHit;

        yield return new WaitForSeconds(invincibilityDurationSeconds);

        isInvincible = false;
        Debug.Log("HRAC NENI NESMRTELNY!!!!");
    }

    IEnumerator LerpPosition(float duration, GameObject collisionObjekt)
    {
        float time = 0;
        Vector2 startPosition = collisionObjekt.transform.position;
        while (time < duration)
        {
            collisionObjekt.transform.position = Vector2.Lerp(startPosition, transform.position, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        collisionObjekt.transform.position = transform.position;
        Destroy(collisionObjekt);
    }

   

    


}


