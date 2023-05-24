using System.Collections;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 10f;
    public bool isGrounded = false;
    public int score;
    public bool ult = false;
    public bool won = false;
    [SerializeField] private GameObject ak;
    [SerializeField] private GameObject[] end;
    [SerializeField] private Transform shotPoint;
    [SerializeField] private Transform Point;

    private Vector3 akTramsform;

    [SerializeField] Text scoreText;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    [SerializeField] private SpriteRenderer akSprite;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        akTramsform = ak.transform.localPosition;
    }

    void Update()
    {
        if (!won)
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            rb.velocity = new Vector2(moveHorizontal * speed, rb.velocity.y);


            if (moveHorizontal > 0)
            {
                if (!ult)
                {
                    animator.SetBool("Stand", false);
                    spriteRenderer.flipX = false;
                }
                else
                {
                    animator.SetBool("ULTStand", false);
                    spriteRenderer.flipX = false;
                    ak.transform.localPosition = new Vector3(akTramsform.x, akTramsform.y, akTramsform.z);
                    shotPoint.transform.localPosition = new Vector3(Point.localPosition.x, Point.localPosition.y, Point.localPosition.z);
                    akSprite.flipX = false;
                }
            }
            else if (moveHorizontal < 0)
            {
                if (!ult)
                {
                    animator.SetBool("Stand", false);
                    spriteRenderer.flipX = true;
                }
                else
                {
                    animator.SetBool("ULTStand", false);
                    spriteRenderer.flipX = true;
                    ak.transform.localPosition = new Vector3(-(akTramsform.x), akTramsform.y, akTramsform.z);
                    shotPoint.transform.localPosition = new Vector3(-(Point.localPosition.x), Point.localPosition.y, Point.localPosition.z);
                    akSprite.flipX = true;

                }
            }
            else
            {
                if (!ult)
                {
                    if (isGrounded)
                        animator.SetBool("Stand", true);
                }
                else
                {
                    if (isGrounded)
                        animator.SetBool("ULTStand", true);
                }
            }

            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
                isGrounded = false;
            }
            if (Input.GetKeyDown(KeyCode.Q) && score >= 3 && !ult)
            {
                score -= 3;
                StartCoroutine(ULT());
            }
            scoreText.text = score.ToString();
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Feather")
        {
            Destroy(collision.gameObject);
            score++;
        }
        if (collision.gameObject.tag == "FireBird")
        {
            animator.SetTrigger("Won");
            StartCoroutine(LVLEnd());
        }

    }
    private IEnumerator ULT()
    { 
        ult = true;
        animator.SetTrigger("ULT");
        ak.SetActive(true);

        yield return new WaitForSeconds(5f);

        ak.SetActive(false);
        animator.SetTrigger("ULTEnd");
        ult = false;
    }
    private IEnumerator LVLEnd()
    {
        won = true;
        yield return new WaitForSeconds(5f);

        foreach (var item in end)
        {
            item.SetActive(true);
        }
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(0);

    }
}
