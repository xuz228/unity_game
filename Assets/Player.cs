using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player: MonoBehaviour
{
    Rigidbody2D rb;
    public float speed;
    public float jumpHaigth;
    public Transform groundCheck;
    bool isGrounded;
    Animator anim;
    int curHP;
    int maxHP = 3;
    bool isHit = false;
    public Main main;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        curHP = maxHP;
    }
     
    // Update is called once per frame
    void Update()
    {
        CheckGrounded();
        if (Input.GetAxis("Horizontal") == 0 && (isGrounded)) 
        {
            anim.SetInteger("Stete", 1);
        }
        else{
            Flip();
            if (isGrounded)
                anim.SetInteger("Stete", 2);
        }
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, rb.velocity.y);
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            rb.AddForce(transform.up * jumpHaigth, ForceMode2D.Impulse);
    }

    void Flip()
    {
        if (Input.GetAxis("Horizontal") > 0)
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        if (Input.GetAxis("Horizontal") < 0)
            transform.localRotation = Quaternion.Euler(0, 180, 0);
    }
    void CheckGrounded()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, 0.2f);
        isGrounded = colliders.Length > 1;
        if (!isGrounded)
            anim.SetInteger("Stete", 3);
    }

    public void RecountHP(int deltaHP)
    {
        curHP = curHP + deltaHP;
        if (deltaHP < 0)
        {
            StopCoroutine(OnHit());
            isHit = true;
            StartCoroutine(OnHit());
        }
        print(curHP);
        if (curHP <= 0)
        {
            GetComponent<CapsuleCollider2D>().enabled = false;
            Invoke("lose", 1.5f);
        }
    }

    IEnumerator OnHit()
    {
        if (isHit) 
            GetComponent<SpriteRenderer>().color = new Color(1f, GetComponent<SpriteRenderer>().color.g - 0.08f, GetComponent<SpriteRenderer>().color.b - 0.08f);
        else 
            GetComponent<SpriteRenderer>().color = new Color(1f, GetComponent<SpriteRenderer>().color.g + 0.08f, GetComponent<SpriteRenderer>().color.b + 0.08f);

        if (GetComponent<SpriteRenderer>().color.g == 1f)
            StopCoroutine(OnHit());
            if (GetComponent<SpriteRenderer>().color.g <= 0)
            isHit = false;
        yield return new WaitForSeconds(0.02f);
        StartCoroutine(OnHit());
    } 

    void lose()
    {
         main.GetComponent<Main>().lose();
    }
}
