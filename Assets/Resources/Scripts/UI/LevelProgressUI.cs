using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelProgressUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _currentLevel;
    [SerializeField] GameObject[] _currentEnvironmentIcons;
    [SerializeField] GameObject[] _nextEnvironmentIcons;
    [SerializeField] Image[] _levelProgressPoints;
    [Header("Point colors")]
    [SerializeField] Color _previousLevelColor;
    [SerializeField] Color _currentLevelColor;
    [SerializeField] Color _nextLevelColor;
    private void OnLevelWasLoaded(int level)
    {
        int currentLevel = LevelProgress.singleton.currentLevel;
        int levelDozens = 0;

        _currentLevel.text = "Level " + currentLevel.ToString();

        currentLevel -= 1;

        while (currentLevel >= 10)
        {
            currentLevel -= 10;
            levelDozens++;
        }

        for (int i = 0; i < _currentEnvironmentIcons.Length; i++)
        {
            if (i == levelDozens) _currentEnvironmentIcons[i].SetActive(true);
            else _currentEnvironmentIcons[i].SetActive(false);
        }
        for (int i = 0; i < _nextEnvironmentIcons.Length; i++)
        {
            if (i == levelDozens) _nextEnvironmentIcons[i].SetActive(true);
            else _nextEnvironmentIcons[i].SetActive(false);
        }

        for (int i = 0; i < _levelProgressPoints.Length; i++)
        {
            if (i < currentLevel) _levelProgressPoints[i].color = _previousLevelColor;
            else if (i == currentLevel) _levelProgressPoints[i].color = _currentLevelColor;
            else if (i > currentLevel) _levelProgressPoints[i].color = _nextLevelColor;
        }
    }
}
