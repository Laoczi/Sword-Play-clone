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
    [SerializeField] GameObject _progressMenu;
    [SerializeField] GameObject[] _skinIcons;
    [SerializeField] Image _progressBar;
    [SerializeField] TextMeshProUGUI _progressText;
    private void Start()
    {
        _currentSkinID = 1;

        if (PlayerPrefs.HasKey("openedSkin"))//���� ���� ���� ������� ������ ��������� (�� �� ������� ��� ����)
        {
            _currentSkinID = PlayerPrefs.GetInt("openedSkin");//�� �� ��������� ��� �� ���������� � ����� ���������, ���� �� ��� ����� ��������
            if(PlayerPrefs.HasKey("OpenSkin " + _currentSkinID)) SetNewOpenedSkin();//���� �������� ����� ����, (������ �� ����� ��������), �� �������� �������� � �������� �����
        }
        else
        {
            SetNewOpenedSkin();
        }

        _currentSkinProgress = 0;

        if (PlayerPrefs.HasKey("skinProgress")) _currentSkinProgress = PlayerPrefs.GetFloat("skinProgress");

        for (int i = 0; i < _skinIcons.Length; i++) _skinIcons[i].SetActive(i == (_currentSkinID - 1));
    }
    void SetNewOpenedSkin()
    {
        ResetProgress();

        for (int i = 0; i < _skinIcons.Length; i++)//���� ��� �������, �� �� ������ �������� ������ ������
        {
            if (PlayerPrefs.HasKey("OpenSkin " + i) == false)
            {
                _currentSkinID = i;
                PlayerPrefs.SetInt("openedSkin", i);
                return;
            }
        }

        _progressMenu.SetActive(false);
    }
    void OnLevelEnd()
    {
        _currentSkinProgress += _progressPointsForCompletedLevel;
        if (_currentSkinProgress > 100) _currentSkinProgress = 100;

        PlayerPrefs.SetFloat("skinProgress", _currentSkinProgress);

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
        CameraMovement.onReachedFinish += OnLevelEnd;
    }
    private void OnDisable()
    {
        CameraMovement.onReachedFinish -= OnLevelEnd;
    }
}
