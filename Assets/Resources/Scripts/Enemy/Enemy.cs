using System;
using UnityEngine;
using DynamicMeshCutter;

public abstract class Enemy : CutterBehaviour
{
    public static event Action onDeath;

    [Space(10)]
    [SerializeField] protected EnemyRange _range;
    [SerializeField] protected EnemySlow _slow;
    [SerializeField] protected float _slowAnimationSpeed;
    [SerializeField] protected Animator _animator;
    protected PlaneBehaviour _slicer;
    [SerializeField] protected ParticleSystem _deathEffect;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Sword")) DealDamage(other.transform);
    }
    protected abstract void DealDamage(Transform swordTransform);
    protected abstract void OnPlayerEnterRangeZone();
    private void OnPlayerEnterSlowZone()
    {
        _animator.SetFloat("Speed", _slowAnimationSpeed);
    }
    protected void CallOnDeathEvent() { onDeath?.Invoke(); }
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
