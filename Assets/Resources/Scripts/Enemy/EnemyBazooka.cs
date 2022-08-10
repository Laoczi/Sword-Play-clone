using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBazooka : Enemy
{
    [SerializeField] GameObject _bullet;
    [SerializeField] int _bulletsCount;
    [SerializeField] float _bulletSpawnDelay;
    [SerializeField] Transform _bulletSpawnPoint;
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
        StartCoroutine(AttackProcess());
    }
    IEnumerator AttackProcess()
    {
        int count = 0;

        while(count < _bulletsCount)
        {
            _animator.SetTrigger("Attack");
            yield return new WaitForSeconds(_bulletSpawnDelay);
            GameObject bullet = Instantiate(_bullet);
            bullet.transform.position = _bulletSpawnPoint.position;
            bullet.transform.forward = _bulletSpawnPoint.forward;

            count++;
        }
    }
}
