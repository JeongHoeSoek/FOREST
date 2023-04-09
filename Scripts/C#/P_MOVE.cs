using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_MOVE : MonoBehaviour
{
    public GameManager gameManager;
    public float maxSpeed;
    public float jumpPower;
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator anim;
    CapsuleCollider2D capsule_pl;
    public bool doubleJump = false;
    public int jumpCount = 0;
    Vector2 dirVec;

    GameObject scanObject;
    public GameObject Interect;

    // Start is called before the first frame update
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        capsule_pl = GetComponent<CapsuleCollider2D>();

        gameManager.HealthDown();
        gameManager.HealthDown();
    }

    void Update()
    {

  
        //Jump
        if ((Input.GetButtonDown("Jump") && !anim.GetBool ("isJumping") && (gameManager.isAction == false)) ||
            anim.GetBool("isJumping") && doubleJump && Input.GetButtonDown("Jump")) 
        {


            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            anim.SetBool("isJumping", true);
            jumpCount++;

            if (jumpCount == 2)
            {
                doubleJump = false;
                jumpCount = 0;
            }
          

        }


        

            //Stop Speed
            if (Input.GetButtonUp("Horizontal"))
        {
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);

        }

        if ((Input.GetButton("Horizontal")) && (gameManager.isAction == false))
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;


        if (Mathf.Abs(rigid.velocity.x) < 0.5) //횡이동 x값이 0, 즉 움직이지 않는다. Math f로 절대값.
            anim.SetBool("isWalking", false);
        else
            anim.SetBool("isWalking", true);




        if (spriteRenderer.flipX == true)
            dirVec = Vector2.left;
        else if (spriteRenderer.flipX == false)
            dirVec = Vector2.right;



        if (Input.GetButtonDown("Fire1") && scanObject != null)
        {
;
            gameManager.Action(scanObject);
        }



    }

    void FixedUpdate()
    {
        //Move by Control
        float h = gameManager.isAction ? 0 : Input.GetAxisRaw("Horizontal");
        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        //Max Speed
        if (rigid.velocity.x > maxSpeed) // right max speed
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
        else if (rigid.velocity.x < maxSpeed * (-1)) //left max speed
            rigid.velocity = new Vector2(maxSpeed * (-1), rigid.velocity.y);


        Debug.DrawRay(rigid.position, dirVec * 0.7f , new Color(0, 1.2f, 0));
        RaycastHit2D rayHitt = Physics2D.Raycast(rigid.position, dirVec * 0.7f, 1.2f, LayerMask.GetMask("Object"));

        if (rayHitt.collider != null)
        {
            scanObject = rayHitt.collider.gameObject;
            Interect.SetActive(true);
        }
        else
        {
            scanObject = null;
            Interect.SetActive(false);
        }
        //Landing platform
        if (rigid.velocity.y < 0)
        {

            Debug.DrawRay(rigid.position, Vector3.down, new Color(0, 1.2f, 0));

            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 1.2f, LayerMask.GetMask("platform"));


            if (rayHit.collider != null)
            {
                if (rayHit.distance < 0.7f)
                    anim.SetBool("isJumping", false);
            }

        }
    }











    void OnCollisionEnter2D(Collision2D collision)
    {


        if ((collision.gameObject.tag == "Enemy")|| (collision.gameObject.tag == "Enemy_B")
            || (collision.gameObject.tag == "Enemy_N"))
        {
            if (rigid.velocity.y < 0 && transform.position.y > collision.transform.position.y)
            {
                OnAttack(collision.transform);

            }
            else //기존 데미지 방식
                OnDamaged(collision.transform.position);

        }

    }
    



    void OnTriggerEnter2D(Collider2D collision)
    {

        if ((collision.gameObject.layer == 12)|| (collision.gameObject.layer == 7) && rigid.velocity.y < 0)
        {
           anim.SetBool("isJumping", false);
        }


        if ((collision.gameObject.layer == 12) && rigid.velocity.y < -0.5)
        {
            anim.SetBool("isJumping", false);

            Block block = collision.gameObject.GetComponent<Block>();

            switch (block.type)
            {
                case "Up":
                    Vector2 upVelocity = new Vector2(0, block.value);
                    rigid.AddForce(upVelocity, ForceMode2D.Impulse);
                    break;

                case "DoubleJump":
                    jumpCount = 0;
                    doubleJump = true;
                    break;
                case "Portal Enter":
                    Vector3 anotherPortalPos = block.portal.transform.position;
                    Vector3 warpPos = new Vector3(anotherPortalPos.x, anotherPortalPos.y + 2f, anotherPortalPos.z);
                    transform.position = warpPos;
                    break;
            }
        }


        if (collision.gameObject.tag == "ITEM")
        {
            //Point
            gameManager.hiddenpoint += 1;
            gameManager.stagePoint += 100;



            //Deactive Item
            collision.gameObject.SetActive(false);
        }
        else if (collision.gameObject.tag == "coffee")
        {
            gameManager.HealthUp();
            collision.gameObject.SetActive(false);
        }
        else if (collision.gameObject.tag == "Energy")
        {
            collision.gameObject.SetActive(false);
            SuperPower();

        }
        else if (collision.gameObject.tag == "Finish")
        {
            gameManager.Nextstage();
        }

    }






    void OnDamaged(Vector2 targetPos)
    {
        // Health Down
        gameManager.HealthDown();
        //Change Layer
        gameObject.layer = 9;

        //View Alpha
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);

        int dirc = transform.position.x - targetPos.x > 0 ? 1 : -1;
        // Reaction Force
        rigid.AddForce(new Vector2(dirc, 1) * 10, ForceMode2D.Impulse);


        //Ani
        anim.SetTrigger("Damaged");
        Invoke("OffDamaged", 3);

    }



    void OnAttack(Transform enemy)
    {
        // point
        gameManager.stagePoint += 100;


        //reaction Force
        rigid.AddForce(Vector2.up * 10, ForceMode2D.Impulse);

        //enemy die

        if (enemy.tag == "Enemy")
        {
            E_MOVE_T enemyMove_T = enemy.GetComponent<E_MOVE_T>();
            enemyMove_T.OnDamaged();
        }
        else if (enemy.tag == "Enemy_B")
        {
            E_MOVE enemyMove = enemy.GetComponent<E_MOVE>();
            enemyMove.OnDamaged();
        }
        else if (enemy.tag == "Enemy_N")
        {
             E_MOVE_N enemyMove = enemy.GetComponent<E_MOVE_N>();
             enemyMove.OnDamaged();
        }

    }

   


    void OffDamaged()
    {
        gameObject.layer = 8;
        spriteRenderer.color = new Color(1, 1, 1, 1);
    }
    public void OnDie()
    {


        //Sprite Alpha
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);
        //Sprite Flip Y
        spriteRenderer.flipY = true;
        //collider Disable
        capsule_pl.enabled = false;
        //Die Efeect Jump
        rigid.AddForce(Vector2.up * 10, ForceMode2D.Impulse);




    }

    

    public void VelocityZero()
    {
        rigid.velocity = Vector2.zero;
    }

    void Normal()
    {
        maxSpeed = 7;
        jumpPower = 24;
    }
    void SuperPower()
    {
        maxSpeed = 15;
        jumpPower = 30;

        Invoke("Normal", 15);
        
    }
}
