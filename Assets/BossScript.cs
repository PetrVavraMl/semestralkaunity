using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform player;
    public bool isFlipped;
    int healthPoints = 50;
    public LayerMask maskRc;
    private bool isAlive = true;
    public bool canMove = false;
    public Animator animator;
    public Transform soundHurt;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        GetComponent<Rigidbody2D>().freezeRotation = true;
        animator.speed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (isAlive && canMove)
        {
            facePlayer();
            //kontroluje raycastem vzdálenost od zemì a vznáší bosse
            RaycastHit2D rcHitBottom = Physics2D.Raycast(transform.position, -Vector2.up, Mathf.Infinity, maskRc);
            if (rcHitBottom != null)
            {
                float distance = Mathf.Abs(rcHitBottom.point.y - transform.position.y);
                if (distance <= 4)
                {
                    GetComponent<Rigidbody2D>().AddForce(transform.TransformDirection(Vector3.up) * 2);
                }
            }
        }
    }

    public void AwakeBoss()
    {
        animator.speed = 1f;
    }

    public void TakeDamage(int damage)
    {
        if (isAlive)
        {
            animator.SetTrigger("TakeHitTrigger");
            Debug.Log("BOSS DAMAGE GIVEN");
            soundHurt.GetComponent<AudioSource>().Play();
            healthPoints -= damage;
            if (healthPoints <= 0)
            {
                Die();
            }
        }
    }

    private void Die()
    {
        animator.SetTrigger("DieTrigger");
        isAlive = false;
        player.GetComponent<MovementScript>().FinishTheGame();
    }

    public void facePlayer()
    {
        if (transform.position.x > player.position.x && isFlipped)
        {
            transform.Rotate(0, 180, 0);
            isFlipped = false;
        }
        else if (transform.position.x < player.position.x && !isFlipped)
        {
            transform.Rotate(0, 180, 0);
            isFlipped = true;
        }
    }
}
