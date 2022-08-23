using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCountUI : MonoBehaviour
{
    [SerializeField] float _duration;
    [SerializeField] GameObject _menu;
    [SerializeField] GameObject[] _keyIcons;
    [SerializeField] KeyCounter _keyCounter;
    void ShowMenu()
    {
        StartCoroutine(ShowProcess());
    }
    private void OnEnable()
    {
        CollectableKey.onCutKey += ShowMenu;
    }
    private void OnDisable()
    {
        CollectableKey.onCutKey -= ShowMenu;
    }
    IEnumerator ShowProcess()
    {
        _menu.SetActive(true);
        UpdateKeyIcons();
        yield return new WaitForSeconds(_duration);
        _menu.SetActive(false);
    }
    void UpdateKeyIcons()
    {
        int keyCount = PlayerPrefs.GetInt("KeyCount") + _keyCounter.currentCount;

        for (int i = 0; i < 3; i++)
        {
            _keyIcons[i].SetActive(keyCount - 1 >= i);
        }
        for (int i = 3; i < _keyIcons.Length; i++)
        {
            _keyIcons[i].SetActive(keyCount - 1 < i - 3);
        }
    }
}
