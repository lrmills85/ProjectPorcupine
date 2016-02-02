using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    int money = 1000;
    Text moneyText;

    void Start()
    {
        moneyText = GameObject.Find("MoneyText").GetComponent<Text>();
        UpdateMoneyUI();
    }

    public bool HasEnoughMoney(int price)
    {
        return money >= price;
    }

    public void SubtractMoney(int price)
    {
        money -= price;
        UpdateMoneyUI();
    }

    void UpdateMoneyUI()
    {
        moneyText.text = "$" + money.ToString();
    }
}
