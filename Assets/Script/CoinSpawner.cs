using System.Collections;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private Coin _coinPrefab;
    [SerializeField] private float _delayBeforeSpawning;
    [SerializeField] private int _startPositionX;
    [SerializeField] private int _endPositionX;
    [SerializeField] private int _maxCoins;

    private CoinGrabber _grabber;
    private int _coinsSpawned = 0;

    private void Start()
    {
        _grabber = FindAnyObjectByType<CoinGrabber>();
        StartCoroutine(nameof(SpawnCoins));
    }

    private void OnEnable() => _grabber.OnCoinCollected += HandleCollectedCoins;

    private void OnDisable() => _grabber.OnCoinCollected -= HandleCollectedCoins;

    private void HandleCollectedCoins(Coin coin) => _coinsSpawned--;

    private IEnumerator SpawnCoins()
    {
        var wait = new WaitForSeconds(_delayBeforeSpawning);

        while (true)
        {
            if (_coinsSpawned < _maxCoins)
            {
                Vector2 spawnPoint = new Vector2(Random.Range(_startPositionX, _endPositionX), transform.position.y);
                Instantiate(_coinPrefab, spawnPoint, Quaternion.identity);
                _coinsSpawned++;
            }
            yield return wait;
        }
    }
}
