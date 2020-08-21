using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyManager : MonoBehaviour
{
	[SerializeField]
	private int _healthDisplayed;
	private int _health;
	[SerializeField]
	private int _scoreValue;


	[Space]

	[SerializeField]
	private float _speed;
	[Tooltip("This is only for the turret rotation segment")]
	[SerializeField]
	private float _offsetZ;
	[SerializeField]
	private float _firingCooldown;
	private float _timePass;
	[SerializeField]
	private float _explosionScale;

	[Space]

	[SerializeField]
	private bool up, down, left, right;
	[SerializeField]
	private bool isTurret, isAsteroid, isAircraft;

	[Space]

	[SerializeField]
	private GameObject _explosionEffectPrefab;
	[SerializeField]
	private GameObject _shootingPoint;
	[SerializeField]
	private Transform _player;
	[SerializeField]
	private AudioSource _fireSound;
	[SerializeField]
	private AudioSource _hitSound;

	[SerializeField]
	private PoolManager _poolManager;

	//[Space]

	//[Header("Aircraft Elements")]
	//[SerializeField]


	private void Start()
	{
		_poolManager = FindObjectOfType<PoolManager>();

		if (_poolManager == null)
		{
			Debug.LogError("Prazan pool manager");
		}

		_health = _healthDisplayed;
	}
	void OnBecameInvisible()
	{
		if (isAircraft) 
		{
			Level_3._counter--;
			gameObject.SetActive(false);
		}
	}

	private void OnDisable()
	{
		_healthDisplayed = _health;
	}

	private void Update()
	{
		if (isAsteroid || isAircraft)
		{
			if (up)
			{
				transform.Translate(Vector2.up * Time.deltaTime * _speed);
			}
			else if (down)
			{
				transform.Translate(Vector2.down * Time.deltaTime * _speed);
			}
			else if (left)
			{
				transform.Translate(Vector2.left * Time.deltaTime * _speed);
			}
			else if (right)
			{
				transform.Translate(Vector2.right * Time.deltaTime * _speed);
			}
		}

		if (isTurret && _player != null) 
		{
			_timePass += Time.deltaTime;
			if (_timePass >= _firingCooldown)
			{
				_timePass = 0;
				ShootingTurret();
			}

			Vector3 dir = _player.position - transform.position;
			float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
			Quaternion rotation = Quaternion.Euler(0, 0, angle + _offsetZ);
			transform.rotation = Quaternion.Lerp(transform.rotation, rotation, _speed * Time.deltaTime);
		}

		if (isAircraft) 
		{
			_timePass += Time.deltaTime;
			if (_timePass >= _firingCooldown)
			{
				_timePass = 0;
				ShootingAicraft();
			}
		}
	}

	public void ShootingTurret()
	{
		GameObject spawn = _poolManager.SpawnTurrentAmmo();

		if (spawn != null)
		{
			_fireSound.Play();
			spawn.GetComponent<Ammo>().SetDirection(1);
			spawn.transform.rotation = transform.rotation;
			spawn.transform.position = _shootingPoint.transform.position;
			spawn.SetActive(true);
		}
	}

	public void ShootingAicraft()
	{
		GameObject spawn = _poolManager.SpawnAicraftAmmo();

		if (spawn != null)
		{
			_fireSound.Play();
			spawn.transform.position = _shootingPoint.transform.position;
			spawn.SetActive(true);
		}
	}


	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Ammo") && collision.GetComponent<Ammo>().GetIndex() == 1) 
		{
			EnemyDamaged(collision.GetComponent<Ammo>().GetDamage());
		}

	}

	public void EnemyDamaged(int damage) 
	{
		_healthDisplayed -= damage;
		_hitSound.Play();

		if (_healthDisplayed <= 0) 
		{
			EnemyDied();
		}
	}

	public void EnemyDied()
	{
		PlayerManager._score += _scoreValue;

		GameObject spawn = Instantiate(_explosionEffectPrefab, transform.position, Quaternion.identity);
		spawn.transform.localScale = new Vector2(_explosionScale, _explosionScale);
		Destroy(spawn, 1f);

		if (isAircraft)
		{
			gameObject.SetActive(false);
			if (Level_3._counter > 0) 
			{
				Level_3._counter--;
			}
			
		}
		else 
		{
			Destroy(this.gameObject, 0.25f);
		}
	}

	public void SetPlayer(Transform player) 
	{
		_player = player;
	}
}
