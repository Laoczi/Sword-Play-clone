using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCounter : MonoBehaviour
{
    public int currentCount { get; private set; }


    [SerializeField] float _duration;
    [SerializeField] GameObject _menu;
    [SerializeField] GameObject[] _keyIcons;
    void ShowMenu()
    {
        StartCoroutine(ShowProcess());
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
        currentCount++;

        int keyCount = PlayerPrefs.GetInt("KeyCount") + currentCount;

        for (int i = 0; i < 3; i++)
        {
            _keyIcons[i].SetActive(keyCount - 1 >= i);
        }
        for (int i = 3; i < _keyIcons.Length; i++)
        {
            _keyIcons[i].SetActive(keyCount - 1 < i - 3);
        }
    }

    private void Start()
    {
        Debug.Log("key count: " + PlayerPrefs.GetInt("KeyCount"));
        currentCount = 0;
    }
    void OnEndLevel()
    {
        int keyCount = PlayerPrefs.GetInt("KeyCount");

        keyCount += currentCount;

        PlayerPrefs.SetInt("KeyCount", keyCount > 3 ? 3 : keyCount);
    }
    private void OnEnable()
    {
        CollectableKey.onCutKey += ShowMenu;
        CameraMovement.onReachedFinish += OnEndLevel;
    }
    private void OnDisable()
    {
        CollectableKey.onCutKey -= ShowMenu;
        CameraMovement.onReachedFinish -= OnEndLevel;
    }
}
