using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eagleController : MonoBehaviour
{
    // Start is called before the first frame update
    public float upCap = 0;
    public float DownCap = 0;
    public bool isFly=true;
    [SerializeField] private LayerMask ground;
    public Collider2D coll;
    public Rigidbody2D rb;
    public Animator animator;
    void Start()
    {
        upCap = transform.position.y + 3;
        DownCap = transform.position.y - 3;
    }
    private void Move()
    {
        if (isFly)
        {
            rb.linearVelocity = new Vector2(0,5);
        }
        else
        {
            rb.linearVelocity = new Vector2(0, -5);
        }
        if (coll.IsTouchingLayers(ground) || (transform.position.y<DownCap&&!isFly))
        {
            isFly = true;
        }
        else if(isFly&&transform.position.y>upCap)
        {
            isFly = false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        Move();
    }
    public void Triggle()
    {
        coll.isTrigger=true;
        animator.SetTrigger("Death");
    }
    private void Death()
    {
        Destroy(gameObject);
    }
}
