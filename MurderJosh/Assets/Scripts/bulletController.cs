using UnityEngine;
using System.Collections;

public class bulletController : MonoBehaviour {
	public Rigidbody2D rb;
	Vector3 velocity; //Our movement velocity
	public float speed; //Speed of object


	// Use this for initialization
	void Start () {
		rb.velocity = new Vector2 (1, 1);
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.transform.position += velocity * Time.deltaTime * speed; //Update our position based on our new-found velocity
	}



	void OnCollisionEnter2D(Collision2D info) //When we run into something
	{
		Debug.Log ("it hapened");
		//'Bounce' off surface
		foreach(ContactPoint2D contact in info.contacts ) //Find collision point
		{
			//Find the BOUNCE of the object
			velocity = 2 * ( Vector3.Dot( velocity, Vector3.Normalize( contact.normal ) ) ) * Vector3.Normalize( contact.normal ) - velocity; //Following formula  v' = 2 * (v . n) * n - v

			velocity *= -1; //Had to multiply everything by -1. Don't know why, but it was all backwards.
		}
	}

}


