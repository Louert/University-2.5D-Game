using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Transform attackPoint;
    public Transform Point;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    int attackDamage = 20;
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public Animator animator;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Attack();
        }

        void Attack()
        {
            if (spriteRenderer.flipX)
            {
                attackPoint.localPosition = new Vector3(-(Point.localPosition.x), Point.localPosition.y, Point.localPosition.z);
            }
            else if (!spriteRenderer.flipX)
            { 
                attackPoint.localPosition = new Vector3(Point.localPosition.x, Point.localPosition.y, Point.localPosition.z);
            }

            animator.SetTrigger("Attack");

            Collider2D[] hitEnemys =  Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

            foreach (Collider2D enemy in hitEnemys)
            {
                enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
            }

        }
        
    }
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
