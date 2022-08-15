using System;
using UnityEngine;
using DynamicMeshCutter;

public abstract class Enemy : MonoBehaviour
{
    public static event Action onDeath;
    public static event Action onShoot;

    [Space(10)]
    [Header("Zone settings")]
    [SerializeField] protected EnemyRange _range;
    [SerializeField] protected EnemySlow _slow;
    [SerializeField] protected GameObject _fov;
    [Header("Animation settings")]
    [SerializeField] protected float _slowAnimationSpeed;
    [SerializeField] protected Animator _animator;
    [Header("Slice settings")]
    protected PlaneBehaviour _slicer;
    [SerializeField] protected ParticleSystem _deathEffect;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Sword")) DealDamage(other.transform);
    }
    protected abstract void DealDamage(Transform swordTransform);
    protected abstract void OnPlayerEnterRangeZone();
    protected virtual void OnPlayerEnterSlowZone()
    {
        _animator.SetFloat("Speed", _slowAnimationSpeed);
    }
    protected void CallOnDeathEvent() {
        Destroy(_range.gameObject);
        if (_slow != null) Destroy(_slow.gameObject);
        if (_fov != null) Destroy(_fov.gameObject);
        onDeath?.Invoke(); 
    }
    protected void CallOnShootEvent() { onShoot?.Invoke(); }
    private void OnEnable()
    {
        _slicer = GetComponent<PlaneBehaviour>();
        _range.onPlayerEnter += OnPlayerEnterRangeZone;
        if (_slow != null) _slow.onPlayerEnter += OnPlayerEnterSlowZone;
    }
    private void OnDisable()
    {
        _range.onPlayerEnter -= OnPlayerEnterRangeZone;
        if (_slow != null) _slow.onPlayerEnter -= OnPlayerEnterSlowZone;
    }
}
