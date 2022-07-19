using System;
using UnityEngine;
using PathCreation;

public class CameraPathMoveControl : MonoBehaviour
{
    public static event Action onReachedFinish;

    [SerializeField] PathCreator _path;

    [SerializeField] float _defaultMoveSpeed;
    [SerializeField] float _slowMoveSpeed;

    float _currentMoveSpeed;

    float distanceTraveled;
    float _maxPathDistance;

    bool _isReachedEnd;

    private void Start()
    {
        _currentMoveSpeed = _defaultMoveSpeed;
        _maxPathDistance = _path.path.length;
        _isReachedEnd = false;
    }

    private void Update()
    {
        if (_isReachedEnd) return;

        distanceTraveled += _currentMoveSpeed * Time.deltaTime;

        if(distanceTraveled >= _maxPathDistance)
        {
            _isReachedEnd = true;
            distanceTraveled = _maxPathDistance - 0.01f;
            onReachedFinish?.Invoke();
        }

        transform.position = _path.path.GetPointAtDistance(distanceTraveled);
        transform.rotation = _path.path.GetRotationAtDistance(distanceTraveled);
    }
}
