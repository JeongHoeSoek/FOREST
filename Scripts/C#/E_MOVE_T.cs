using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_MOVE_T : MonoBehaviour
{

    bool Tracing = false;

GameObject traceTarget;
    public float movePower = 1f;
    Rigidbody2D rigid;
    Animator anim;
    SpriteRenderer spriteRenderer;
    CircleCollider2D Ccircle;
    CapsuleCollider2D Capcollider;
    public int Hp;

    int movementFlag = 0;
    // Start is called before the first frame update
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        Ccircle = GetComponent<CircleCollider2D>();
        Capcollider = GetComponent<CapsuleCollider2D>();

        StartCoroutine("changeMovement");
    }

    IEnumerator changeMovement()
    {
        movementFlag = Random.Range(0, 3);

        if (movementFlag == 0)
            anim.SetInteger("WalkSpeed", 0);
        else
            anim.SetInteger("WalkSpeed", 1);
        yield return new WaitForSeconds(1);

        StartCoroutine(changeMovement());
    }

    void FixedUpdate()
    {
        
        Move();
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            traceTarget = collision.gameObject;

            StopCoroutine(changeMovement());
        }
        
    }

  

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Tracing = true;
            anim.SetBool("isTracing", true);
            anim.SetBool("StartTracing", true);
            movePower = 0;
            Invoke("TracingStart", 1);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Tracing = false;
            anim.SetBool("isTracing", false);
            anim.SetBool("StartTracing", false);
            movePower = 2;
        }

    }


    void TracingStart()
    {
        anim.SetBool("StartTracing", false);
        movePower = 5;
    }

    void Move()
    {
        Vector3 moveVelocity = Vector3.zero;
        string dist = "";

        if (Tracing)
        {
            Vector3 playerPos = traceTarget.transform.position;

            if (playerPos.x < transform.position.x)
                dist = "Left";
            else if (playerPos.x > transform.position.x)
                dist = "Right";
        }
        else
        {
            if (movementFlag == 1)
            {
                dist = "Left";
            }

            else if (movementFlag == 2)
            {
                dist = "Right";
            }
        }

        if (dist == "Left")
        {
            moveVelocity = Vector3.left;
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if(dist == "Right")
            {
            moveVelocity = Vector3.right;
            transform.localScale = new Vector3(1, 1, 1);
        }

        transform.position += moveVelocity * movePower * Time.deltaTime;
    }


    public void OnDamaged()
    {
        Hp--;

        if (Hp == 0)
        {
            //Sprite Alpha
            spriteRenderer.color = new Color(1, 1, 1, 0.4f);
            //Sprite Flip Y
            spriteRenderer.flipY = true;
            //collider Disable
            Capcollider.enabled = false;
            //Die Efeect Jump
            rigid.AddForce(Vector2.up * 3, ForceMode2D.Impulse);
            //Destroy
            Invoke("DeActive", 2);
        }
        else
        {
            movePower = 0;
            spriteRenderer.color = new Color(1, 1, 1, 0.4f);
            gameObject.layer = 9;
            Invoke("Normal", 2);

        }
        
    }

    void DeActive()
    {
        gameObject.SetActive(false);
    }

    void Normal()
    {
        movePower = 2.6f;
        spriteRenderer.color = new Color(1, 1, 1, 1);
        gameObject.layer = 6;

    }
}
