using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Highscore : MonoBehaviour
{

	[SerializeField]
	private TextMeshProUGUI _scoreText;

	private void Start()
	{
		_scoreText.text = "Highscore : " + PlayerPrefs.GetInt("score") ;
	}

}
