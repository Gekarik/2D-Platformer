using System.Collections;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private Coin _coinPrefab;
    [SerializeField] private float _delayBeforeSpawning;
    [SerializeField] private int _startPositionX;
    [SerializeField] private int _endPositionX;
    [SerializeField] private int _maxCoins;

    private int _coinsSpawned = 0;

    private void Start()
    {
        StartCoroutine(nameof(SpawnCoins));
    }

    private IEnumerator SpawnCoins()
    {
        var wait = new WaitForSeconds(_delayBeforeSpawning);

        while (true)
        {
            if (_coinsSpawned < _maxCoins)
                SpawnCoin();

            yield return wait;
        }
    }

    private void SpawnCoin()
    {
        Vector2 spawnPoint = new Vector2(Random.Range(_startPositionX, _endPositionX), transform.position.y);
        Coin coin = Instantiate(_coinPrefab, spawnPoint, Quaternion.identity);
        coin.Collected += HandleCoinDestroyed;
        _coinsSpawned++;
    }

    private void HandleCoinDestroyed(ICollectible coin)
    {
        _coinsSpawned--;
        coin.Collected -= HandleCoinDestroyed;
    }
}
