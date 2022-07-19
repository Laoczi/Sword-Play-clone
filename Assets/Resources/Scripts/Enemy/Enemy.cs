using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected Animator _animator;
    [SerializeField] protected Collider _triggerRange;
    [SerializeField] protected EnemyCheckPlayerInRange _triggerRangePlayerCheck;
    [SerializeField] protected GameObject _sliceTarget;

    protected abstract void DealDamage(Transform swordTransform);

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Sword"))
        {
            DealDamage(other.transform);
        }
    }
    protected abstract void OnPlayerEnterRangeZone();
    private void OnEnable()
    {
        _triggerRangePlayerCheck.onPlayerEnter += OnPlayerEnterRangeZone;
    }
    private void OnDisable()
    {
        _triggerRangePlayerCheck.onPlayerEnter -= OnPlayerEnterRangeZone;
    }
}
