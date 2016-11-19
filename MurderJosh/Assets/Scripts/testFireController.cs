using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class testFireController : MonoBehaviour {
	public float bulletsInPlay = 0;
	public List<bulletController> bullets = new List<bulletController>();


	public bulletController bullet;
	float xVel, yVel;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {


		/*
		 * Button checks to get gun directionality
		*/


		if(Input.GetButton("AimUp")){
			yVel = 1f;
		}

		if(Input.GetButton("AimRight")){
			xVel = 1f;
		}

		if(Input.GetButton("AimDown")){
			yVel = -1f;
		}

		if(Input.GetButton("AimLeft")){
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
		if (Input.GetButtonDown("CustomFire") && !(yVel == 0 && xVel == 0)) {
			// Instantiate a new bullet
			bulletController curGO = (bulletController)Instantiate (bullet, new Vector3 (0, 0, -0.1f), Quaternion.identity);

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
