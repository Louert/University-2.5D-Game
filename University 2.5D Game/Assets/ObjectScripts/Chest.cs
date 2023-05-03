using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public GameObject chest;
    public GameObject pr;
    public Animator animator;
    public int maxHealth = 1;
    private int currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OpenDamage(int damage)
    {
        currentHealth -= damage;

        animator.SetTrigger("HurtChest");

        if (currentHealth <= 0)
            StartCoroutine(Die());
    }

    IEnumerator Die()
    {
        yield return new WaitForSeconds(0.8f);
        pr.SetActive(true);
        chest.SetActive(false);

    }
}
