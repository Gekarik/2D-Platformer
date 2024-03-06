using UnityEngine;

[RequireComponent(typeof(Health), typeof(MoneyBag))]
public class Collector : MonoBehaviour
{
    private MoneyBag _bag;
    private Health _health;
    private void Start()
    {
        _health = GetComponent<Health>();
        _bag = GetComponent<MoneyBag>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out ICollectible item))
        {
            switch (item)
            {
                case AidKit kit:
                    HandleAidKit(kit);
                    break;
                case Coin coin:
                    HandleCoin(coin);
                    break;
            }

            item.RespondToCollection();
        }
    }

    private void HandleAidKit(AidKit kit) => _health.Heal(kit.HealPoints);
    private void HandleCoin(Coin coin) => _bag.AddMoney(coin.Cost);
}