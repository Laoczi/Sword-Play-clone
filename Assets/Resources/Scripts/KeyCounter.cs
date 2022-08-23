using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCounter : MonoBehaviour
{
    public int currentCount { get; private set; }

    private void Start()
    {
        Debug.Log("key count: " + PlayerPrefs.GetInt("KeyCount"));
        currentCount = 0;
    }
    void OnCollectKey()
    {
        currentCount++;
    }
    void OnEndLevel()
    {
        int keyCount = PlayerPrefs.GetInt("KeyCount");

        keyCount += currentCount;

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
