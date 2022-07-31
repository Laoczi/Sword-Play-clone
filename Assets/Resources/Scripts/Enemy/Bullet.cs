using System;
using UnityEngine;
using DynamicMeshCutter;

public class Bullet : MonoBehaviour
{
    public static event Action onDead;

    [SerializeField] float _defaultSpeed;
    [SerializeField] float _slowSpeed;
    float _currentSpeed;
    [SerializeField] EnemySlow _slow;
    [SerializeField] PlaneBehaviour _slicer;
    bool _isSliced;
    private void Awake()
    {
        _isSliced = false;
        _currentSpeed = _defaultSpeed;

        if (_slow != null) _slow.onPlayerEnter += OnPlayerEnterSlowZone;
    }
    void Update()
    {
        if (_isSliced) return;
        transform.position += transform.forward * _currentSpeed * Time.deltaTime;
    }
    void OnPlayerEnterSlowZone()
    {
        _currentSpeed = _slowSpeed;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (_isSliced) return;
        if (other.CompareTag("Sword"))
        {
            _isSliced = true;
            tag = "Untagged";
            onDead?.Invoke();
            _slicer.Cut(other.transform);
        }
    }
    private void OnDisable()
    {
        _slow.onPlayerEnter -= OnPlayerEnterSlowZone;
    }
}
