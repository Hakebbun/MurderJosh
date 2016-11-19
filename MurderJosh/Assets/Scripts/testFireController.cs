using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class testFireController : MonoBehaviour {
	public float bulletsInPlay = 0;
	public List<bulletController> bullets = new List<bulletController>();
	public GameObject player;
	public GameObject arm;
	public GameObject gun;
    public SpriteRenderer gunSprite;


    public bulletController bullet;
	float xVel, yVel;
	// Use this for initialization
	void Start () {
        gunSprite = gun.GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update () {


		/*
		 * Button checks to get gun directionality
		*/


		if(Input.GetButton("AimUp")){
			arm.SetActive (true);

			yVel = 1f;
		}

		if(Input.GetButton("AimRight")){
			arm.SetActive (true);
			xVel = 1f;

		}

		if(Input.GetButton("AimDown")){
			arm.SetActive (true);
			yVel = -1f;
		}

		if(Input.GetButton("AimLeft")){
			arm.SetActive (true);
			xVel = -1f;
		}

		if (Input.GetButtonUp ("AimUp") || Input.GetButtonUp ("AimDown")) {
			yVel = 0f;

		}

		if (Input.GetButtonUp ("AimRight") || Input.GetButtonUp ("AimLeft")) {
			xVel = 0f;

		}
			

		/*
		 * Button check to get fire 
		 */

		// Don't fire if we're not aiming
		if (Input.GetButtonDown("CustomFire") && !(yVel == 0 && xVel == 0) && !(Input.GetButton("SlowDown"))) {
			// Instantiate a new bullet
			bulletController curGO = (bulletController)Instantiate (bullet, gun.transform.position, Quaternion.identity);

			// Give it directional velocity
			curGO.GetComponent<Rigidbody2D> ().velocity = new Vector2 (xVel, yVel);

			//update bullets in play and list of bullets
			bulletsInPlay += 1;
			bullets.Add (curGO);
		}

		// Button check to do slowdown
		if(Input.GetButtonDown("SlowDown")){
			slowDown();
		}else if(Input.GetButtonUp("SlowDown")){
			endSlowDown();
		}


		// angle check for rotating gun

        if(xVel < 0)
        {
            gunSprite.flipY = true;
        }
        else
        {
            gunSprite.flipY = false;
        }

		// straight
		if (xVel == 1 && yVel == 0) {
			arm.transform.rotation = Quaternion.Euler (0, 0, 0);
		}

		// up and right
		if (xVel == 1 && yVel == 1) {
			arm.transform.rotation = Quaternion.Euler (0, 0, 45);

		}

		// straight up
		if (xVel == 0 && yVel == 1) {
			arm.transform.rotation = Quaternion.Euler (0, 0, 90);

		}

		// up and back
		if (xVel == -1 && yVel == 1) {
			arm.transform.rotation = Quaternion.Euler (0, 0, 135);
		}

		// straight back
		if (xVel == -1 && yVel == 0) {
			arm.transform.rotation = Quaternion.Euler (0, 0, 180);

		}

		// down and back
		if (xVel == -1 && yVel == -1) {
			arm.transform.rotation = Quaternion.Euler (0, 0, 225);

		}

		// straight down
		if (xVel == 0 && yVel == -1) {
			Debug.Log ("aim down");
			arm.transform.rotation = Quaternion.Euler (0, 0, 270);

		}

		// down and forward
		if (xVel == 1 && yVel == -1) {
			arm.transform.rotation = Quaternion.Euler (0, 0, 315);

		}

		// Inactive
		if (xVel == 0 && yVel == 0) {
			arm.SetActive (false);

		}

	}



	void slowDown(){
		Debug.Log ("slow down");

		foreach(bulletController bullet in bullets){
			bullet.gameObject.GetComponent<Rigidbody2D> ().velocity = bullet.gameObject.GetComponent<Rigidbody2D> ().velocity / 4;
		}
	}

	void endSlowDown(){
		Debug.Log ("End Slow down");

		foreach(bulletController bullet in bullets){
			bullet.gameObject.GetComponent<Rigidbody2D> ().velocity = bullet.gameObject.GetComponent<Rigidbody2D> ().velocity * 4;
		}
	}
}
