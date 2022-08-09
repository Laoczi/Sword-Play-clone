using System;
using UnityEngine;
using TMPro;

public class UnlockRandomSkinButton : MonoBehaviour
{
    [SerializeField] GameObject _gray;
    [SerializeField] TextMeshProUGUI _priceText;
    [SerializeField] float _price;

    private void Start()
    {
        UpdateUI();
    }
    void UpdateUI()
    {
        _gray.SetActive(_price > Wallet.singleton.count);
        _priceText.text = _price.ToString();
    }
    private void OnEnable()
    {
        Wallet.onChanged += UpdateUI;
    }
    private void OnDisable()
    {
        Wallet.onChanged -= UpdateUI;
    }
}
