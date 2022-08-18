using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class InGameUI : MonoBehaviour
{
    [SerializeField] GameObject _panel;
    [SerializeField] Image _levelProgress;
    [SerializeField] TextMeshProUGUI _currentLevelProgress;
    [SerializeField] TextMeshProUGUI _currentLevel;
    [SerializeField] GameObject _comboEffect;

    Transform _playerTransform;
    Transform _endPoint;
    float _baseDistance;
    
    private void Start()
    {
        _panel.SetActive(false);
        _comboEffect.SetActive(false);

        _playerTransform = FindObjectOfType<CameraPathMoveControl>().transform;
        Path path = FindObjectOfType<Path>();
        _endPoint = path.GetPoint(path.lenght - 1);

        _baseDistance = Vector3.Distance(_playerTransform.position, _endPoint.position);

        _currentLevelProgress.text = LevelProgress.level.ToString();
        _currentLevel.text = "Level " + LevelProgress.level.ToString();
    }
    void OnStartGame()
    {
        _panel.SetActive(true);
    }
    void OnEndGame()
    {
        _panel.SetActive(false);
    }
    private void Update()
    {
        if(_panel.activeInHierarchy) _levelProgress.fillAmount = 1 - (Vector3.Distance(_playerTransform.position, _endPoint.position) / _baseDistance);
    }
    public void GoMainMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    void OnStartCombo()
    {
        _comboEffect.SetActive(true);
    }
    void OnEndCombo()
    {
        _comboEffect.SetActive(false);
    }
    private void OnEnable()
    {
        ComboSystem.onStartCombo += OnStartCombo;
        ComboSystem.onEndCombo += OnEndCombo;
        StartTutor.onClick += OnStartGame;
        CameraPathMoveControl.onReachedFinish += OnEndGame;
        CameraPathMoveControl.onDeath += OnEndGame;
    }
    private void OnDisable()
    {
        ComboSystem.onStartCombo -= OnStartCombo;
        ComboSystem.onEndCombo -= OnEndCombo;
        StartTutor.onClick -= OnStartGame;
        CameraPathMoveControl.onReachedFinish -= OnEndGame;
        CameraPathMoveControl.onDeath -= OnEndGame;
    }
}
