using System.Collections;
using UnityEngine;

public class Factory : MonoBehaviour
{
    [SerializeField] private Coin _coinPrefab;
    [SerializeField] private AidKit _aidKitPrefab;
    [SerializeField] private float _delayBeforeSpawning;
    [SerializeField] private int _startPositionX;
    [SerializeField] private int _endPositionX;
    [SerializeField] private int _maxCoins;
    [SerializeField] private int _maxAidKits;

    private int _coinsSpawned = 0;
    private int _aidKitsSpawned = 0;

    private void Start()
    {
        StartCoroutine(nameof(SpawnItems));
    }

    private IEnumerator SpawnItems()
    {
        var wait = new WaitForSeconds(_delayBeforeSpawning);

        while (true)
        {
            if (_coinsSpawned < _maxCoins)
                SpawnItem(_coinPrefab, ref _coinsSpawned);

            if (_aidKitsSpawned < _maxAidKits)
                SpawnItem(_aidKitPrefab, ref _aidKitsSpawned);

            yield return wait;
        }
    }

    private void SpawnItem<T>(T prefab, ref int counter) where T : MonoBehaviour, ICollectible
    {
        Vector2 spawnPoint = new Vector2(Random.Range(_startPositionX, _endPositionX), transform.position.y);
        T item = Instantiate(prefab, spawnPoint, Quaternion.identity);
        item.Collected += HandleItemCollected;
        counter++;
    }

    private void HandleItemCollected(ICollectible collectible)
    {
        switch (collectible)
        {
            case AidKit kit:
                _aidKitsSpawned--;
                break;

            case Coin coin:
                _coinsSpawned--;
                break;
        }

        collectible.Collected -= HandleItemCollected;
        Destroy(((MonoBehaviour)collectible).gameObject);
    }
}
