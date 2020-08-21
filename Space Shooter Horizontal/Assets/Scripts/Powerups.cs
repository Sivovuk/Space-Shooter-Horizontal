using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Powerups : MonoBehaviour
{
	[SerializeField]
	private int _index;
	[SerializeField]
	private float _speed;

	private void OnBecameInvisible()
	{
		Destroy(gameObject);
	}

	private void Update()
	{
		transform.Translate(Vector2.left * Time.deltaTime * _speed);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player")) 
		{
			collision.GetComponent<PlayerManager>().ActivatePowerup(_index);
			Destroy(gameObject);
		}
	}

}
