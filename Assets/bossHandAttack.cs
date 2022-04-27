using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossHandAttack : MonoBehaviour
{

    public Transform attackPoint;
    public int damage = 15;
    public float attackRange = 2f;
    public LayerMask attackMask;
    Transform player;
    MovementScript playerScript;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerScript = player.GetComponent<MovementScript>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Attack()
    {
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, attackMask);

        foreach (Collider2D player in hitPlayer)
        {
            playerScript.LoseHealth(damage);
            playerScript.pushPlayer(GameObject.FindGameObjectWithTag("Boss"));
            Debug.Log("HRAC DOSTAL HIT " + player.name);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
