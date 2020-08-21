using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Ammo : MonoBehaviour
{
    [SerializeField]
    private int _damage;
    [SerializeField]
    private int _index;

    [Space]

    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _scaleSize;

    [Space]

    [SerializeField]
    private bool up, down, left, right = false;

    [Space]

    [SerializeField]
    private GameObject _explosionPrefab;
    [SerializeField]
    private GameObject _player;

    void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }

    private void Update()
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && _index == 1 || collision.CompareTag("Player") && _index == 0)
        {
            DestroyAmmo();
        }
    }

    public void DestroyAmmo() 
    {
        _speed = 0;
        GameObject spawn = Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
        spawn.transform.localScale = new Vector2(_scaleSize, _scaleSize);
        Destroy(spawn, 1f);
        StartCoroutine(DeactivateObject(0));
    }

    public void SetPlayer(GameObject player, int index) 
    {
        _player = player;
        _index = index;
    }

    IEnumerator DeactivateObject(float timePass) 
    {
        yield return new WaitForSeconds(timePass);
        _speed = 20;
        gameObject.SetActive(false);
    }

    public int GetDamage() 
    {
        return _damage;
    }
    public int GetIndex()
    {
        return _index;
    }


    public void SetDirection(int index) 
    {
        up = down = right = left = false;
        if (index == 1) 
        {
            up = true;
        }
        else if (index == 2)
        {
            down = true;
        }
        else if (index == 3)
        {
            right = true;
        }
        else if (index == 4)
        {
            left = true;
        }
    }

    public void SetDamage(int damage) 
    {
        _damage = damage;
    }
}
