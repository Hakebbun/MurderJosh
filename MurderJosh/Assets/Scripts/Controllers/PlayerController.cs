using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class PlayerController : MonoBehaviour
{
	public List<PlayerModel> mPlayers = new List<PlayerModel>();
	public GameController mGameController;
	private float lockPos = 0;
    [HideInInspector]



	public void startup(PlayerModel playerModel, GameObject curObj){
		curObj.GetComponent<SpriteRenderer> ().flipX = false;
		playerModel.score = 0;
		mPlayers.Add (playerModel);
	}

	public void checkState(PlayerModel playerModel, GameObject curObj){
		bool wasGrounded = playerModel.grounded;
		playerModel.grounded = Physics2D.Linecast (curObj.transform.position, playerModel.groundCheck.position, 1 << LayerMask.NameToLayer (Globals.LAYER_NAME_GROUND));

		Animator curAnimator = curObj.GetComponent<Animator> ();

		// if we just landed, play
		if (!wasGrounded && playerModel.grounded && curAnimator.GetCurrentAnimatorStateInfo (0).IsName (Globals.ANIM_NAME_FLOATD)) {
			curAnimator.SetTrigger (Globals.ANIM_TRIGGER_LAND_IDLE);
		}

		playerModel.wallL = Physics2D.Linecast(curObj.transform.position, playerModel.wallCheckL.position, 1 << LayerMask.NameToLayer("Ground"));
		playerModel.wallR = Physics2D.Linecast(curObj.transform.position, playerModel.wallCheckR.position, 1 << LayerMask.NameToLayer("Ground"));

		// Lock rotation
		curObj.transform.rotation = Quaternion.Euler(lockPos, lockPos, lockPos);


	}

	public void onJumpPressed(PlayerModel playerModel, GameObject gameObject){
		if (playerModel.grounded)
		{
			playerModel.jump = true;
			gameObject.GetComponent<Animator>().SetTrigger(Globals.ANIM_TRIGGER_JUMP_IDLE);

		}
		else if (playerModel.wallR)
		{
			playerModel.walljumpR = true;
			gameObject.GetComponent<Animator>().SetTrigger(Globals.ANIM_TRIGGER_JUMP_IDLE);

		}
		else if (playerModel.wallL)
		{
			playerModel.walljumpL = true;
			gameObject.GetComponent<Animator>().SetTrigger(Globals.ANIM_TRIGGER_JUMP_IDLE);

		}
	}

	public void onHAxis(PlayerModel playerModel, GameObject gameObject){
		gameObject.GetComponent<Animator>().SetTrigger(Globals.ANIM_TRIGGER_STOP_WALKING);
	}
		
	public void checkPhysicsState(PlayerModel playerModel, GameObject gameObject){
	// EMPTY

	}

	public void onFaceRight(PlayerModel playerModel, GameObject gameObject){
		gameObject.GetComponent<SpriteRenderer>().flipX = false;

	}

	public void onFaceLeft(PlayerModel playerModel, GameObject gameObject){
		gameObject.GetComponent<SpriteRenderer>().flipX = true;

	}

	public void onWalk(PlayerModel playerModel, GameObject gameObject){
		gameObject.GetComponent<Animator>().SetTrigger (Globals.ANIM_TRIGGER_WALK);

	}

	public void onFalling(PlayerModel playerModel, GameObject gameObject){
		gameObject.GetComponent<Animator>().SetTrigger ("floatDown");

	}

	public void onJump(PlayerModel playerModel, GameObject gameObject){
		gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, playerModel.jumpSpeed));
		playerModel.jump = false;
	}

	public void onWallJumpL(PlayerModel playerModel, GameObject gameObject){
		Vector2 newMovement = new Vector2(0, 0);
		gameObject.GetComponent<Rigidbody2D>().velocity = newMovement;
		gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(playerModel.jumpSpeed, playerModel.jumpSpeed));
		playerModel.walljumpL = false;
	}

	public void onWallJumpR(PlayerModel playerModel, GameObject gameObject){
		Vector2 newMovement = new Vector2(0, 0);
		gameObject.GetComponent<Rigidbody2D>().velocity = newMovement;
		gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-playerModel.jumpSpeed, playerModel.jumpSpeed));
		playerModel.walljumpR = false;
	}
		

	public void onBulletCollision(PlayerModel playerModel, GameObject gameObject){
		mGameController.GameOver ();
	}
		

	public void disablePlayers(){
		foreach(PlayerModel pm in mPlayers){
			pm.enabled = false;
		}
	}

}