using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Custom Variables
    Rigidbody2D rb2d;
    public LayerMask environment;

    Vector2 move = Vector2.zero;

    public float horizontalSpeed;
    public float verticalForce;
    public float maxSpeed;
    public float maxRaycastDistance = 0.2f;

    public bool onGround;
    #endregion

    #region Functions
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        environment = LayerMask.GetMask("Environment");
    }

    // Update is called once per frame
    void Update()
    {
        GroundCheck();
        MovePlayer();
    }

    private void FixedUpdate()
    {
        
        Jump();
    }

    /// <summary>
    /// Controls the movement of the player.
    /// </summary>
    private void MovePlayer()
    {
        if(Input.GetButtonDown("Move Right"))
        {
                rb2d.AddForce(Vector2.right * horizontalSpeed * Time.fixedDeltaTime);
        }

        else if (Input.GetButtonDown("Move Left"))
        {
                rb2d.AddForce(Vector2.left * horizontalSpeed * Time.fixedDeltaTime);
        }
    }

    private void Jump()
    {
        if (onGround && Input.GetButton("Jump"))
        {
                rb2d.AddForce(Vector2.up * verticalForce, ForceMode2D.Impulse);
        }
    }

    void GroundCheck()
    {
        onGround = Physics2D.OverlapArea(new Vector2(transform.position.x - 0.5f, transform.position.y - 0.5f),
            new Vector2(transform.position.x + 0.5f, transform.position.y + 0.51f), environment);
    }
    #endregion
}
