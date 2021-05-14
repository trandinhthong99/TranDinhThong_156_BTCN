using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_2 : MonoBehaviour
{
    public int maxHealth = 100;
    int currentHealth;
    public Animator anim;
    private Transform player;
    public float shootingRange;
    public GameObject arrow;
    public GameObject arrowParent;
    public float fireRate = 5f;
    private float nextFireTime; 
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        currentHealth = maxHealth;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);
        if(distanceFromPlayer <= shootingRange && nextFireTime < Time.time)
        {
            anim.SetBool("shoot", true);
            Instantiate(arrow, arrowParent.transform.position, Quaternion.identity);
            nextFireTime = Time.time + fireRate;
        }
        else
        {
            anim.SetBool("shoot", false);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position, shootingRange);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerSword"))
        {
            Die();
        }
    }

    //public void takeDamage(int damage)
    //{
    //    currentHealth -= damage;
    //    //anim.SetTrigger("hurt");
    //    if (currentHealth <= 0)
    //    {
    //        Die();
    //    }
    //}

    void Die() 
    {
        anim.SetBool("isdead2", true);
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
    }
}
