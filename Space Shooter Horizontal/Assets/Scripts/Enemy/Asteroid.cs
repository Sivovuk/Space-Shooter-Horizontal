using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Asteroid : MonoBehaviour
{
	[SerializeField]
	private float _speed;

	void OnBecameInvisible()
	{
		Destroy(transform.parent.gameObject);
	}

	private void Update()
	{
		transform.Rotate(Vector3.forward * Time.deltaTime * _speed);
	}
}
