using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogController : MonoBehaviour
{
    // Start is called before the first frame update
    private float leftCap=0;
    private float rightCap=0;

    [SerializeField] private float jumplength=3f;
    [SerializeField] private float jumpHeight=4f;
    [SerializeField] private LayerMask ground;
    public Collider2D coll;
    public Rigidbody2D rb;

    private bool facingLeft = true;

    public Animator animator;

    void Start()
    {
        leftCap = transform.position.x-2;
        rightCap = transform.position.x+2;
        
    }

    // Update is called once per frame
    void Update()
    {
    
        if (animator.GetBool("Jumping"))
        {
            if (rb.linearVelocity.y < .1)
            {
                animator.SetBool("Falling",true);
                animator.SetBool("Jumping", false);
            }
        }
        if (coll.IsTouchingLayers(ground)&&animator.GetBool("Falling"))
        {
          
            animator.SetBool("Falling", false);
        }

    }

    private void Move()
    {
        if (facingLeft)
        {
            if (transform.position.x > leftCap)
            {
                if (transform.localScale.x != 1)
                {
                    transform.localScale = new Vector3(1, 1);
                }
                if (coll.IsTouchingLayers(ground))
                {
                    rb.linearVelocity = new Vector2(-jumplength, jumpHeight);
                    animator.SetBool("Jumping", true);
                }
            }
            else
            {
                facingLeft = false;
            }

        }
        else
        {
            if (transform.position.x < rightCap)
            {
                if (transform.localScale.x != -1)
                {
                    transform.localScale = new Vector3(-1, 1);
                }
                if (coll.IsTouchingLayers(ground))
                {
                    rb.linearVelocity = new Vector2(jumplength, jumpHeight);
                    animator.SetBool("Jumping", true);
                }
            }
            else
            {
                facingLeft = true;
            }
        }
    }
    public void Triggle()
    {
        coll.isTrigger = true;
        animator.SetTrigger("Death");
    }
    private void Death()
    {
        Destroy(gameObject);
    }
}
