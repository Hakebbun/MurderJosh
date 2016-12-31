using UnityEngine;
using System.Collections;

public class PlayerModel : MonoBehaviour {
	public int score;
	public bool grounded;
	public bool jump = false;
	public bool wallL = false;
	public bool wallR = false;
	public bool walljumpL = false;
	public bool walljumpR = false;
	public float movementSpeed;
	public float jumpSpeed;                             //Speed at which player should jump
	public float maxSpeed = 5f;     

	public Transform wallCheckL;
	public Transform wallCheckR;
	public Transform groundCheck;
	public PlayerController mController;

	// Use this for initialization
	void Start () {
		mController.startup (this, gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		mController.checkState (this, gameObject);

		// input checks
		if (Input.GetButtonDown (Globals.INPUT_JUMP)) {
			mController.onJumpPressed (this, gameObject);
		}

		if (Input.GetAxis (Globals.AXIS_H)==0 && gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName(Globals.ANIM_NAME_WALK)) {
			mController.onHAxis (this, gameObject);
		}
	}


	void FixedUpdate(){
		mController.checkPhysicsState (this, gameObject);


		float moveHorizontal = Input.GetAxis(Globals.AXIS_H);
		Rigidbody2D curRB2D = gameObject.GetComponent<Rigidbody2D> ();
		Vector2 movement = new Vector2((moveHorizontal * movementSpeed), curRB2D.velocity.y);
		curRB2D.velocity = movement;

		Vector2 realMovement = curRB2D.transform.InverseTransformDirection(curRB2D.velocity);


		if (movement.x > 0) {
			mController.onFaceRight (this, gameObject);
		} else if (movement.x < 0) {
			mController.onFaceLeft (this, gameObject);
		} else {
			// CATCH
		}

		// if we're moving, grounded, and not jumping, our state is walk
		if (moveHorizontal != 0 && grounded && !jump) {
			mController.onWalk (this, gameObject);
		}

		// if we're not grounded and floating up, our state is falling
		if (!grounded && realMovement.y < 0 && gameObject.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0).IsName (Globals.ANIM_NAME_FLOAT_UP)) {
			mController.onFalling (this, gameObject);

		} else if (grounded && jump) {
			// If we're grounded and jump, then we are on our way up
			mController.onJump (this, gameObject);


		} else if (walljumpL) {
			mController.onWallJumpL (this, gameObject);
		} else if (walljumpR) {
			mController.onWallJumpR (this, gameObject);
		}
	}

	void OnCollisionEnter2D(Collision2D collision){
		if (collision.collider.CompareTag (Globals.COLLIDER_TAG_BULLET)) {
			mController.onBulletCollision (this, gameObject);
		}
	}


}
