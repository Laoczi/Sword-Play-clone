using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkinProgress : MonoBehaviour
{
    public static event Action<int> onOpenSkin;

    int _currentSkinID;
    float _currentSkinProgress;
    [SerializeField] float _progressPointsForCompletedLevel;
    [SerializeField] GameObject[] _skinIcons;
    [SerializeField] Image _progressBar;
    [SerializeField] TextMeshProUGUI _progressText;
    private void Start()
    {
        _currentSkinID = 1;

        for (int i = 0; i < 12; i++)//всего 12 скинов
        {
            if (PlayerPrefs.HasKey("OpenSkin " + i) == false)
            {
                _currentSkinID = i;
                break;
            }
        }

        _currentSkinProgress = 0;

        if (PlayerPrefs.HasKey("skinProgress")) _currentSkinProgress = PlayerPrefs.GetFloat("skinProgress");

        for (int i = 0; i < _skinIcons.Length; i++) _skinIcons[i].SetActive(i == (_currentSkinID - 1));
    }
    void OnLevelEnd()
    {
        _currentSkinProgress += _progressPointsForCompletedLevel;
        PlayerPrefs.SetFloat("skinProgress", _currentSkinProgress);

        if (_currentSkinProgress > 100) _currentSkinProgress = 100;

        _progressBar.fillAmount = _currentSkinProgress / 100;
        _progressText.text = _currentSkinProgress.ToString() + "%";

        if (_currentSkinProgress == 100)
        {
            ResetProgress();
            PlayerPrefs.SetInt("OpenSkin " + _currentSkinID, 1);
            onOpenSkin?.Invoke(_currentSkinID);
        }
    }
    void ResetProgress()
    {
        _currentSkinProgress = 0;
        PlayerPrefs.SetFloat("skinProgress", _currentSkinProgress);
    }
    private void OnEnable()
    {
        CameraPathMoveControl.onReachedFinish += OnLevelEnd;
    }
    private void OnDisable()
    {
        CameraPathMoveControl.onReachedFinish -= OnLevelEnd;
    }
}
