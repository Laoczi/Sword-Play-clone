using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPistol : Enemy
{
    [SerializeField] Bullet _bullet;
    [SerializeField] Transform _bulletSpawnPoint;
    protected override void DealDamage(Transform swordTransform)
    {
        _animator.StopPlayback();
        _animator.enabled = false;

        Collider[] ourColliders = GetComponents<Collider>();
        foreach (Collider collider in ourColliders)
        {
            collider.enabled = false;
        }

        CallOnDeathEvent();
        _deathEffect.Play();

        _slicer.Cut(swordTransform);
    }
    protected override void OnPlayerEnterRangeZone()
    {
        Bullet bullet = Instantiate(_bullet);
        bullet.transform.position = _bulletSpawnPoint.position;
        bullet.transform.forward = _bulletSpawnPoint.forward;
    }
}
