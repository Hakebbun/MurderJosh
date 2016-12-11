using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	private float score;
	private float scoreMultiplier;
	private bool gameOver;											
	private bool restart;
	public PlayerController mPlayerController;
	public testFireController tfc;

	public Text scoreText;
	public Text scoreMultiplierText;
	public Text gameOverText;
	public Text restartText;
	public Text reloadCounter;
	public static GameController instance = null;


	// Use this for initialization
	void Awake () {
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy(gameObject);
	}

	// Use this for initialization
	void Start () {
		gameOver = false;
		restart = false;
		score = 0;
		scoreMultiplier = 0;

		scoreText.text = "Score: " + score;
		scoreMultiplierText.text = "Multiplier: " + scoreMultiplier;
		gameOverText.text = "";
		restartText.text = "";

		InvokeRepeating("UpdateEverySecond", 0f, 1.0f);
	}

	// Update is called once per frame
	void Update () {
		updateScoreText ();
		scoreMultiplier = Mathf.Pow (tfc.bulletsInPlay, 2);
		if (gameOver) {
			restartText.text = "Press 'R' to Restart";
			restart = true;
			mPlayerController.disablePlayers();
			tfc.enabled = false;
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
		//if the game isn't over and the bullets aren't travelling slowly
		if (!gameOver && !tfc.slow)
			score = score + scoreMultiplier;
	}

	/// <summary>
	/// Updates the score text.
	/// </summary>
	void updateScoreText(){
		scoreText.text = "Score: " + score;
		scoreMultiplierText.text = "Multiplier: " + scoreMultiplier;
	}

	/// <summary>
	/// Games the over.
	/// </summary>
	public void GameOver(){
		gameOverText.text = "Game Over";
		gameOver = true;
	}

	// Updates the reload timer
	public void updateReload(float count){
		reloadCounter.text = "Reload in: " + count.ToString("0");
	}

}
