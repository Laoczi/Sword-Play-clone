using System;
using System.Collections;
using UnityEngine;
using PathCreation;

public class CameraPathMoveControl : MonoBehaviour
{
    public static event Action onDeath;
    public static event Action onReachedFinish;

    [SerializeField] Camera _playerCamera;
    [SerializeField] Path _path;
    [Header("Move speed settings")]
    [SerializeField] float _minDistanceToGoOnNextPoint;
    [SerializeField] float _defaultMoveSpeed;
    [SerializeField] float _slowMoveSpeed;
    float _currentMoveSpeed;
    [Header("FOV settings")]
    [SerializeField] float _defaultFov;
    [SerializeField] float _slowFov;
    [SerializeField] float _smoothFov;
    float _currentFov;
    [Header("Look settings")]
    [SerializeField] float _smoothLook;

    int _pathLenght;
    int _currentPathPoint;

    bool _isReachedEnd;
    bool _isDead;

    Transform _lookTarget;

    bool _gameIsStarted;

    private void Start()
    {
        _pathLenght = _path.lenght;
        _isReachedEnd = false;
        _isDead = false;
        _currentMoveSpeed = _defaultMoveSpeed;
        _currentFov = _defaultFov;

        _gameIsStarted = true;
    }

    private void Update()
    {
        if (_gameIsStarted == false) return;
        if (_isDead) return;
        if (_isReachedEnd) return;

        Transform nextPoint = _path.GetPoint(_currentPathPoint);

        if (_currentPathPoint >= _pathLenght)
        {
            _isReachedEnd = true;
            onReachedFinish?.Invoke();
            return;
        }
        //moving along path
        transform.position = Vector3.MoveTowards(transform.position, nextPoint.position, _currentMoveSpeed * Time.deltaTime);
        //looking on target or get rotation at path point
        if(_lookTarget == null)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, nextPoint.rotation, _smoothLook * Time.deltaTime);
        }
        else
        {
            Vector3 lookDirection = _lookTarget.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(lookDirection);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, _smoothLook * Time.deltaTime);
        }
        //camera fov control
        _playerCamera.fieldOfView = Mathf.MoveTowards(_playerCamera.fieldOfView, _currentFov, _smoothFov * Time.deltaTime);

        if (Vector3.Distance(transform.position, nextPoint.position) < _minDistanceToGoOnNextPoint) _currentPathPoint++;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            OnDeath();
        }
        if (other.CompareTag("Shuriken"))
        {
            OnDeath();
        }
        if (other.CompareTag("EnemySlow"))
        {
            SetSlowMovement();
        }
        if (other.CompareTag("EnemyFov"))
        {
            _currentFov = _slowFov;
            _lookTarget = other.transform;

        }
    }
    void OnDeath()
    {
        _isDead = true;
        onDeath?.Invoke();
        Debug.Log("я умир");
    }
    void SetSlowMovement()
    {
        _currentMoveSpeed = _slowMoveSpeed;
    }
    void SetDefaultMovementAndLook()
    {
        _currentMoveSpeed = _defaultMoveSpeed;
        _lookTarget = null;
        _currentFov = _defaultFov;
    }
    void OnStartGame()
    {
        _gameIsStarted = true;
    }
    private void OnEnable()
    {
        StartTutor.onClick += OnStartGame;
        Bullet.onDead += SetDefaultMovementAndLook;
        Enemy.onDeath += SetDefaultMovementAndLook;
    }
    private void OnDisable()
    {
        StartTutor.onClick -= OnStartGame;
        Bullet.onDead -= SetDefaultMovementAndLook;
        Enemy.onDeath -= SetDefaultMovementAndLook;
    }
}
