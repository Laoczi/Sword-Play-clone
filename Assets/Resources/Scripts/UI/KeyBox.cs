using System;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class KeyBox : MonoBehaviour, IPointerClickHandler
{
    public static event Action onOpenChest;

    GameObject _lockPanel;
    MoneyVariant _moneyVariant;
    SkinVariant _skinVariant;

    bool _isOpen;
    bool _isInitiated;

    public void OnPointerClick(PointerEventData eventData)
    {
        int keyCount = PlayerPrefs.GetInt("KeyCount");

        if (KeyUI.singleton.openedBoxesCount > 2) return;
        if (keyCount <= 0) return;
        if (_isOpen) return;

        keyCount--;
        PlayerPrefs.SetInt("KeyCount", keyCount);

        _lockPanel.SetActive(false);
        _isOpen = true;

        onOpenChest?.Invoke();

        if (KeyUI.singleton.boxType == BoxType.reward) _moneyVariant.Show();
        else _skinVariant.Show();
    }
    private void OnEnable()
    {
        if (_isInitiated) return;

        _moneyVariant = new MoneyVariant();
        _skinVariant = new SkinVariant();

        _lockPanel = transform.GetChild(0).gameObject;

        _moneyVariant.panel = transform.GetChild(1).gameObject;
        _moneyVariant.panel.SetActive(false);
        _moneyVariant.rewardText = transform.GetChild(1).GetChild(2).GetComponent<TextMeshProUGUI>();

        _skinVariant.panel = transform.GetChild(2).gameObject;
        _skinVariant.panel.SetActive(false);

        Transform skinsPanel = transform.GetChild(2);
        _skinVariant.skins = new GameObject[skinsPanel.childCount - 1];

        for (int i = 1; i < skinsPanel.childCount; i++)
        {
            _skinVariant.skins[i - 1] = skinsPanel.GetChild(i).gameObject;
        }

        _isInitiated = true;
    }
}
[System.Serializable]
class MoneyVariant
{
    public GameObject panel;
    public TextMeshProUGUI rewardText;

    public void Show()
    {
        panel.SetActive(true);
        float reward = KeyUI.singleton.reward;
        rewardText.text = reward.ToString();

        Wallet.singleton.Add(reward);
        Debug.Log("add " + reward + " coins");
    }
}
[System.Serializable]
class SkinVariant
{
    public GameObject panel;
    public GameObject[] skins;
    public void Show()
    {
        panel.SetActive(true);

        int skinId = KeyUI.singleton.topPrizeSkinId;

        for (int i = 0; i < skins.Length; i++)
        {
            skins[i].SetActive(i == skinId);
        }

        PlayerPrefs.SetInt("OpenSkin " + skinId, 1);
        Debug.Log("open " + skinId + " skin");
    }
}
