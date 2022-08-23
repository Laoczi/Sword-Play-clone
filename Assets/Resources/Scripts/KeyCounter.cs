using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCounter : MonoBehaviour
{
    int _currentKeyCountForLevel = 0;

    private void Start()
    {
        Debug.Log("key count: " + PlayerPrefs.GetInt("KeyCount"));
    }
    void OnCollectKey()
    {
        _currentKeyCountForLevel++;
    }
    void OnEndLevel()
    {
        int keyCount = PlayerPrefs.GetInt("KeyCount");

        keyCount += _currentKeyCountForLevel;

        PlayerPrefs.SetInt("KeyCount", keyCount > 3 ? 3 : keyCount);
    }
    private void OnEnable()
    {
        CollectableKey.onCutKey += OnCollectKey;
        CameraMovement.onReachedFinish += OnEndLevel;
    }
    private void OnDisable()
    {
        CollectableKey.onCutKey -= OnCollectKey;
        CameraMovement.onReachedFinish -= OnEndLevel;
    }
}
