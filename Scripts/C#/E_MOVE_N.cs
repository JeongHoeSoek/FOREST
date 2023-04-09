using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_MOVE_N : MonoBehaviour
{

    bool isTracing = false;

GameObject traceTarget;
    public float movePower = 1f;
    Rigidbody2D rigid;
    Animator anim;
    SpriteRenderer spriteRenderer;
    CapsuleCollider2D Capcollider;
    int movementFlag = 0;

    // Start is called before the first frame update
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
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
        yield return new WaitForSeconds(1.5f);

        StartCoroutine(changeMovement());
    }

    void FixedUpdate()
    {
        
        Move();
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            traceTarget = other.gameObject;

            StopCoroutine(changeMovement());
        }
        
    }

  

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Plyer")
        {
            isTracing = true;
            anim.SetInteger("WalkSpeed", 1);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Plyer")
        {
            isTracing = false;
            anim.SetInteger("WalkSpeed", 0);
        }

    }
    void Move()
    {
        Vector3 moveVelocity = Vector3.zero;
        string dist = "";


        if (movementFlag == 1)
        { 
            moveVelocity = Vector3.left;
            transform.localScale = new Vector3(1, 1, 1);
          }
       
        else if (movementFlag == 2)
        {
            moveVelocity = Vector3.right;
            transform.localScale = new Vector3(-1, 1, 1);
        }
        transform.position += moveVelocity * movePower * Time.deltaTime;
    }


    public void OnDamaged()
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

    void DeActive()
    {
        gameObject.SetActive(false);
    }

}
