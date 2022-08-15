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
        _panel.SetActive(true);
        Wallet.singleton.Add(70);
    }
    public void GoToNextLevel()
    {
        LevelProgress.singleton.GoToNextLevel();
    }
    void OnOpenSkin(int id)
    {
        _skinProgressPanel.SetActive(false);
        _newSkinPanel.SetActive(true);
        _newSkinIcons[id - 1].SetActive(true);
    }
    private void OnEnable()
    {
        SkinProgress.onOpenSkin += OnOpenSkin;
        CameraPathMoveControl.onReachedFinish += OnEndGame;
    }
    private void OnDisable()
    {
        SkinProgress.onOpenSkin += OnOpenSkin;
        CameraPathMoveControl.onReachedFinish -= OnEndGame;
    }
}
