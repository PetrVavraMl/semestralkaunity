using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatLogic : MonoBehaviour
{
    // Start is called before the first frame update
    public static int attackDamage;
    public Transform hitArea;
    public static float attackRange = 1.5f;
    public LayerMask enemyLayers;
    public float attackSpeed = 2f;
    float nextAttackTime = 0f;
    public Animator animator;
    void Start()
    {
        attackDamage = 25;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            Vector2 posCollider = GetComponentInParent<BoxCollider2D>().transform.position;
            hitArea.transform.position = new Vector2(posCollider.x + 1f, posCollider.y);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            Vector2 posCollider = GetComponentInParent<BoxCollider2D>().transform.position;
            Debug.Log("X:" + posCollider.x);
            hitArea.transform.position = new Vector2(posCollider.x - 1f, posCollider.y);
        }

        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Debug.Log("ATTACK");
                animator.SetTrigger("attackTrigger");
                Attack();
                nextAttackTime = Time.time + 1f / attackSpeed;
            }
        }

    }

    void Attack()
    {
        //získá všechny collidery které jsou v hit oblasti a vloží je do pole
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(hitArea.position, attackRange, enemyLayers);
        for (int i = 0; i < hitEnemies.Length; i++)
        {
            if (hitEnemies[i] != null)
            {
                //zjistí o který typ nepøítele se jedná a zavolá jeho metodu TakeDamage()
                Debug.Log("TREFIL: " + hitEnemies[i].name);
                if (hitEnemies[i].GetComponentInParent<EnemyAI>() != null)
                {
                    hitEnemies[i].GetComponentInParent<EnemyAI>().TakeDamage(attackDamage, GetComponent<Collider2D>());
                }
                if (hitEnemies[i].GetComponentInParent<Enemy2AI>() != null)
                {
                    hitEnemies[i].GetComponentInParent<Enemy2AI>().TakeDamage(attackDamage, GetComponent<Collider2D>());
                }
                if (hitEnemies[i].GetComponentInParent<BossScript>() != null)
                {
                    hitEnemies[i].GetComponentInParent<BossScript>().TakeDamage(attackDamage);
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (hitArea == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(hitArea.position, attackRange);
    }
}
