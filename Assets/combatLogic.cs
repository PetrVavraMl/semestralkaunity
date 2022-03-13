using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatLogic : MonoBehaviour
{
    // Start is called before the first frame update
    public int attackDamage = 10;
    public Transform hitArea;
    public float attackRange = 1.5f;
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
        if (Input.GetKeyDown(KeyCode.D))
        {
            Vector2 posCollider = GetComponentInParent<BoxCollider2D>().transform.position;
            hitArea.transform.position = new Vector2(posCollider.x + 1f, posCollider.y);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            Vector2 posCollider = GetComponentInParent<BoxCollider2D>().transform.position;
            Debug.Log("X:"+posCollider.x);
            hitArea.transform.position = new Vector2(posCollider.x - 1f,posCollider.y);
        }

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
