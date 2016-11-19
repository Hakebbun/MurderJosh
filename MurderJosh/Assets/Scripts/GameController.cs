using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	private float score;
	private float scoreMultiplier;
	public bool gameOver;											//TODO: make this private
	private bool restart;


	public int numberOfBullets;										//TODO: This is just for testing purposes. For the actual game get this number for real
	public Text scoreText;
	public Text gameOverText;
	public Text restartText;
	public static GameController instance = null;

	// Use this for initialization
	void Awake () {
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy(gameObject);

		DontDestroyOnLoad(gameObject);
	}

	// Use this for initialization
	void Start () {
		scoreText.text = "Score: " + score;
		gameOverText.text = "";
		restartText.text = "";
		score = 0;
		InvokeRepeating("UpdateEverySecond", 0f, 1.0f);
	}

	// Update is called once per frame
	void Update () {
		scoreMultiplier = Mathf.Pow (numberOfBullets, 2);
		Debug.Log ("Score Multiplier: " + scoreMultiplier);
		if (gameOver) {
			restartText.text = "Press 'R' to Restart";
			restart = true;
		}
		if (restart) {
			if (Input.GetKeyDown (KeyCode.R)) {
				int scene = SceneManager.GetActiveScene ().buildIndex;
				SceneManager.LoadScene (scene, LoadSceneMode.Single);
			}
		}
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

	/// <summary>
	/// Games the over.
	/// </summary>
	void GameOver(){
		gameOverText.text = "Game Over";
		gameOver = true;
	}

}
