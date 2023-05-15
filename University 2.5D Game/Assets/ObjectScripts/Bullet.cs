using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float lifetime = 2;
    [SerializeField] private float distance;
    [SerializeField] private int damage;
    [SerializeField] private LayerMask whatIsSolid;
    private SpriteRenderer spriteRenderer;
    private float flyTimer;



    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        flyTimer = lifetime;
    }

    // Update is called once per frame
    void Update()
    {
        flyTimer -= Time.deltaTime;

        if(flyTimer <= 0)
{
            Destroy(gameObject);
        }
        RaycastHit2D hitinfo = Physics2D.Raycast(transform.position, transform.up, distance, whatIsSolid);
        if (hitinfo.collider != null)
        {
            if (hitinfo.collider.CompareTag("Enemy"))
            { 
                hitinfo.collider.GetComponent<Enemy>().TakeDamage(damage);
            }
            Destroy(gameObject);
        }
        if (spriteRenderer.flipX)
        {
            spriteRenderer.flipX = true;
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
        else if (!spriteRenderer.flipX)
        {
            spriteRenderer.flipX = false;
            transform.Translate(Vector2.right * speed * Time.deltaTime);

        }

    }
}
