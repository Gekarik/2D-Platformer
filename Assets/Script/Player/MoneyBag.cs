using UnityEngine;

public class MoneyBag : MonoBehaviour
{
    [SerializeField] private int _countOfGold;

    public void AddMoney(int countOfMoney) => _countOfGold += countOfMoney;
}
