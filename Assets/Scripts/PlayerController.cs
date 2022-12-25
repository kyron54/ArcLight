using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Custom Variables
    Rigidbody2D rb2d;
    BoxCollider2D bc;
    Animator anim;
    SpriteRenderer sr;
    public GameObject basicAttack;
    public LayerMask environment;
    private DespawnObject despawnOb;

    Vector2 move = Vector2.zero;

    public float baseSpeed;
    public float horizontalSpeedMod;
    public float verticalForce;
    public float maxSpeed;
    public float maxRaycastDistance = 0.2f;
    public float gravity = 1.6f;
    public float gravityMod = 2.5f;
    public float jumpTimeCounter;
    public float jumpTime;

    public bool onGround;
    public bool isJumping;
    #endregion

    #region Functions
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        environment = LayerMask.GetMask("Environment");
    }

    // Update is called once per frame
    void Update()
    {
        LimitVelocity();
        Jump();
        PlayAnimations();
        BasicAttack();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    /// <summary>
    /// Controls the movement of the player.
    /// </summary>
    /*private void MovePlayer()
    {
        if(Input.GetButtonDown("Move Right"))
        {
                rb2d.AddForce(Vector2.right * horizontalSpeed * Time.fixedDeltaTime);
        }

        else if (Input.GetButtonDown("Move Left"))
        {
                rb2d.AddForce(Vector2.left * horizontalSpeed * Time.fixedDeltaTime);
        }
    }*/

    private void MovePlayer()
    {
        Vector2 move = Vector2.right;

        move.x = Input.GetAxisRaw("Horizontal");

        if(Mathf.Abs(move.x) > 0)
        {
            anim.SetBool("NotRunning", false);
        }
        else
        {
            anim.SetBool("NotRunning", true);
        }

        // Running test

        if(Input.GetKey(KeyCode.F))
        {
            rb2d.AddForce(move * baseSpeed * horizontalSpeedMod, ForceMode2D.Impulse);
        }
        else 
        {
            rb2d.AddForce(move * baseSpeed, ForceMode2D.Impulse);
        }
    }

    private void Jump()
    {
        if (GroundCheck() && Input.GetButtonDown("Jump"))
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rb2d.AddForce(Vector2.up * verticalForce, ForceMode2D.Impulse);
            //rb2d.velocity = Vector2.up * verticalForce;
        }

        if (isJumping == true)
        {
            if (jumpTimeCounter >= 0)
            {
                rb2d.AddForce(Vector2.up * verticalForce, ForceMode2D.Impulse);
                //rb2d.velocity = Vector2.up * verticalForce;

                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }

        if(Input.GetButtonUp("Jump"))
        {
            isJumping = false;
        }

        if (rb2d.velocity.y < 0)
        {
            rb2d.gravityScale = gravity * gravityMod;
        }
        else 
        {
            rb2d.gravityScale = gravity;
        }

        /*if(Input.GetKey(KeyCode.S))
        {
            gravityMod = gravityMod + .05f;
        }
        else 
        {
            gravityMod = gravityMod - .05f;
        }

        if(gravityMod > 3f)
        {
            gravityMod = 3f;
        }*/
    }

    private bool GroundCheck()
    {
        Vector3 cutSize = new Vector3(0.01f, 0f, 0);
        RaycastHit2D raycastHit = Physics2D.BoxCast(bc.bounds.center, bc.bounds.size - cutSize, 0f, Vector2.down, maxRaycastDistance, environment);

        Color rayColor;
        if(raycastHit.collider != null)
        {
            rayColor = Color.green;
            onGround = true;
        }
        else
        {
            rayColor = Color.red;
            onGround = false;
        }

        Debug.DrawRay(bc.bounds.center, Vector2.down * (bc.bounds.extents.y + maxRaycastDistance), rayColor);

        return raycastHit.collider != null;
    }

    void LimitVelocity()
    {
        float limitXVelocity = Mathf.Min(Mathf.Abs(rb2d.velocity.x), maxSpeed) * Mathf.Sign(rb2d.velocity.x);
        float limitYVelocity = rb2d.velocity.y;

        rb2d.velocity = new Vector2(limitXVelocity, limitYVelocity);
    }

    void PlayAnimations()
    {
        //Checks for Y Velocity
        if(rb2d.velocity.y > 0)
        {
            anim.SetFloat("yVelocity", 1f);
        }
        else if(rb2d.velocity.y < 0)
        {
            anim.SetFloat("yVelocity", -1f);
        }
        else
        {
            anim.SetFloat("yVelocity", 0f);
        }

        //Checks for X Velocity
        if(Mathf.Abs(rb2d.velocity.x) > 0)
        {
            anim.SetFloat("xVelocity", 1f);
        }
        else
        {
            anim.SetFloat("xVelocity", 0f);
        }

        if (rb2d.velocity.x < -0.1)
        {
            sr.flipX = true;
        }
        else if (rb2d.velocity.x > 0.1)
        {
            sr.flipX = false;
        }

        //Checks if player is on Ground

        if (GroundCheck())
        {
            anim.SetBool("OnGround", true);
        }
        else if(!GroundCheck())
        {
            anim.SetBool("OnGround", false);
        }
    }

    void BasicAttack()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            Instantiate(basicAttack, new Vector2(transform.position.x, transform.position.y), rotation, gameObject.transform);

        }
    }
    #endregion
}
