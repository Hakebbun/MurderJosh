﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	private float score;
	private float scoreMultiplier;
	public bool gameOver;											//TODO: make this private
	private bool restart;

	public testFireController tfc;

	public Text scoreText;
	public Text scoreMultiplierText;
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
		scoreMultiplierText.text = "Multiplier: " + scoreMultiplier;
		gameOverText.text = "";
		restartText.text = "";
		score = 0;
		InvokeRepeating("UpdateEverySecond", 0f, 1.0f);
	}

	// Update is called once per frame
	void Update () {
		updateScoreText ();
		scoreMultiplier = Mathf.Pow (tfc.bulletsInPlay, 2);
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
	void GameOver(){
		gameOverText.text = "Game Over";
		gameOver = true;
	}

}
