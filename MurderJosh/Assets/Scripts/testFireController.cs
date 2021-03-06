﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class testFireController : MonoBehaviour {
	public float bulletsInPlay = 0;
	public List<bulletController> bullets = new List<bulletController>();
	public GameObject player;
	public GameObject arm;
	public GameObject gun;
    public SpriteRenderer gunSprite;
	public PlayerController playerController;
	public GameController gameController;
	public float reload = 0f;

	public bool slow;


    public bulletController bullet;
	float xVel, yVel;
	// Use this for initialization
	void Start () {
        gunSprite = gun.GetComponent<SpriteRenderer>();
		slow = false;
    }
	
	// Update is called once per frame
	void Update () {
		// count down reload timer
		reload -= Time.deltaTime;

		gameController.updateReload (reload);


		/*
		 * Button checks to get gun directionality
		*/


		//if(Input.GetButton("AimUp")){
		if(Input.GetAxis("Vertical") < 0)
        {
			arm.SetActive (true);
			yVel = 1f;
		}

		if((Input.GetAxis("AimHorizontal") > 0) && !playerController.wallR){
			arm.SetActive (true);
			xVel = 1f;

		}

        //if(Input.GetButton("AimDown")){
        if (Input.GetAxis("Vertical") > 0)
        {
            arm.SetActive (true);
			yVel = -1f;
		}
        
		//if(Input.GetButton("AimLeft") && !playerController.wallL){
		if((Input.GetAxis("AimHorizontal") < 0) && !playerController.wallL){
			arm.SetActive (true);
			xVel = -1f;
		}
        
		//if (Input.GetButtonUp ("AimUp") || Input.GetButtonUp ("AimDown")) {
		if (Input.GetAxis("Vertical") == 0) {
			yVel = 0f;

		}
        
		//if (Input.GetButtonUp ("AimRight") || Input.GetButtonUp ("AimLeft")) {
		if (Input.GetAxis("AimHorizontal") == 0) {
			xVel = 0f;

		}
			

		/*
		 * Button check to get fire 
		 */

		// Don't fire if we're not aiming
		if (Input.GetButtonDown("CustomFire") && !(yVel == 0 && xVel == 0) && !(Input.GetButton("SlowDown")) && reload < 0) {
			// Instantiate a new bullet
			bulletController curGO = (bulletController)Instantiate (bullet, gun.transform.position, Quaternion.identity);

			// Give it directional velocity
			curGO.GetComponent<Rigidbody2D> ().velocity = new Vector2 (xVel, yVel);

			//update bullets in play and list of bullets
			bulletsInPlay += 1;
			bullets.Add (curGO);

			reload = 5;


		}

		// Button check to do slowdown
		if(Input.GetButtonDown("SlowDown")){
			slow = true;
			slowDown();
		}else if(Input.GetButtonUp("SlowDown")){
			slow = false;
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
