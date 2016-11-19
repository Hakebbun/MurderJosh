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

	public GameController instance;

    private Animator animator;                          //Used to store reference to player's animator component

    private Rigidbody2D rb2D;
    private SpriteRenderer sprite;
    private int score;                          //How many coins player has picked up
    private bool grounded;                      //is the player on the ground?
    public bool wallL;                      //is the player on the ground?
    public bool wallR;                      //is the player on the ground?

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
        bool groundedTemp = grounded;
        grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
        if(!groundedTemp && grounded && animator.GetCurrentAnimatorStateInfo(0).IsName("FloatDown"))
        {
            Debug.Log("Land Idle");
            animator.SetTrigger("landIdle");

        }


        wallL = Physics2D.Linecast(transform.position, wallCheckL.position, 1 << LayerMask.NameToLayer("Ground"));
        wallR = Physics2D.Linecast(transform.position, wallCheckR.position, 1 << LayerMask.NameToLayer("Ground"));

		// Lock rotation
		transform.rotation = Quaternion.Euler(lockPos, lockPos, lockPos);


        if (Input.GetButtonDown("Jump"))
        {

            if (grounded)
            {
                jump = true;
                animator.SetTrigger("jumpIdle");

            }
            else if (wallR)
            {
                Debug.Log("WALL Right");
                walljumpR = true;
                animator.SetTrigger("jumpIdle");

            }
            else if (wallL)
            {
                Debug.Log("WALL Left");
                walljumpL = true;
                animator.SetTrigger("jumpIdle");

            }
        }
    }

    void FixedUpdate()
    {
		float moveHorizontal = Input.GetAxis("Horizontal");
        Vector2 movement = new Vector2((moveHorizontal * movementSpeed), rb2D.velocity.y);
        rb2D.velocity = movement;

        Vector2 realMovement = rb2D.transform.InverseTransformDirection(rb2D.velocity);

        //character facing right
        if (movement.x > 0)
		{
			sprite.flipX = false;
		}
		else if (movement.x < 0)//character facing left
        {
			sprite.flipX = true;
		}
        else if (grounded && animator.GetCurrentAnimatorStateInfo(0).IsName("Land") || animator.GetCurrentAnimatorStateInfo(0).IsName("Walk"))
        {
            animator.SetTrigger("stopWalking");
        }

		//only play walk animation if arrow is being pressed and character is on ground
		if (moveHorizontal != 0 && grounded && !jump)
			animator.SetTrigger ("walk");
        
        

		//If the dude is falling
		if (!grounded && realMovement.y < 0 && animator.GetCurrentAnimatorStateInfo(0).IsName("FloatUp"))
        {
			animator.SetTrigger ("floatDown");
		}
        else if (grounded && jump)
        {
            rb2D.AddForce(new Vector2(0f, jumpSpeed));
            jump = false;
        }
        else if (walljumpL)
        {
            movement = new Vector2(0, 0);
            rb2D.velocity = movement;
            rb2D.AddForce(new Vector2(jumpSpeed, jumpSpeed));
            walljumpL = false;
        }
        else if (walljumpR)
        {
            movement = new Vector2(0, 0);
            rb2D.velocity = movement;
            rb2D.AddForce(new Vector2(-jumpSpeed, jumpSpeed));
            walljumpR = false;
        }

    }

	void OnCollisionEnter2D(Collision2D collision){
		if (collision.collider.CompareTag ("bullet")) {
			instance.GameOver ();
		}
	}

}