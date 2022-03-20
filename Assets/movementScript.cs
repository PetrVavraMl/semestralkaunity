using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MovementScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector2 vectorMove;
    public float speed = 10;
    public int jumpHeight = 700;
    public Vector2 currentInputVector;

    SpriteRenderer spriteRenderer;
    //public Animator animator;
    //public Vector2 vectorMove;
    Rigidbody2D rigidBody;
    public int healthPoints = 100;
    public bool isInvincible = false;
    public float invincibilityDurationSeconds = 1;
    public Text scoreText;
    public Text healthText;
    public int score = 0;
    public ParticleSystem particleRun;
    public GameObject particleSystemRun;
    public ParticleSystem particleRunRight;
    public GameObject particleSystemRunRight;
    public int offset = 1;
    public bool isParticlePlaying = false;
    public static bool isInAir;
    public static bool isAlive = true;
    public static bool isRunning;
    public static bool canMove;
    public int level;
    public bool isAttacking = false;
    public Animator animator;
    public float knockbackStrength;
    public BoxCollider2D colliderMain;
    public BoxCollider2D colliderBottom;
    public SpriteRenderer map1SpriteRenderer;
    public SpriteRenderer map2SpriteRenderer;


    public void SavePlayer()
    {
        SystemSave.SavePlayer(this);
    }

    public void LoadPlayer()
    {
        PlayerSave loadedData = SystemSave.LoadPlayer();
        healthPoints = loadedData.healthPoints;
        score = loadedData.score;
        transform.position = new Vector3(loadedData.position[0], loadedData.position[1], loadedData.position[2]);
        scoreText.text = "Score: " + score;
        healthText.text = "Health: " + healthPoints;
    }

    void Start()
    {
        level = 1;
        canMove = true;
        knockbackStrength = 10;
        vectorMove = transform.position;
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.freezeRotation = true;
        spriteRenderer = GetComponent<SpriteRenderer>();
        scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        healthText = GameObject.Find("HealthText").GetComponent<Text>();
        particleRun = GameObject.Find("ParticleRun").GetComponent<ParticleSystem>();
        particleSystemRun = GameObject.Find("ParticleRun");
        particleRunRight = GameObject.Find("ParticleRunRight").GetComponent<ParticleSystem>();
        particleSystemRunRight = GameObject.Find("ParticleRunRight");
        Debug.Log("RESTART");
        isAlive = true;
        checkBackground();


    }

    // Update is called once per frame
    void Update()
    {
        if (healthPoints <= 0)
        {
            isAlive = false;



        }


        if (Input.GetKeyDown(KeyCode.Space) && isInAir == false && isAlive && canMove)
        {
            vectorMove.y = vectorMove.y + jumpHeight * 2;
            rigidBody.AddForce(transform.TransformDirection(Vector3.up) * jumpHeight);
        }

        //resetov·nÌ scÈny
        if (Input.GetKeyDown(KeyCode.P))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

    }
    private void FixedUpdate()
    {


        vectorMove = transform.position;
        //pokud nenÌ hr·Ë mrtv˝ m˘ûe se spustit skript
        if (isAlive && canMove)
        {



            if (Input.GetKey(KeyCode.A))
            {
                isRunning = true;
                spriteRenderer.flipX = true;

                if (!isInAir)
                {
                    particleSystemRunRight.transform.position = new Vector2(transform.position.x, transform.position.y - offset);
                }

                vectorMove.x = vectorMove.x - (speed * Time.deltaTime);
                transform.position = vectorMove;

            }
            else

            if (Input.GetKey(KeyCode.D))
            {
                isRunning = true;
                spriteRenderer.flipX = false;
                if (!isInAir)
                {
                    particleSystemRun.transform.position = new Vector2(transform.position.x, transform.position.y - offset);
                }

                vectorMove.x = vectorMove.x + (speed * Time.deltaTime);
                transform.position = vectorMove;
            }
            else
            {
                isRunning = false;
            }

            //zapnutÌ a vypnutÌ animace particl˘ p¯i bÏhu
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
            particleRunRight.enableEmission = false;
            particleRun.enableEmission = false;
        }





    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        string objName = collision.gameObject.name.Substring(0, 10);
        //kolize s nep¯Ìtelem
        if (objName.Equals("EnemySlime") && isAlive)
        {
            animator.SetTrigger("hitTrigger");
            //odhozenÌ hr·Ëe p¯i kolizi s nep¯Ìtelem
            GameObject enemyObject = collision.gameObject;
            //rigidBody.AddForce(transform.TransformDirection(Vector3.up) * 400);

            if (enemyObject.transform.position.x > transform.position.x)
            {
                Vector2 difference = (transform.position - collision.transform.position).normalized;
                Vector2 force = difference * knockbackStrength;
                rigidBody.AddForce(force, ForceMode2D.Impulse);
                //rigidBody.AddForce(transform.TransformDirection(Vector3.left) * 300);

            }
            if (enemyObject.transform.position.x < transform.position.x)
            {
                Vector2 difference = (transform.position - collision.transform.position).normalized;
                Vector2 force = difference * knockbackStrength;
                rigidBody.AddForce(force, ForceMode2D.Impulse);
                //rigidBody.AddForce(transform.TransformDirection(Vector3.right) * 300);

            }

            LoseHealth(10);
            healthText.text = "Health: " + healthPoints;
        }
    }


    //nahrazeno dalsim colliderem a skriptem colliderGround.cs
    //private void OnCollisionStay2D(Collision2D collision)
    //{
    //    //Debug.Log("kolider: " + collision.collider);
    //    //if (collision.collider == colliderGround)
    //    //{
    //    //    Debug.Log("Dotykam se zeme");
    //    //    if (collision.gameObject.name == "ground_collider" || collision.gameObject.name == "tilemap_collider")
    //    //    {
    //    //        isInAir = false;


    //    //    }
    //    //}


    //}
    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    if (collision.collider == colliderGround)
    //    {
    //        if (collision.gameObject.name == "ground_collider" || collision.gameObject.name == "tilemap_collider")
    //        {
    //            isInAir = true;


    //        }
    //    }

    //}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //BUG - obËas se zapoËÌt· coin 2 kr·t, pravdÏpodobnÏ se d· vy¯eöit ignorov·nÌm kolize(Physics2D.IgnoreCollision)
        if (collision != null)
        {
            string objectName = collision.gameObject.name.Substring(0, 3);
            //p¯i kolizi s objektem diamant ho smaû
            if (objectName.Equals("gem"))
            {
                score += 25;
                scoreText.text = "Score: " + score;
                //spusù coroutine pro vyhlazenÌ pohybu gemu k hr·Ëi - coroutina na konci objekt smaûe
                StartCoroutine(LerpPosition(0.2f, collision.gameObject));
            }

            //p¯i kolizi s mincÌ ji smaû
            Debug.Log("Nazev objektu: " + objectName);
            if (objectName.Equals("coi") && collision != null)
            {
                Transform soundCoin = transform.Find("SoundCoin");
                soundCoin.GetComponent<AudioSource>().Play();
                Debug.Log("Je to mince");
                score += 10;
                scoreText.text = "Score: " + score;
                //spusù coroutine pro vyhlazenÌ pohybu mince k hr·Ëi - coroutina na konci objekt smaûe
                StartCoroutine(LerpPosition(0.3f, collision.gameObject));
            }
            if (collision.gameObject.name.Equals("DropCollider"))
            {
                SceneManager.LoadScene(sceneName: "MainGame");
            }
            //teleport do dalöÌ mapy, zmÏna levelu na 2
            if (collision.gameObject.name.Equals("portal-bottom"))
            {
                Debug.Log("PORTAL:!");
                transform.position = new Vector2(0, -56);
                level = 2;
                //zavol· metodu check background kter· nastavÌ pozadÌ dle mapy
                checkBackground();
            }
        }


    }

    private void checkBackground()
    {
        map1SpriteRenderer.enabled = false;
        map2SpriteRenderer.enabled = false;
        switch (level)
        {
            case 1:
                map1SpriteRenderer.enabled = true;

                break;
            case 2:
                map2SpriteRenderer.enabled = true;
                break;
            case 3:
                break;
            default:
                break;
        }
    }

    private void LoseHealth(int ammount)
    {
        if (!isInvincible)
        {
            Transform soundCoin = transform.Find("SoundHurt");
            soundCoin.GetComponent<AudioSource>().Play();
            Debug.Log("HIT -10 HP");
            healthPoints -= ammount;
            StartCoroutine(BecomeInvincible());
        }
    }

    private IEnumerator BecomeInvincible()
    {
        isInvincible = true;
        yield return new WaitForSeconds(invincibilityDurationSeconds);
        isInvincible = false;
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

    IEnumerator AnimationAttackWait(float duration)
    {
        float time = 0;
        while (time < duration)
        {
            time += Time.deltaTime;
            yield return null;
        }
        isAttacking = false;

    }






}


