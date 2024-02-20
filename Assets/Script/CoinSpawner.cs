using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private Transform _coinPrefab;
    [SerializeField] private float _delayBeforeSpawning = 2.0f;

    private Vector2 _spawnPoint;

    private void Start()
    {
        StartCoroutine(nameof(SpawnCoins));
    }

    private IEnumerator SpawnCoins()
    {
        var wait = new WaitForSeconds(_delayBeforeSpawning);

        while (true)
        {
            _spawnPoint = new Vector2(UnityEngine.Random.Range(-80, 80), 0);
            Instantiate(_coinPrefab, _spawnPoint, Quaternion.identity);
            yield return wait;
        }
    }
}
