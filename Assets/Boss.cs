using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public int maxHealth = 100;
    int currentHealth;
    public Animator anim;
    private Transform player;
    public float attackRange;
    public GameObject blackhole;
    public GameObject blackholeParent;
    public float fireRate = 5f;
    private float nextFireTime;
    public EndGame EndGame;
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
        if (distanceFromPlayer <= attackRange && nextFireTime < Time.time)
        {
            anim.SetBool("attack", true);
            Instantiate(blackhole, blackholeParent.transform.position, Quaternion.identity);
            nextFireTime = Time.time + fireRate;
        }
        else
        {
            anim.SetBool("attack", false);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position, attackRange);
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("PlayerSword"))
    //    {
    //        Die();
    //    }
    //}

    //public void takedamage(int damage)
    //{
    //    currentHealth -= damage;
    //    anim.SetTrigger("hurt");
    //    if (currentHealth <= 0)
    //    {
    //        Die();
    //    }
    //}

    public void Die()
    {
        anim.SetBool("isdead", true);
        
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
        StartCoroutine(GameOver());
    }

    private IEnumerator GameOver()
    {
        yield return new WaitForSeconds(5);
        EndGame.Setup();
        Time.timeScale = 0;
    }
}
