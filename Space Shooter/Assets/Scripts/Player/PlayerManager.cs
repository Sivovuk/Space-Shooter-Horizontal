using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerManager : MonoBehaviour
{
	[SerializeField]
	private int _health;
	public static int _score;

	[Space]

	[SerializeField]
	private float _speed;
	private float _timePassShooting;
	[SerializeField]
	private float _shootindCooldown;
	[SerializeField]
	private float _offsetX;
	[SerializeField]
	private float _offsetY;
	
	[Space]

	[SerializeField]
	private bool isReadyToFire = true;
	public static bool isGameFinish = false;
	
	[Space]
	
	[SerializeField]
	private GameObject _shootingPoint;
	[SerializeField]
	private GameObject _explosionPrefab;
	[SerializeField]
	private Animator _animator;
	[SerializeField]
	private TextMeshProUGUI _scoreText;
	[SerializeField]
	private AudioSource _fireSound;
	[SerializeField]
	private AudioSource _hitSound;

	[Space]

	[SerializeField]
	private GameManager _gameManager;
	[SerializeField]
	private PoolManager _poolManager;

	[Header("Powerups element")]

	[SerializeField]
	private PowerupsManager _powerupsManager;
	[SerializeField]
	private GameObject _shield;

	[Space]

	public bool isShiledActivate, isAmmoActivate;

	[Space]

	[SerializeField]
	private List<Sprite> _lives = new List<Sprite>();
	[SerializeField]
	private Image _playerLives;

	private void Start()
	{
		_score = 0;
		_scoreText.text = _score.ToString();
	}

	private void Update()
	{
		float horizontal = Input.GetAxis("Horizontal");
		float vertical = Input.GetAxis("Vertical");

		if (horizontal != 0 || vertical != 0) 
		{
			Movement(horizontal, vertical);
		}

		if (vertical > 0)
		{
			_animator.SetBool("TurnLeft", true);
		}
		else if (vertical < 0)
		{
			_animator.SetBool("TurnRight", true);
		}
		else
		{
			_animator.SetBool("TurnLeft", false);
			_animator.SetBool("TurnRight", false);
		}

		if (!isReadyToFire) 
		{
			_timePassShooting += Time.deltaTime;

			if (_timePassShooting >= _shootindCooldown) 
			{
				isReadyToFire = true;
				_timePassShooting = 0;
			}
		}

		if (Input.GetKeyDown(KeyCode.Space) && isReadyToFire) 
		{
			isReadyToFire = false;
			Shooting();
		}

		_scoreText.text = "Score : " + _score.ToString();
	}

	public void Movement(float horizontal, float vertical) 
	{
		float _screenWidth = Screen.width;
		float _screenHeight = Screen.height;

		Vector3 _offsetPosition = Camera.main.ScreenToWorldPoint(new Vector3(_screenWidth, _screenHeight, 0));
		_offsetPosition = new Vector3(_offsetPosition.x - _offsetX, _offsetPosition.y - _offsetY, 0);

		if (transform.position.x > -_offsetPosition.x && horizontal < 0 || transform.position.x < _offsetPosition.x && horizontal > 0)
		{
			transform.position = new Vector3(transform.position.x + horizontal * Time.deltaTime * _speed, transform.position.y, 0);
		}

		if (transform.position.y > -_offsetPosition.y && vertical < 0 || transform.position.y < _offsetPosition.y && vertical > 0)
		{
			transform.position = new Vector3(transform.position.x, transform.position.y + vertical * Time.deltaTime * _speed, 0);
		}
	}

	public void Shooting() 
	{
		GameObject spawn = _poolManager.SpawnPlayerAmmo();
		if (spawn != null) 
		{
			_fireSound.Play();
			spawn.transform.position = _shootingPoint.transform.position;
			if (isAmmoActivate) 
			{
				spawn.GetComponent<Ammo>().SetDamage(5);
			}
			spawn.SetActive(true);
		}
	}

	public void PlayerDied() 
	{
		_speed = 0;
		//	ovde da dodam deo za UI i gameover funkcije
		GameObject spawn = Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
		spawn.transform.localScale = new Vector2(2,2);
		Destroy(spawn, 1f);

		_gameManager.GameOver();

		Destroy(this.gameObject);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Enemy")) 
		{
			PlayerDied();
		}

		if (collision.CompareTag("Ammo"))
		{
			PlayerDamaged(collision.GetComponent<Ammo>().GetDamage());
		}
	}

	public void PlayerDamaged(int damage)
	{
		if (!isShiledActivate && _health > 0)
		{
			_health -= damage;
			_playerLives.sprite = _lives[_health - 1];
		}
		else 
		{
			isShiledActivate = false;
			_shield.SetActive(false);
		}
		
		_hitSound.Play();

		if (_health <= 0)
		{
			PlayerDied();
		}
	}

	public void ActivatePowerup(int index) 
	{
		if (index == 1) 
		{
			_powerupsManager.SetShield();
			isShiledActivate = true;
			_shield.SetActive(true);
		}
		else if (index == 2)
		{
			_powerupsManager.SetAmmo();
			isAmmoActivate = true;
		}
		else if (index == 3)
		{
			_health = 3;
			_playerLives.sprite = _lives[_health - 1];
		}
	}

}
