using UnityEngine;

public class PlayerMoney : MonoBehaviour
{
    [SerializeField] private int _moneyBag;

    public void AddMoney(int countOfMoney) => _moneyBag += countOfMoney;
}
