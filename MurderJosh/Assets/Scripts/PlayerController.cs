using UnityEngine;
using System.Collections;


public class PlayerController : MonoBehaviour
{
	private float lockPos = 0;
    public float movementSpeed;                         //Speed at which player should travel
    public float jumpSpeed;                             //Speed at which player should jump
    public float maxSpeed = 5f;                         //max speed player can move
    [HideInInspector]
    public bool jump = false;
    public bool walljumpL = false;
    public bool walljumpR = false;
    public Transform groundCheck;                       //used to check if player is on the ground

    public Transform wallCheckL;
    public Transform wallCheckR;

    private Animator animator;                          //Used to store reference to player's animator component

    private Rigidbody2D rb2D;
    private SpriteRenderer sprite;
    private int score;                          //How many coins player has picked up
    private bool grounded;                      //is the player on the ground?
    private bool wallL;                      //is the player on the ground?
    private bool wallR;                      //is the player on the ground?

    // Use this for initialization
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();

        score = 0;
        sprite.flipX = false;                       //player should start facing right



    }

    void Update()
    {
        //Cast line down to see if player is touching the ground
        grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
        wallL = Physics2D.Linecast(transform.position, wallCheckL.position, 1 << LayerMask.NameToLayer("Ground"));
        wallR = Physics2D.Linecast(transform.position, wallCheckR.position, 1 << LayerMask.NameToLayer("Ground"));

		// Lock rotation
		transform.rotation = Quaternion.Euler(lockPos, lockPos, lockPos);


        if ((Input.GetKeyDown(KeyCode.UpArrow) && grounded))
            jump = true;

        if (Input.GetKeyDown(KeyCode.UpArrow) && wallR)
        {
            Debug.Log("WALL Right");
            walljumpR = true;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && wallL)
        {
            Debug.Log("WALL Left");
            walljumpL = true;
        }
    }

    void FixedUpdate()
    {

        float moveHorizontal = Input.GetAxis("Horizontal");
        Vector2 movement = new Vector2((moveHorizontal * movementSpeed), rb2D.velocity.y);
        
        rb2D.velocity = movement;
        
        if (jump)
        {
            rb2D.AddForce(new Vector2(0f, jumpSpeed));
            jump = false;
        }
        if (walljumpL)
        {
            movement = new Vector2(0, 0);
            rb2D.velocity = movement;
            rb2D.AddForce(new Vector2(jumpSpeed, jumpSpeed));
            walljumpL = false;
        }
        if (walljumpR)
        {
            movement = new Vector2(0, 0);
            rb2D.velocity = movement;
            rb2D.AddForce(new Vector2(-jumpSpeed, jumpSpeed));
            walljumpR = false;
        }

    }

}