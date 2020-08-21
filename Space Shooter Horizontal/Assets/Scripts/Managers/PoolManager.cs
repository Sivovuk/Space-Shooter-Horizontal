using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PoolManager : MonoBehaviour
{
	[SerializeField]
	private int _ammoPlayerAmount;
	[SerializeField]
	private int _ammoTurrentAmount;
	[SerializeField]
	private int _ammoAircraftAmount;

	[Space]

	[SerializeField]
	private GameObject _ammoPlayerPrefab;
	[SerializeField]
	private GameObject _player;
	[SerializeField]
	private GameObject _ammoTurrentPrefab;
	[SerializeField]
	private GameObject _ammoAircraftPrefab;

	[Space]

	[SerializeField]
	private List<GameObject> _ammoPlayer = new List<GameObject>();
	[SerializeField]
	private List<GameObject> _ammoTurrent = new List<GameObject>();
	[SerializeField]
	private List<GameObject> _ammoAicraft = new List<GameObject>();

	[Space]
	[Header("Aircraft prefab")]
	[SerializeField]
	private int _aircraftAmount;
	[SerializeField]
	private GameObject _aircraftPrefab;
	[SerializeField]
	private Transform _parent;
	[SerializeField]
	private List<GameObject> _aicrafts = new List<GameObject>();

	private void Start()
	{
		//	spawning player ammo pool
		for (int i = 0; i < _ammoPlayerAmount; i++) 
		{
			GameObject spawn = Instantiate(_ammoPlayerPrefab);
			spawn.transform.parent = transform;
			_ammoPlayer.Add(spawn);
			spawn.GetComponent<Ammo>().SetPlayer(_player, 1);
		}

		//	spawning turrent ammo pool
		for (int i = 0; i < _ammoTurrentAmount; i++)
		{
			GameObject spawn = Instantiate(_ammoTurrentPrefab);
			spawn.transform.parent = transform;
			_ammoTurrent.Add(spawn);
			spawn.GetComponent<Ammo>().SetPlayer(_player, 0);
		}

		//	spawning aicraft ammo pool
		for (int i = 0; i < _ammoAircraftAmount; i++)
		{
			GameObject spawn = Instantiate(_ammoAircraftPrefab);
			spawn.transform.parent = transform;
			_ammoAicraft.Add(spawn);
			spawn.GetComponent<Ammo>().SetPlayer(_player, 0);
		}

		for (int i = 0; i < _aircraftAmount; i++)
		{
			GameObject spawn = Instantiate(_aircraftPrefab);
			spawn.transform.parent = _parent.transform;
			_aicrafts.Add(spawn);
		}
	}

	public GameObject SpawnPlayerAmmo() 
	{
		foreach (GameObject ammo in _ammoPlayer) 
		{
			if (!ammo.activeSelf) 
			{
				ammo.GetComponent<Ammo>().SetDamage(1);
				return ammo;
				
			}
		}

		return null;
	}

	public GameObject SpawnTurrentAmmo()
	{
		foreach (GameObject ammo in _ammoTurrent)
		{
			if (!ammo.activeSelf)
			{
				return ammo;

			}
		}

		return null;
	}

	public GameObject SpawnAicraftAmmo()
	{
		foreach (GameObject ammo in _ammoAicraft)
		{
			if (!ammo.activeSelf)
			{
				return ammo;

			}
		}

		return null;
	}

	public GameObject SpawnAicraft()
	{
		foreach (GameObject aircraft in _aicrafts)
		{
			if (!aircraft.activeSelf)
			{
				return aircraft;

			}
		}

		return null;
	}

}
