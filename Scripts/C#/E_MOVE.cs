using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_MOVE : MonoBehaviour
{
    Rigidbody2D rigid;
    public int nextMove;
    Animator anim;
    SpriteRenderer spriteRenderer;
    CapsuleCollider2D Capcollider;
    // Start is called before the first frame update
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        Capcollider = GetComponent<CapsuleCollider2D>();
        Invoke("Think", 1);

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Move
        rigid.velocity = new Vector2(nextMove * 5, rigid.velocity.y);



        Vector2 frontVec = new Vector2(rigid.position.x + nextMove * 0.2f, rigid.position.y);
        Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0));

        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("platform"));
        if (rayHit.collider == null) 
            Turn();



    }

    void Think()
    {
        //Set Next Active
        nextMove = Random.Range(-1, 2);



        //Sprite Animation
        anim.SetInteger("WalkSpeed", nextMove);

        //Flip Sprite
        if (nextMove != 0)
            spriteRenderer.flipX = nextMove == 1;

        //Recursive
        float nextThinkTime = Random.Range(0.5f, 1f);
        Invoke("Think", nextThinkTime);
    }

    void Turn()
    {
        if (spriteRenderer.flipY == true)
            CancelInvoke();
        else
        {
            nextMove *= -1;
            spriteRenderer.flipX = nextMove == 1;

            CancelInvoke();
            Invoke("Think", 2);
        }
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
