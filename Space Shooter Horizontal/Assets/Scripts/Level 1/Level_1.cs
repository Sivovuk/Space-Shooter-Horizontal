using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Level_1 : MonoBehaviour
{
	public static bool isLevelOneFinish;
	private bool isLevelOneLoaded;

	[SerializeField]
	private List<GameObject> _asteroids = new List<GameObject>();

	[SerializeField]
	private GameObject _levelText;
	[SerializeField]
	private Level_2 _levelTwo;

	private void Start()
	{
		isLevelOneFinish = false;

		foreach (GameObject asteroid in _asteroids) 
		{
			asteroid.SetActive(true);
		}
		isLevelOneLoaded = true;
	}

	private void LateUpdate()
	{
		if (isLevelOneLoaded && !isLevelOneFinish) 
		{
			if (_asteroids.Count <= 0) 
			{
				isLevelOneLoaded = false;
				isLevelOneFinish = true;
				if (!PlayerManager.isGameFinish)
				{
					_levelText.SetActive(true);
				}
				_levelTwo.enabled = true;
				this.enabled = false;
			}
		}

		if (isLevelOneLoaded)
		{
			if (_asteroids.Count > 0)
			{
				for (int i = 0; i < _asteroids.Count; i++)
				{
					if (_asteroids[i] == null)
					{
						_asteroids.RemoveAt(i);
						break;
					}
				}
			}
		}
	}

}
