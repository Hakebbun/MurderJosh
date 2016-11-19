using UnityEngine;
using System.Collections;

public class bulletController : MonoBehaviour {
	public Rigidbody2D rb;
	private Vector2 oldVelocity;

	public float fireSpeed = 16f;

	// Use this for initialization
	void Start () {
//		// set our laser on its merry way. no need to update transform manually
//		// Initialise with a random vector, normalize it then multiply by bullet speed
//		Vector2 initVector = new Vector2(Random.Range(-1f,1f), Random.Range(-1f,1f)) ;
//		initVector.Normalize();
//		initVector = initVector;

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




