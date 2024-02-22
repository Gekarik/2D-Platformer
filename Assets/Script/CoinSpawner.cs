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
    private int _coinsCollected = 0;

    private void OnEnable()
    {
        CoinGrabber.OnCoinCollected += HandleCoinCollected;
    }

    private void OnDisable()
    {
        CoinGrabber.OnCoinCollected -= HandleCoinCollected;
    }

    private void Start()
    {
        StartCoroutine(nameof(SpawnCoins));
    }

    private void HandleCoinCollected(Coin coin)
    {
        _coinsCollected++;
    }

    private IEnumerator SpawnCoins()
    {
        var wait = new WaitForSeconds(_delayBeforeSpawning);

        while (true)
        {
            if ((_coinsSpawned - _coinsCollected) < _maxCoins)
            {
                Vector2 spawnPoint = new Vector2(Random.Range(_startPositionX, _endPositionX), transform.position.y);
                Instantiate(_coinPrefab, spawnPoint, Quaternion.identity);
                _coinsSpawned++;
            }
            yield return wait;
        }
    }
}
