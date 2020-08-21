using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Level_2 : MonoBehaviour
{
    public static bool isLevelTwoFinish;
    private bool isLevelTwoLoaded;

    [Space]

    [SerializeField]
    private Transform _player;
    [SerializeField]
    private GameObject _levelText;
    [SerializeField]
    private Level_3 _levelThree;

    [Space]

    [SerializeField]
    private List<Transform> _positions = new List<Transform>();
    [SerializeField]
    private List<GameObject> _turrents = new List<GameObject>();

    private void Start()
    {
        isLevelTwoFinish = false;
        StartCoroutine(LateStart());
    }

    private void Update()
    {
        if (isLevelTwoLoaded)
        {
            if (_turrents.Count > 0)
            {
                for (int i = 0; i < _turrents.Count; i++)
                {
                    if (_turrents[i] == null)
                    {
                        _turrents.RemoveAt(i);
                        break;
                    }
                }
            }

            if (_positions.Count > 0 && _turrents.Count > 0)
            {
                for (int i = 0; i < _turrents.Count; i++)
                {
                    _turrents[i].transform.position = Vector2.MoveTowards(_turrents[i].transform.position, _positions[i].position, 0.05f);
                }
            }
        }

        if (_turrents.Count <= 0 && !isLevelTwoFinish) 
        {
            isLevelTwoFinish = true;
            isLevelTwoLoaded = false;
            if (!PlayerManager.isGameFinish)
            {
                _levelText.SetActive(true);
            }
            _levelThree.enabled = true;
            this.enabled = false;
        }
    }

    IEnumerator LateStart() 
    {
        yield return new WaitForSeconds(1);
        isLevelTwoLoaded = true;

        yield return new WaitForSeconds(2);
        _levelText.SetActive(false);

        for (int i = 0; i < _turrents.Count; i++)
        {
            _turrents[i].GetComponent<EnemyManager>().SetPlayer(_player);
        }
    }

}
