using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D physic;

    [SerializeField] private Transform player; 

    public GameObject enemy;
    public Animator animator;
    private SpriteRenderer spriteRenderer;

    public int maxHealth = 100;
    private int currentHealth;

    public float speed;
    public float agroDistance;
    // Start is called before the first frame update
    void Start()
    {
        physic = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        float distToPlayer = Vector2.Distance(transform.position, player.position);

        if (distToPlayer <= agroDistance)
        {
            StartHuting();
        }
        else
        {
            StopHunting();
        }
    }

    public void TakeDamage(int  damage)
    { 
        currentHealth -= damage;

        animator.SetTrigger("Hurt");

        if (currentHealth <= 0)
            Die();
    }

    void Die()
    {
        Destroy(enemy);
    }
    private void StartHuting() 
    {
        if (player.position.x < transform.position.x)
        {
            physic.velocity = new Vector2(-speed, 0);
            spriteRenderer.flipX = true;
        }

        else if (player.position.x > transform.position.x)
        {
            physic.velocity = new Vector2(speed, 0);
            spriteRenderer.flipX = false;
        }
    }
    private void StopHunting()
    {
        physic.velocity = new Vector2(0, 0);
    }
}
