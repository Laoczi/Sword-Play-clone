using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySwordTen : Enemy
{
    [SerializeField] bool _isNeedWalk;
    [SerializeField] float _walkSpeed;
    [SerializeField] float _walkDuration;
    protected override void DealDamage(Transform swordTransform)
    {
        _animator.StopPlayback();
        _animator.enabled = false;

        GetComponent<Collider>().enabled = false;

        CallOnDeathEvent();
        _deathEffect.Play();
        StopAllCoroutines();

        _slicer.Cut(swordTransform);
    }
    private void Start()
    {
        StartTutor.onClick += OnPlayerEnterRangeZone;
    }
    protected override void OnPlayerEnterRangeZone()
    {
        StartTutor.onClick -= OnPlayerEnterRangeZone;

        if (_isNeedWalk)
        {
            StartCoroutine(WalkAndAttack());
        }
        else
        {
            _animator.SetTrigger("Attack");
        }
    }
    IEnumerator WalkAndAttack()
    {
        float currentTime = 0;
        _animator.SetTrigger("Walk");
        while (currentTime <= _walkDuration)
        {
            transform.position += transform.forward * _walkSpeed * Time.deltaTime;
            yield return new WaitForEndOfFrame();
            currentTime += Time.deltaTime;
        }
        _animator.SetTrigger("Attack");
    }
}
