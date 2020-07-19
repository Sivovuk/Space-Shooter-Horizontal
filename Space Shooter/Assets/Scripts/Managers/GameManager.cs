using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
	const string SCORE_KEY = "score";

	[Header("In Game elements")]
	[SerializeField]
	private GameObject _inGameMenu;
	[SerializeField]
	private GameObject _menuButton;
	[SerializeField]
	private Button _continueButton;
	[SerializeField]
	private TextMeshProUGUI _winText;
	[SerializeField]
	private AudioSource _gameOver;

	public void GameOver() 
	{
		_gameOver.Play();
		_inGameMenu.SetActive(true);
		_menuButton.SetActive(false);
		_continueButton.interactable = false;
		_winText.transform.gameObject.SetActive(true);
		_winText.text = "Game Over!";
		SetNewHighScore(PlayerManager._score);
	}

	public void GameWon()
	{
		_inGameMenu.SetActive(true);
		_menuButton.SetActive(false);
		_continueButton.interactable = false;
		_winText.transform.gameObject.SetActive(true);
		_winText.text = "Congratulation! You have finish the game!";
		SetNewHighScore(PlayerManager._score);
	}


	public void OpenInGameMenu(GameObject menu) 
	{
		Time.timeScale = 0;
		menu.SetActive(true);
	}

	public void CloseInGameMenu(GameObject menu) 
	{
		Time.timeScale = 1;
		menu.SetActive(false);
	}


	public void OpenScene(int sceneIndex) 
	{
		if (SceneManager.GetActiveScene().buildIndex != 0)
		{
			SetNewHighScore(PlayerManager._score);
		}

		Application.LoadLevel(sceneIndex);
		Time.timeScale = 1;
		
	}

	public void Quit() 
	{
		if (SceneManager.GetActiveScene().buildIndex != 0)
		{
			SetNewHighScore(PlayerManager._score);
		}
		Application.Quit();
	}

	public void SetNewHighScore(int _score) 
	{
		int score = PlayerPrefs.GetInt(SCORE_KEY);
		if (_score > score) 
		{
			PlayerPrefs.SetInt(SCORE_KEY, _score);
		}
	}
}
