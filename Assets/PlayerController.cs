using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private Collider2D coll;
    [SerializeField]
    private int gem = 0;
    [SerializeField]
    private Text gemText;
    [SerializeField]
    private float movementSpeed;
    private bool facingRight;
    private bool attack;
    private bool roll;
    private bool jump;
    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private Transform[] groundPoints;
    [SerializeField]
    private float groundRadius;
    [SerializeField]
    private LayerMask whatIsGround;
    private bool isGrounded;
    [SerializeField]
    private bool airControl;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public int attackDamage = 55;
    public static float health;
    public GameOverScreen GameOverScreen;
    public NextLevel NextLevel;
    private int count = 0;
    public Boss Boss;
    
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        facingRight = true;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
        health = 0.5f;
    }

    void Update()
    {
        HandleInput();
        if (health <= 0)
        {
            Die();
            StartCoroutine(GameOver());
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        isGrounded = IsGrounded();
        HandleMovement(horizontal);
        Flip(horizontal);
        HandleAttacks();
        HandleLayer();
        ResetValue();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Collectables")
        {
            SoundManager.PlaySound("Gem");
            Destroy(collision.gameObject);
            gem += 1;
            gemText.text = gem.ToString();
        }
        if (this.anim.GetCurrentAnimatorStateInfo(0).IsName("Roll") && collision.CompareTag("EnemySword"))
        {
            health -= 0f;
            //anim.SetTrigger("gethit");
        }
        if (!this.anim.GetCurrentAnimatorStateInfo(0).IsName("Roll") && collision.CompareTag("EnemySword"))
        {
            health -= 0.101f;
            anim.SetTrigger("gethit");
            SoundManager.PlaySound("Hurt");
        }
        if (this.anim.GetCurrentAnimatorStateInfo(0).IsName("Roll") && collision.CompareTag("Arrow"))
        {
            health -= 0f;
            //anim.SetTrigger("gethit");
        }
        if (!this.anim.GetCurrentAnimatorStateInfo(0).IsName("Roll") && collision.CompareTag("Arrow"))
        {
            health -= 0.101f;
            anim.SetTrigger("gethit");
            SoundManager.PlaySound("Hurt");
        }
        if (collision.CompareTag("HealthPotion"))
        {
            SoundManager.PlaySound("Gem");
            Destroy(collision.gameObject);
            health += 0.5f;
            if(health >= 0.5f)
            {
                health = 0.5f;
            }
        }
        if (collision.CompareTag("PowerUp"))
        {
            SoundManager.PlaySound("Gem");
            Destroy(collision.gameObject);
            jumpForce = 800;
            movementSpeed = 12;
            GetComponent<SpriteRenderer>().color = Color.red;
            StartCoroutine(ResetPower());
        }
        if (collision.CompareTag("Trap"))
        {
            Die();
            GameOverScreen.Setup(gem);
            Time.timeScale = 0;
        }
        if (collision.CompareTag("CheckPoint"))
        {
           SoundManager.PlaySound("Victory");
           NextLevel.Setup2(gem);
        }
        if (this.anim.GetCurrentAnimatorStateInfo(0).IsTag("Attack") && collision.CompareTag("Minion"))
        {
            count += 1;
            if (count >= 10)
            {
                Boss.Die();
            }
            
        }
    }

    private void HandleMovement(float horizontal)
    {
        if (rb.velocity.y < 0)
        {
            anim.SetBool("fall", true);
        }
        if (!anim.GetBool("roll") && !this.anim.GetCurrentAnimatorStateInfo(0).IsTag("Attack") && (isGrounded || airControl))
        {
            rb.velocity = new Vector2(horizontal * movementSpeed, rb.velocity.y);
        }
        if (isGrounded && jump && !anim.GetBool("roll") && !this.anim.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            isGrounded = false;
            rb.AddForce(new Vector2(0, jumpForce));
            anim.SetTrigger("jump");
            SoundManager.PlaySound("Jump");
        }
        if(roll && !this.anim.GetCurrentAnimatorStateInfo(0).IsName("Roll"))
        {
            //rb.velocity = Vector2.zero;
            anim.SetBool("roll", true);
        }
        else if (!this.anim.GetCurrentAnimatorStateInfo(0).IsName("Roll"))
        {
            anim.SetBool("roll", false);
        }

        anim.SetFloat("speed", Mathf.Abs(horizontal));
        
    }

    private void HandleAttacks()
    {
        if (attack && !this.anim.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            anim.SetTrigger("attack");
            SoundManager.PlaySound("Attack");
            rb.velocity = Vector2.zero;
            //Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
            //foreach (Collider2D enemy in hitEnemies)
            //{
            //    enemy.GetComponent<Minion>().takeDamage(attackDamage);

            //}
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawSphere(attackPoint.position, attackRange);
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            attack = true;
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            roll = true;
            rb.velocity = Vector2.zero;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            jump = true;
        }
    }

    private void Flip(float horizontal)
    {
        if (horizontal > 0 && !facingRight || horizontal < 0 && facingRight)
        {
            facingRight = !facingRight;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }

    private void ResetValue()
    {
        attack = false;
        roll = false;
        jump = false;
    }

    private bool IsGrounded()
    {
        if (rb.velocity.y <= 0)
        {
            foreach (Transform point in groundPoints)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(point.position, groundRadius, whatIsGround);
                for (int i = 0; i < colliders.Length; i++)
                {
                    if (colliders[i].gameObject != gameObject)
                    {
                        anim.ResetTrigger("jump");
                        anim.SetBool("fall", false);
                        return true;
                    }
                }
            }
        }
        return false;
    }

    private void HandleLayer()
    {
        if (!isGrounded)
        {
            anim.SetLayerWeight(1, 1);
        }
        else
        {
            anim.SetLayerWeight(1, 0);
        }
    }

    void Die()
    {
        anim.SetBool("dead", true);
        SoundManager.PlaySound("Dead");
        this.enabled = false;
    }

    private IEnumerator ResetPower()
    {
        yield return new WaitForSeconds(5);
        jumpForce = 500;
        movementSpeed = 7;
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    private IEnumerator GameOver()
    {
        yield return new WaitForSeconds(3);
        GameOverScreen.Setup(gem);
        Time.timeScale = 0;
    }
}
