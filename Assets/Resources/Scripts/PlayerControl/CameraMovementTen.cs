using System;
using System.Collections;
using UnityEngine;
using PathCreation;

public class CameraMovementTen : MonoBehaviour
{
    CameraMovement _playerCamera;

    int _enemyCount = 4;
    int _currentDeadEnemys = 0;

    private void Start()
    {
        _playerCamera = FindObjectOfType<CameraMovement>();
    }
    void OnKillEnemy()
    {
        _currentDeadEnemys++;

        if (_currentDeadEnemys == _enemyCount) _playerCamera.EndMove();
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
