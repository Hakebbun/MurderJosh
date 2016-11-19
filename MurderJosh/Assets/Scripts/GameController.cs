using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	private float score;
	private float scoreMultiplier;

	public int numberOfBullets;										//TODO: This is just for testing purposes. For the actual game get this number for real
	public Text scoreText;

	// Use this for initialization
	void Start () {
		updateScoreText ();
		score = 0;
		InvokeRepeating("UpdateEverySecond", 0f, 1.0f);
	}

	// Update is called once per frame
	void Update () {
		scoreMultiplier = Mathf.Log10 (10 * numberOfBullets);
		Debug.Log ("Score Multiplier: " + scoreMultiplier);
	}

	/// <summary>
	/// Code that runs every second
	/// Meant for updating the score
	/// </summary>
	void UpdateEverySecond(){
		score = score + scoreMultiplier;
		updateScoreText ();
	}

	/// <summary>
	/// Updates the score text.
	/// </summary>
	void updateScoreText(){
		scoreText.text = "Score: " + score;
	}

}
