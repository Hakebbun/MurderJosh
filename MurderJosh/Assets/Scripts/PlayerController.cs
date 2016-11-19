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
    private bool wallL;                      //is the player on the ground?
    private bool wallR;                      //is the player on the ground?

    // Use this for initialization
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
		animator = GetComponent<Animator> ();

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


        if ((Input.GetButtonDown("Jump") && grounded))
            jump = true;

        if (Input.GetButtonDown("Jump") && wallR)
        {
            Debug.Log("WALL Right");
            walljumpR = true;
        }

        if (Input.GetButtonDown("Jump") && wallL)
        {
            Debug.Log("WALL Left");
            walljumpL = true;
        }
    }

    void FixedUpdate()
    {
		float moveHorizontal = Input.GetAxis("Horizontal");
        Vector2 movement = new Vector2((moveHorizontal * movementSpeed), rb2D.velocity.y);

		//character facing right
		if (movement.x >= 0)
		{
			sprite.flipX = false;
		}
		else //character facing left
		{
			sprite.flipX = true;
		}
		//only play walk animation if arrow is being pressed and character is on ground
		if ((Input.GetKey (KeyCode.RightArrow) || Input.GetKey (KeyCode.LeftArrow)) && grounded)
			animator.SetTrigger ("walk");
        
        rb2D.velocity = movement;

		//If the dude is falling
		if (movement.y < 0) {
			animator.SetTrigger ("floatDown");
		}
        if (jump)
        {
            rb2D.AddForce(new Vector2(0f, jumpSpeed));
			animator.SetTrigger ("jumpIdle");
			animator.SetTrigger ("floatDown");
			animator.SetTrigger ("landIdle");
            jump = false;
        }
        if (walljumpL)
        {
            movement = new Vector2(0, 0);
            rb2D.velocity = movement;
            //rb2D.AddForce(new Vector2((jumpSpeed * 2), jumpSpeed));
            rb2D.AddForce(new Vector2(0, jumpSpeed));
            walljumpL = false;
        }
        if (walljumpR)
        {
            movement = new Vector2(0, 0);
            rb2D.velocity = movement;
            //rb2D.AddForce(new Vector2((-jumpSpeed * 2), jumpSpeed));
            rb2D.AddForce(new Vector2(0, jumpSpeed));
            walljumpR = false;
        }

    }

	void OnCollisionEnter2D(Collision2D collision){
		if (collision.collider.CompareTag ("bullet")) {
			instance.GameOver ();
		}
	}

}