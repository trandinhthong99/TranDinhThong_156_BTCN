using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public Animator anim;
    public GameObject potion;
    public GameObject potionParent;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerSword"))
        {
            anim.SetBool("open", true);
            Instantiate(potion, potionParent.transform.position, Quaternion.identity);
            GetComponent<Collider2D>().enabled = false;
            this.enabled = false;
        }
    }
}
