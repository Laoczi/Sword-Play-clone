using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] GameObject _panel;

    private void Start()
    {
        _panel.SetActive(true);
    }
    void OnStartGame()
    {
        _panel.SetActive(false);
    }
    private void OnEnable()
    {
        StartTutor.onClick += OnStartGame;
    }
    private void OnDisable()
    {
        StartTutor.onClick -= OnStartGame;
    }
}
