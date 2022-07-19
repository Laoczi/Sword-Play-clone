using System;
using UnityEngine;
using PathCreation;

public class CameraPathMoveControl : MonoBehaviour
{
    public static event Action onDeath;
    public static event Action onReachedFinish;

    [SerializeField] PathCreator _path;

    [SerializeField] float _defaultMoveSpeed;
    [SerializeField] float _slowMoveSpeed;

    float _currentMoveSpeed;

    float distanceTraveled;
    float _maxPathDistance;

    bool _isReachedEnd;

    bool _isDead;

    private void Start()
    {
        _currentMoveSpeed = _defaultMoveSpeed;
        _maxPathDistance = _path.path.length;
        _isReachedEnd = false;
        _isDead = false;
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

        transform.position = _path.path.GetPointAtDistance(distanceTraveled);
        Quaternion nextRotation = _path.path.GetRotationAtDistance(distanceTraveled);

        transform.rotation = Quaternion.Euler(nextRotation.eulerAngles.x, nextRotation.eulerAngles.y, 0);
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("EnemyRange"))
        {
            _currentMoveSpeed = _slowMoveSpeed;
        }
        if (other.CompareTag("Enemy"))
        {
            _isDead = true;
            onDeath?.Invoke();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("EnemyRange"))
        {
            _currentMoveSpeed = _defaultMoveSpeed;
        }
    }
}
