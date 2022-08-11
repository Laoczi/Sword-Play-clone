using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevelUI : MonoBehaviour
{
    [SerializeField] GameObject _panel;

    private void Start()
    {
        _panel.SetActive(false);
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
    private void OnEnable()
    {
        CameraPathMoveControl.onReachedFinish += OnEndGame;
    }
    private void OnDisable()
    {
        CameraPathMoveControl.onReachedFinish -= OnEndGame;
    }
}
