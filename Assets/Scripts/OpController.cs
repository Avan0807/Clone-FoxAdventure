using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpController : MonoBehaviour
{
    // Start is called before the first frame update
    public float leftCap = 0;
    public float rightCap = 0;
    [SerializeField] private LayerMask ground;
    public Collider2D coll;
    public Rigidbody2D rb;

    private bool facingLeft = true;

    public Animator animator;

    void Start()
    {
        leftCap = transform.position.x - 3;
        rightCap = transform.position.x + 3;

    }

    // Update is called once per frame
    void Update()
    {

        Move();

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
            
                    rb.linearVelocity = new Vector2(-5, 0);
             
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
               
                    rb.linearVelocity = new Vector2(5, 0);
                   
            }
            else
            {
                facingLeft = true;
            }
        }
    }
    public void Triggle()
    {coll.isTrigger = true;
        animator.SetTrigger("Death");
    }
    private void Death()
    {
        Destroy(gameObject);
    }
}
