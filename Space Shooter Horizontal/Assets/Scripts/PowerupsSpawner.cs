using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PowerupsSpawner : MonoBehaviour
{
	[SerializeField]
	private List<GameObject> _powerups = new List<GameObject>();

	private float _timePass;
	[SerializeField]
	private float _cooldownSpawn;


	private void Update()
	{
		_timePass += Time.deltaTime;
		if (_timePass >= _cooldownSpawn)
		{
			_timePass = 0;

			Spawn(Random.Range(0, 3));
		}
	}

	public void Spawn(int index)
	{
		float width = Screen.width;
		float height = Screen.height;

		Vector2 position = Camera.main.ScreenToWorldPoint(new Vector2(width, height));
		float random = Random.Range(-position.y + 1, position.y - 1);

		GameObject spawn = Instantiate(_powerups[index]);

		spawn.transform.position = new Vector3(position.x, random, 0);
		spawn.transform.parent = transform;
	}
}
