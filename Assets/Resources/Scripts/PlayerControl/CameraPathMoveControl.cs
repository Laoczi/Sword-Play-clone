using System;
using UnityEngine;
using PathCreation;

public class CameraPathMoveControl : MonoBehaviour
{
    public static event Action onDeath;
    public static event Action onReachedFinish;

    [SerializeField] Camera _playerCamera;
    [SerializeField] PathCreator _path;
    [SerializeField] float _slowTimeScale;
    [Header("Move speed settings")]
    [SerializeField] float _moveSpeed;
    [Header("FOV settings")]
    [SerializeField] float _defaultFov;
    [SerializeField] float _slowFov;
    [SerializeField] float _smoothFov;
    float _currentFov;

    float distanceTraveled;
    float _maxPathDistance;

    bool _isReachedEnd;
    bool _isDead;

    private void Start()
    {
        _maxPathDistance = _path.path.length;
        _isReachedEnd = false;
        _isDead = false;
        _currentFov = _defaultFov;
    }

    private void Update()
    {
        if (_isDead) return;
        if (_isReachedEnd) return;

        distanceTraveled += _moveSpeed * Time.deltaTime;

        if(distanceTraveled >= _maxPathDistance)
        {
            _isReachedEnd = true;
            distanceTraveled = _maxPathDistance - 0.01f;
            onReachedFinish?.Invoke();
        }

        transform.position = _path.path.GetPointAtDistance(distanceTraveled);

        Quaternion nextRotation = _path.path.GetRotationAtDistance(distanceTraveled);
        transform.rotation = Quaternion.Euler(nextRotation.eulerAngles.x, nextRotation.eulerAngles.y, 0);

        _playerCamera.fieldOfView = Mathf.MoveTowards(_playerCamera.fieldOfView, _currentFov, _smoothFov * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            _isDead = true;
            onDeath?.Invoke();
            Debug.Log("� ����");
        }
        if (other.CompareTag("EnemySlow"))
        {
            SetSlowMovement();
        }
    }
    void SetSlowMovement()
    {
        Time.timeScale = _slowTimeScale;
    }
    void SetDefaultMovement()
    {
        Time.timeScale = 1;
    }
    private void OnEnable()
    {
        Enemy.onDeath += SetDefaultMovement;
    }
    private void OnDisable()
    {
        Enemy.onDeath -= SetDefaultMovement;
    }
}
