using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PowerupsManager : MonoBehaviour
{
	[SerializeField]
	private float shieldCooldown, ammoCooldown;	
	private float timePassShield, timePassAmmo;		//	shield index = 1, ammo index = 2, health index = 3

	private bool isShieldActive, isAmmoActive;

	[SerializeField]
	private GameObject _shieldImage;
	[SerializeField]
	private GameObject _ammoImage;

	[SerializeField]
	private PlayerManager _playerManager;

	private void Start()
	{
		timePassShield = shieldCooldown;
		timePassAmmo = ammoCooldown;
	}

	private void Update()
	{
		if (isShieldActive) 
		{
			
			if (!_shieldImage.activeSelf) 
			{
				_shieldImage.SetActive(true);
			}

			timePassShield -= Time.deltaTime;
			float temp = timePassShield / shieldCooldown;
			_shieldImage.GetComponent<Image>().fillAmount = temp;

			if (timePassShield <= 0)
			{
				isShieldActive = false;
				_playerManager.isShiledActivate = false;
				timePassShield = shieldCooldown;
				_shieldImage.SetActive(false);
				_shieldImage.GetComponent<Image>().fillAmount = 1;
			}
		}

		if (isAmmoActive)
		{
			if (!_ammoImage.activeSelf)
			{
				_ammoImage.SetActive(true);
			}

			timePassAmmo -= Time.deltaTime;
			float temp = timePassAmmo / ammoCooldown;
			_ammoImage.GetComponent<Image>().fillAmount = temp;

			if (timePassAmmo <= 0)
			{
				isAmmoActive = false;
				_playerManager.isAmmoActivate = false;
				timePassAmmo = ammoCooldown;
				_ammoImage.SetActive(false);
				_shieldImage.GetComponent<Image>().fillAmount = 1;
			}
		}
	}

	public void SetShield() 
	{
		isShieldActive = true;
	}

	public void SetAmmo() 
	{
		isAmmoActive = true;
	}
}
