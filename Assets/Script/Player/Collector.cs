using UnityEngine;

[RequireComponent(typeof(PlayerHealth), typeof(PlayerMoney))]
public class Collector : MonoBehaviour
{
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

    private void HandleAidKit(AidKit kit)
    {
        var playerHealth = GetComponent<PlayerHealth>();
        playerHealth.Heal(kit.HealPoints);
    }

    private void HandleCoin(Coin coin)
    {
        PlayerMoney playerMoney = GetComponent<PlayerMoney>();
        playerMoney.AddMoney(coin.Cost);
    }
}