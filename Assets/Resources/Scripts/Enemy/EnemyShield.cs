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
        _animator.SetFloat("Speed", 1);
        _currentWalkSpeed = _baseWalkSpeed;
        _animator.SetTrigger("Attack");
    }
    protected override void OnPlayerEnterSlowZone()
    {
        base.OnPlayerEnterSlowZone();
        _animator.SetFloat("Speed", _slowAnimationSpeed);
        _currentWalkSpeed = _slowWalkSpeed;
        StartCoroutine(WalkForward());
    }
    IEnumerator WalkForward()
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
