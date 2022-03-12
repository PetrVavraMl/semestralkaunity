using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatLogic : MonoBehaviour
{
    // Start is called before the first frame update
    public int attackDamage = 10;
    public Transform hitArea;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public float attackSpeed = 2f;
    float nextAttackTime = 0f;
    public Animator animator;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                animator.SetTrigger("attackTrigger");
                Attack();
                nextAttackTime = Time.time + 1f / attackSpeed;
            }
        }
        
    }

    void Attack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(hitArea.position, attackRange, enemyLayers);
        for (int i = 0; i < hitEnemies.Length; i++)
        {
            if (hitEnemies[i] != null)
            {
                Debug.Log("TREFIL: " + hitEnemies[i].name);
                hitEnemies[i].GetComponentInParent<enemyAI>().TakeDamage(10, GetComponent<Collider2D>());
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
