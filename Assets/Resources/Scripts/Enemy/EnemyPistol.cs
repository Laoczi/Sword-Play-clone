using System;
using UnityEngine;

public class EnemyPistol : Enemy
{
    public static event Action onShoot;

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
        Bullet bullet = Instantiate(_bullet, _bulletSpawnPoint.position,Quaternion.LookRotation(_bulletSpawnPoint.forward));
        onShoot?.Invoke();
    }
}
