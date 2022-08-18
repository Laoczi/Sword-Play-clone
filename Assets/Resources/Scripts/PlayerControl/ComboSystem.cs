using System;
using UnityEngine;

public class ComboSystem : MonoBehaviour
{
    public static event Action onStartCombo;
    public static event Action onEndCombo;

    [SerializeField] float _maxSecondsBetweenKills;

    bool _comboIsStarted;
    int _currentComboCount;
    float _nextKill;

    private void Start()
    {
        _comboIsStarted = false;
        _currentComboCount = 0;
        _nextKill = _maxSecondsBetweenKills;
    }
    private void Update()
    {
        if (_comboIsStarted) _nextKill -= Time.deltaTime;
        else return;

        if (_nextKill <= 0)
        {
            onEndCombo?.Invoke();
            _comboIsStarted = false;
            _nextKill = _maxSecondsBetweenKills;
            _currentComboCount = 0;
        }
    }

    void OnKillEnemy()
    {
        if (_currentComboCount >= 2) return;

        if (_comboIsStarted == false)
        {
            _comboIsStarted = true;
            _currentComboCount = 1;
        }
        else
        {
            _nextKill = _maxSecondsBetweenKills;
            _currentComboCount++;

            if(_currentComboCount >= 2) onStartCombo?.Invoke();
        }
    }

    private void OnEnable()
    {
        Enemy.onDeath += OnKillEnemy;
    }
    private void OnDisable()
    {
        Enemy.onDeath -= OnKillEnemy;
    }
}
