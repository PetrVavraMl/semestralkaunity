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
    public SpriteRenderer map3SpriteRenderer;
    public AnimatorOverrideController overrideController;
    public Transform map1Music;
    public Transform map2Music;
    public Transform map3Music;
    public Animator animatorFade;
    private bool facingLeft;
    public Transform firePoint;
    public Text textHS;
    public Canvas canvasEnd;



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
        level = loadedData.level;
        checkLevel();
    }

    void Start()
    {
        canvasEnd.enabled = false;
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
        facingLeft = false;
        checkLevel();


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
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (!facingLeft)
            {
                flipFirePoint();
            }

        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (facingLeft)
            {
                flipFirePoint();

            }
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
                //flipPlayer();

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
                //flipPlayer();
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
                if (!isInAir)
                {
                    particleRunRight.enableEmission = true;
                }
                else
                {
                    particleRunRight.enableEmission = false;
                }
            }
            else
            {
                particleRunRight.enableEmission = false;
            }

            if (Input.GetKey(KeyCode.D))
            {
                if (!isInAir)
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
                particleRun.enableEmission = false;
            }
        }
        else
        {
            particleRunRight.enableEmission = false;
            particleRun.enableEmission = false;
        }





    }

    private void flipFirePoint()
    {
        facingLeft = !facingLeft;
        firePoint.transform.Rotate(0f, 180f, 0f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        string objName = collision.gameObject.name.Substring(0, 6);
        //kolize s nep¯Ìtelem
        if (objName.Equals("EnemyS") || objName.Equals("EnemyB") && isAlive)
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

        }

    }
    public void pushPlayer(GameObject enemyObject)
    {

        if (enemyObject.transform.position.x > transform.position.x)
        {
            Vector2 difference = (transform.position - enemyObject.transform.position).normalized;
            Vector2 force = difference * knockbackStrength;
            rigidBody.AddForce(force, ForceMode2D.Impulse);

        }
        if (enemyObject.transform.position.x < transform.position.x)
        {
            Vector2 difference = (transform.position - enemyObject.transform.position).normalized;
            Vector2 force = difference * knockbackStrength;
            rigidBody.AddForce(force, ForceMode2D.Impulse);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //BUG - obËas se zapoËÌt· coin 2 kr·t, pravdÏpodobnÏ se d· vy¯eöit ignorov·nÌm kolize(Physics2D.IgnoreCollision)
        if (collision != null)
        {
            string objectName = collision.gameObject.name.Substring(0, 3);

            //odhodÌ hr·Ëe


            //p¯i kolizi s objektem diamant ho smaû
            if (objectName.Equals("gem"))
            {
                Transform soundGem = transform.Find("SoundGem");
                soundGem.GetComponent<AudioSource>().Play();
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
                animatorFade.SetTrigger("onMapChange2");
                Debug.Log("PORTAL 1");
                Vector2 posPort = new Vector2(0, -56);
                //coroutina waitforteleport poËk· neû se ztmavÌ obrazovka, potÈ teleportuje hr·Ëe a zavol· metodu checkLevel()
                StartCoroutine(WaitForTeleport(posPort, 2, "onMapChange3"));
                level = 2;
                //zavol· metodu check level kter· nastavÌ pozadÌ,particly a animace dle mapy 

            }
            if (collision.gameObject.name.Equals("portal-bottom-2"))
            {
                animatorFade.SetTrigger("onMapChange2");
                Debug.Log("PORTAL 2");
                Vector2 posPort = new Vector2(-50, -144.5f);
                //coroutina waitforteleport poËk· neû se ztmavÌ obrazovka, potÈ teleportuje hr·Ëe a zavol· metodu checkLevel()
                StartCoroutine(WaitForTeleport(posPort, 2, "onMapChange3"));
                level = 3;
                //zavol· metodu check level kter· nastavÌ pozadÌ,particly a animace dle mapy
            }
            if (collision.gameObject.name.Equals("portal-bottom-3"))
            {
                animatorFade.SetTrigger("onMapChange2");
                Debug.Log("PORTAL 3");
                Vector2 posPort = new Vector2(400, -122);
                //coroutina waitforteleport poËk· neû se ztmavÌ obrazovka, potÈ teleportuje hr·Ëe a zavol· metodu checkLevel()
                StartCoroutine(WaitForTeleport(posPort, 2, "onMapChange3"));
                level = 4;
                //zavol· metodu check level kter· nastavÌ pozadÌ,particly a animace dle mapy
            }
        }


    }

    //metoda kter· zajiöùuje nastavenÌ atribut˘ hr·Ëe podle levelu(particly,animace,pozadÌ)
    private void checkLevel()
    {
        map1SpriteRenderer.enabled = false;
        map2SpriteRenderer.enabled = false;
        map3SpriteRenderer.enabled = false;
        map1Music.GetComponent<AudioSource>().Stop();
        map2Music.GetComponent<AudioSource>().Stop();
        map3Music.GetComponent<AudioSource>().Stop();
        switch (level)
        {
            case 1:
                map1SpriteRenderer.enabled = true;
                map1Music.GetComponent<AudioSource>().Play();
                break;
            case 2:
                map2SpriteRenderer.enabled = true;
                map2Music.GetComponent<AudioSource>().Play();
                Color colorGray = new Color(47 / 255f, 62 / 255f, 69 / 255f);
                particleRun.startColor = colorGray;
                particleRunRight.startColor = colorGray;
                animator.runtimeAnimatorController = overrideController;
                CombatLogic.attackDamage = 50;
                CombatLogic.attackRange = 2.5f;
                break;
            case 3:
                map3Music.GetComponent<AudioSource>().Play();
                CombatLogic.attackDamage = 50;
                CombatLogic.attackRange = 2.5f;
                Color colorYellow = new Color(227 / 255f, 173 / 255f, 80 / 255f);
                particleRunRight.startColor = colorYellow;
                particleRun.startColor = colorYellow;
                animator.runtimeAnimatorController = overrideController;
                map3SpriteRenderer.enabled = true;
                scoreText.color = Color.black;
                healthText.color = Color.black;
                break;
            case 4:
                map2SpriteRenderer.enabled = true;
                map2Music.GetComponent<AudioSource>().Play();
                Color colorGrayBoss = new Color(47 / 255f, 62 / 255f, 69 / 255f);
                particleRun.startColor = colorGrayBoss;
                particleRunRight.startColor = colorGrayBoss;
                animator.runtimeAnimatorController = overrideController;
                CombatLogic.attackDamage = 50;
                CombatLogic.attackRange = 2.5f;
                scoreText.color = Color.white;
                healthText.color = Color.white;
                GameObject.FindGameObjectWithTag("Boss").transform.GetComponent<BossScript>().AwakeBoss();
                break;
            case 5:

                break;
            default:
                break;
        }
    }

    public void FinishTheGame()
    {
        //canMove = false;
        StartCoroutine(WaitForEnd(6));
        StartCoroutine(WaitForCanvas(8, "onMapChange3"));

        Debug.Log("CANVAS: " + canvasEnd.enabled);
        Debug.Log("HRA DOKONCENA!");
        //naËte uloûenÈ skÛre a porovn· s nov˝m
        int oldHS = PlayerPrefs.GetInt("HS");
        if (oldHS < score)
        {
            Debug.Log("NOVE HS!!!");
            PlayerPrefs.SetInt("HS", score);
            string hsText = "You beat the last highscore with " + score + " points.";
            textHS.text = hsText;
        }
        else if (oldHS > score)
        {
            string hsText = "The highscore to beat is " + oldHS + " points.\n Your score is " + score + " points.";
            textHS.text = hsText;
        }
        else
        {
            string hsText = "You reached the same score as the current highscore of " + score + " points. \n Just short!";
            textHS.text = hsText;
        }


    }

    public void LoseHealth(int ammount)
    {
        if (!isInvincible)
        {
            Transform soundCoin = transform.Find("SoundHurt");
            soundCoin.GetComponent<AudioSource>().Play();
            Debug.Log("HIT -" + ammount + "HP");
            healthPoints -= ammount;
            StartCoroutine(BecomeInvincible());
            healthText.text = "Health: " + healthPoints;
        }
    }

    private IEnumerator BecomeInvincible()
    {
        isInvincible = true;
        yield return new WaitForSeconds(invincibilityDurationSeconds);
        isInvincible = false;
    }
    private IEnumerator WaitForTeleport(Vector2 pos, int seconds, string triggerParams)
    {
        yield return new WaitForSeconds(seconds);
        transform.position = pos;
        checkLevel();
        animatorFade.SetTrigger(triggerParams);
    }

    private IEnumerator WaitForCanvas(int seconds, string triggerParams)
    {
        yield return new WaitForSeconds(seconds);
        animatorFade.SetTrigger(triggerParams);
        canvasEnd.enabled = true;
    }

    private IEnumerator WaitForEnd(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        animatorFade.SetTrigger("onMapChange2");

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


