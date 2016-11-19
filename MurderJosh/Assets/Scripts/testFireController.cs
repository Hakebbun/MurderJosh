using UnityEngine;
using System.Collections;

public class testFireController : MonoBehaviour {
	public bulletController bullet;
	float xVel, yVel;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetButton("AimUp")){
//			Debug.Log ("Cur Dir =" + xVel + "," + yVel);
			yVel = 1f;
		}

		if(Input.GetButton("AimRight")){
//			Debug.Log ("Cur Dir =" + xVel + "," + yVel);
			xVel = 1f;
		}

		if(Input.GetButton("AimDown")){
//			Debug.Log ("Cur Dir =" + xVel + "," + yVel);
			yVel = -1f;
		}

		if(Input.GetButton("AimLeft")){
//			Debug.Log ("Cur Dir =" + xVel + "," + yVel);
			xVel = -1f;
		}

		if (Input.GetButtonUp ("AimUp") || Input.GetButtonUp ("AimDown")) {
			yVel = 0f;
//			Debug.Log ("Cur Dir =" + xVel + "," + yVel);

		}

		if (Input.GetButtonUp ("AimRight") || Input.GetButtonUp ("AimLeft")) {
			xVel = 0f;
//			Debug.Log ("Cur Dir =" + xVel + "," + yVel);

		}
			








		if (Input.GetButtonDown("CustomFire")) {
			bulletController curGO = (bulletController)Instantiate (bullet, new Vector3 (0, 0, -0.1f), Quaternion.identity);
			curGO.GetComponent<Rigidbody2D> ().velocity = new Vector2 (xVel, yVel);
			Debug.Log ("Cur Dir =" + xVel + "," + yVel);

		}
	}
}
