using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FailedUI : MonoBehaviour
{
    [SerializeField] GameObject _panel;
    [SerializeField] float _showDelay;

    private void Start()
    {
        _panel.SetActive(false);
    }
    void OnPlayerDead()
    {
        StartCoroutine(ShowMenuWithDelay());
    }
    IEnumerator ShowMenuWithDelay()
    {
        yield return new WaitForSeconds(_showDelay);
        _panel.SetActive(true);
    }
    public void RetryLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    private void OnEnable()
    {
        CameraPathMoveControl.onDeath += OnPlayerDead;
    }
    private void OnDisable()
    {
        CameraPathMoveControl.onDeath -= OnPlayerDead;
    }
}
