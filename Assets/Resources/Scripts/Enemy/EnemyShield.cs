using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyShield : Enemy
{
    [SerializeField] int _shieldXPCount;
    [SerializeField] TextMeshPro _shieldCount;
    [SerializeField] ParticleSystem _shieldEffect;

    [Header("Walk settings")]
    [SerializeField] bool _isNeedWalk;
    [SerializeField] float _baseWalkSpeed;
    [SerializeField] float _slowWalkSpeed;
    float _currentWalkSpeed;

    private void Start()
    {
        _shieldCount.text = _shieldXPCount.ToString();
        _currentWalkSpeed = _baseWalkSpeed;
    }
    protected override void DealDamage(Transform swordTransform)
    {
        _shieldXPCount--;
        _shieldEffect.Play();

        if (_shieldXPCount <= 0)
        {
            _shieldXPCount = 0;
            _animator.StopPlayback();
            _animator.enabled = false;

            GetComponent<Collider>().enabled = false;

            CallOnDeathEvent();
            _deathEffect.Play();
            StopAllCoroutines();

            _slicer.Cut(swordTransform);
        }

        _shieldCount.text = _shieldXPCount.ToString();
    }

    protected override void OnPlayerEnterRangeZone()
    {
        if (_isNeedWalk) StartCoroutine(WalkAndAttack());
        else _animator.SetTrigger("Attack");
    }
    protected override void OnPlayerEnterSlowZone()
    {
        base.OnPlayerEnterSlowZone();
        _currentWalkSpeed = _slowWalkSpeed;
    }
    IEnumerator WalkAndAttack()
    {
        float allTime = 1.1f;
        float currentTime = 0;
        _animator.SetTrigger("Walk");
        while (currentTime <= allTime)
        {
            transform.position += transform.forward * _currentWalkSpeed * Time.deltaTime;
            yield return new WaitForEndOfFrame();
            currentTime += Time.deltaTime;
        }
    }
}
