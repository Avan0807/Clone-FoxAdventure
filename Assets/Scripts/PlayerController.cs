using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour
{
    private Vector2 startPoint;
    private float horizontal;
    private float speed = 8f;
    private float jumpingPower = 16f;
    private bool isFacingRight = true;
    public Vector2 debug;
    [SerializeField] private Text cherriesText;
    [SerializeField] private Text levelText;
    [SerializeField] private Text guideText;

    AudioController audioController;
    
    public Animator animator;
    public int cherries = 0;
    public bool isJumping = false;
    public bool isFalling = false;
    public bool isRunning = false;
    public bool isHurted=false;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;


    private void Awake()
    {
        audioController = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioController>();

    }
    public void frezen()
    {
        speed = 0;
        jumpingPower = 0;
    }

    //cherry triggle
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Cherry"))
        {

            Destroy(collision.gameObject);
            if (SceneManager.GetActiveScene().name == "Tutorial")
            {
                audioController.PlaySFX(audioController.cherryClip);
                cherries++;
                cherriesText.text = "Cherries: " + cherries + "/1";
            }
            else
            {
                audioController.PlaySFX(audioController.cherryClip);
                cherries++;
                cherriesText.text = "Cherries: " + cherries + "/" + (2 * (SceneManager.GetActiveScene().buildIndex) + 3);
            }
           
        }
        if (collision.gameObject.CompareTag("guide"))
        {
            guideText.text = "Hãy dùng quái làm bước đệm";
        }
       
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("guide"))
        {
            guideText.text = "";
        }
    }
    //enemy triggle
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
           
            if (isFalling)
            {
                audioController.PlaySFX(audioController.killClip);
                if (collision.gameObject.GetComponent<FrogController>() != null)
                {
                    FrogController frog = collision.gameObject.GetComponent<FrogController>();
                    
                    frog.Triggle();
                }
              
                else if (collision.gameObject.GetComponent<eagleController>() != null)
                {
                    eagleController eagle = collision.gameObject.GetComponent<eagleController>();
                 
                    eagle.Triggle();
                }
                else if (collision.gameObject.GetComponent<OpController>() != null)
                {
                    OpController op = collision.gameObject.GetComponent<OpController>();

                    op.Triggle();
                }
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpingPower);
               
            }
            else
            {
                speed = 0;jumpingPower = 0;
                isHurted = true;
            }
     
        }
        if (collision.gameObject.CompareTag("trap"))
        {
            speed = 0; jumpingPower = 0;
            isHurted = true;

        }
    }
    private void Respawn()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    private void Start()
    {
      
        if (SceneManager.GetActiveScene().name == "Tutorial")
        {
            cherriesText.text = "Cherries: 0/1" ;
        }
        else
        {
            cherriesText.text = "Cherries: 0/" + (2 * (SceneManager.GetActiveScene().buildIndex) + 3);
            levelText.text="Level: " + (SceneManager.GetActiveScene().buildIndex);
        }

       
        startPoint = transform.position;
    }
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpingPower);
      
        }

        if (Input.GetButtonUp("Jump") && rb.linearVelocity.y > 0f)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);
        }
        if (rb.linearVelocity.y < 0f && !IsGrounded())
        {
            isFalling = true;
        }
        else
        {
            isFalling = false;
        }
     
        //kiem tra co dang chay khong
        isRunning = Mathf.Abs(horizontal) > 0f;
    
        animator.SetBool("isRunning", isRunning);
        animator.SetBool("isJumping", isJumping);
        animator.SetBool("IsFalling", isFalling);
        animator.SetBool("isHurted", isHurted);
        Flip();
    }

    private void FixedUpdate()
    {
   
        
        rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);
        
        isJumping = !IsGrounded();
        if (isFalling && IsGrounded())
        {
            isFalling = false; 
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
    public void RunAudio()
    {
        audioController.PlaySFX(audioController.runClip);
    }
    public void JumpAudio()
    {
        audioController.PlaySFX(audioController.jumpClip); 
    }
    public void deathAudio()
    {
        audioController.PlaySFX(audioController.hurtClip); 
    }

}
