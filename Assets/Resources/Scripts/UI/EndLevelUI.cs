using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevelUI : MonoBehaviour
{
    [SerializeField] GameObject _panel;
    [SerializeField] GameObject _newSkinPanel;
    [SerializeField] GameObject _skinProgressPanel;
    [SerializeField] GameObject[] _newSkinIcons;

    private void Start()
    {
        _panel.SetActive(false);
        _newSkinPanel.SetActive(false);
        _skinProgressPanel.SetActive(true);

        foreach (var item in _newSkinIcons)
        {
            item.SetActive(false);
        }
    }
    void OnEndGame()
    {
        Wallet.singleton.Add(70);

        int keyCount = PlayerPrefs.GetInt("KeyCount");
        if (keyCount >= 3) return;

        _panel.SetActive(true);
    }
    public void GoToNextLevel()
    {
        LevelProgress.singleton.GoToNextLevel();
    }
    void OnOpenSkin(int id)
    {
        int keyCount = PlayerPrefs.GetInt("KeyCount");
        if (keyCount >= 3) return;

        _panel.SetActive(true);
        _skinProgressPanel.SetActive(false);
        _newSkinPanel.SetActive(true);
        _newSkinIcons[id - 1].SetActive(true);
    }
    private void OnEnable()
    {
        SkinProgress.onOpenSkin += OnOpenSkin;
        CameraMovement.onReachedFinish += OnEndGame;
    }
    private void OnDisable()
    {
        SkinProgress.onOpenSkin -= OnOpenSkin;
        CameraMovement.onReachedFinish -= OnEndGame;
    }
}
