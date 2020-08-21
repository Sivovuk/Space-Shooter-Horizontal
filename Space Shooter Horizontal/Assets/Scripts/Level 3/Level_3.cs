using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Level_3 : MonoBehaviour
{
    public static int _counter;

    [SerializeField]
    private float _levelTimer;
    private float timePassLevel;
    [SerializeField]
    private float _cooldawnSpawn;
    private float _timePass;

    public static bool isLevelThreeFinish;
    private bool isLevelThreeLoaded;

    [SerializeField]
    private GameObject _levelText;
    [SerializeField]
    private Image _timerImage;
    [SerializeField]
    private PoolManager _poolManger;
    [SerializeField]
    private GameManager _gameManager;

    private void Start()
    {
        isLevelThreeFinish = false;
        StartCoroutine(LateStart());

        timePassLevel = _levelTimer;
    }

    private void Update()
    {
        if (isLevelThreeLoaded && _counter < 19)
        {
            _timePass += Time.deltaTime;
            if (_timePass >= _cooldawnSpawn)
            {
                _timePass = 0;

                float width = Screen.width;
                float height = Screen.height;

                Vector2 position = Camera.main.ScreenToWorldPoint(new Vector2(width, height));

                Spawn(position);
            }
        }

        if (isLevelThreeLoaded) 
        {
            if (!_timerImage.transform.gameObject.activeSelf) 
            {
                _timerImage.transform.gameObject.SetActive(true);
            }

            timePassLevel -= Time.deltaTime;
            float temp = timePassLevel / _levelTimer;
            _timerImage.GetComponent<Image>().fillAmount = temp;

            if (timePassLevel <= 0) 
            {
                _timerImage.transform.gameObject.SetActive(false);
                _gameManager.GameWon();
                this.enabled = false;
            }
        }
    }

    public void Spawn(Vector2 position) 
    {
        GameObject spawn = _poolManger.SpawnAicraft();

        if (spawn != null) 
        {
            float random = Random.Range(-position.y + 1, position.y - 1);


            spawn.transform.position = new Vector3(position.x, random, 0);
            spawn.SetActive(true);
            _counter++;
        }
    }

    IEnumerator LateStart()
    {
        yield return new WaitForSeconds(1);
        _levelText.SetActive(false);
        isLevelThreeLoaded = true;
    }
}
