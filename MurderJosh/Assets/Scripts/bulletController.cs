using UnityEngine;
using System.Collections;

public class bulletController : MonoBehaviour {
	public Rigidbody2D rb;
	private Vector2 oldVelocity;

	public float fireSpeed = 16f;

	// Use this for initialization
	void Start () {
		rb.velocity = rb.velocity  * fireSpeed;

		// freeze the rotation so it doesnt go spinning after a collision
		rb.freezeRotation = true;
	}


	void FixedUpdate () {
		// because we want the velocity after physics, we put this in fixed update
		oldVelocity = rb.velocity;
	}


	// Update is called once per frame
	void Update () {
		
	}



	// when a collision happens
	void OnCollisionEnter2D (Collision2D collision) {

			// get the point of contact
			ContactPoint2D contact = collision.contacts [0];

			// reflect our old velocity off the contact point's normal vector
			Vector2 reflectedVelocity = Vector2.Reflect (oldVelocity, contact.normal);        

			// assign the reflected velocity back to the rigidbody
			rb.velocity = reflectedVelocity;

			// rotate the object by the same ammount we changed its velocity
			Quaternion rotation = Quaternion.FromToRotation (oldVelocity, reflectedVelocity);

			transform.rotation = rotation * transform.rotation;

		}
	}




