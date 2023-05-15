using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private float offset;
    private SpriteRenderer ak;

    public GameObject bullet;
    public Transform shootPoint;

    private SpriteRenderer bulletSprite;
    private void Start()
    {
        ak = GetComponent<SpriteRenderer>();
        bulletSprite = bullet.GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotz = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        float rotz2 = Mathf.Atan2(-difference.y, -difference.x) * Mathf.Rad2Deg;

        if (ak.flipX)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, rotz2 + offset);
            bulletSprite.flipX = true;
        }
        else
        {
            transform.rotation = Quaternion.Euler(0f, 0f, rotz + offset);
            bulletSprite.flipX = false;
        }

        if (Input.GetMouseButton(0))
            {
                Instantiate(bullet, shootPoint.position, transform.rotation);
            }


    }
}
