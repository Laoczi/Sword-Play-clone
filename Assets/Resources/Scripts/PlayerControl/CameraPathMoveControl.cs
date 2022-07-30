using System;
using System.Collections;
using UnityEngine;
using PathCreation;

public class CameraPathMoveControl : MonoBehaviour
{
    public static event Action onDeath;
    public static event Action onReachedFinish;

    [SerializeField] Camera _playerCamera;
    [SerializeField] PathCreator _path;
    [Header("Move speed settings")]
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
    [SerializeField] float _lookDuration;
    Coroutine _clearLookTarget;

    float distanceTraveled;
    float _maxPathDistance;

    bool _isReachedEnd;
    bool _isDead;

    Transform _lookTarget;

    private void Start()
    {
        _maxPathDistance = _path.path.length;
        _isReachedEnd = false;
        _isDead = false;
        _currentMoveSpeed = _defaultMoveSpeed;
        _currentFov = _defaultFov;
    }

    private void Update()
    {
        if (_isDead) return;
        if (_isReachedEnd) return;

        distanceTraveled += _currentMoveSpeed * Time.deltaTime;

        if(distanceTraveled >= _maxPathDistance)
        {
            _isReachedEnd = true;
            distanceTraveled = _maxPathDistance - 0.01f;
            onReachedFinish?.Invoke();
        }
        //moving along path
        transform.position = _path.path.GetPointAtDistance(distanceTraveled);
        //looking on target or get rotation at path point
        if(_lookTarget != null)
        {
            Vector3 lookDirection = _lookTarget.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(lookDirection);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, _smoothLook * Time.deltaTime);
        }
        else
        {
            Quaternion nextRotation = _path.path.GetRotationAtDistance(distanceTraveled);
            transform.rotation = Quaternion.Euler(nextRotation.eulerAngles.x, nextRotation.eulerAngles.y, 0);
        }

        _playerCamera.fieldOfView = Mathf.MoveTowards(_playerCamera.fieldOfView, _currentFov, _smoothFov * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            _isDead = true;
            onDeath?.Invoke();
            Debug.Log("я умир");
        }
        if (other.CompareTag("EnemySlow"))
        {
            SetSlowMovement();

            _lookTarget = other.transform;

            if (_clearLookTarget != null) StopCoroutine(_clearLookTarget);
            _clearLookTarget = StartCoroutine(ClearLookTarget());
        }
    }
    void SetSlowMovement()
    {
        _currentMoveSpeed = _slowMoveSpeed;
        _currentFov = _slowFov;
    }
    void SetDefaultMovement()
    {
        _currentMoveSpeed = _defaultMoveSpeed;
        _currentFov = _defaultFov;

        if (_clearLookTarget != null) StopCoroutine(_clearLookTarget);

        _clearLookTarget = null;
        _lookTarget = null;
    }
    IEnumerator ClearLookTarget()
    {
        yield return new WaitForSeconds(_lookDuration);
        _lookTarget = null;
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
