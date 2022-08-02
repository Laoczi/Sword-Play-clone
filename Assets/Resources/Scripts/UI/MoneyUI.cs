using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _currentMoney;

    private void Start()
    {
        _currentMoney.text = Wallet.singleton.count.ToString();
    }

    void OnMoneyChanged()
    {
        _currentMoney.text = Wallet.singleton.count.ToString();
    }
    private void OnEnable()
    {
        Wallet.onChanged += OnMoneyChanged;
    }
    private void OnDisable()
    {
        Wallet.onChanged -= OnMoneyChanged;
    }
}
