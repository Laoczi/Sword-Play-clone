using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DynamicMeshCutter;

public class EnemySword : Enemy
{
    [SerializeField] bool _isNeedWalk;
    [SerializeField] float _walkSpeed;
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
    protected override void OnPlayerEnterRangeZone()
    {
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
        float allTime = 1.033f;
        float currentTime = 0;
        _animator.SetTrigger("Walk");
        while (currentTime <= allTime)
        {
            transform.position += transform.forward * -1 * _walkSpeed * Time.deltaTime;
            yield return new WaitForEndOfFrame();
            currentTime += Time.deltaTime;
        }
    }
}
