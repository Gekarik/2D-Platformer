using System.Collections;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private Transform _coinPrefab;
    [SerializeField] private float _delayBeforeSpawning = 2.0f;
    [SerializeField] private int startPositionX = -80;
    [SerializeField] private int endPositionX = -80;

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
            _spawnPoint = new Vector2(Random.Range(startPositionX, endPositionX),transform.position.y);
            Instantiate(_coinPrefab, _spawnPoint, Quaternion.identity);
            yield return wait;
        }
    }
}
