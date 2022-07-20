using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BzKovSoft.ObjectSlicer;

public class EnemyPistol : Enemy
{
    [SerializeField] Bullet _bullet;
    [SerializeField] Transform _bulletSpawnPoint;
    protected override void DealDamage(Transform swordTransform)
    {
        _animator.StopPlayback();
        _animator.enabled = false;
        _triggerRange.enabled = false;
        GetComponent<Collider>().enabled = false;
        //slice mesh
        IBzSliceable sliceTarget = _sliceTarget.GetComponent<IBzSliceable>();
        Plane slicePlane = new Plane(swordTransform.up, swordTransform.position);
        sliceTarget.Slice(slicePlane, OnEndSlice);

        CallOnDeathEvent();
    }
    void OnEndSlice(BzSliceTryResult result)
    {
        result.outObjectNeg.GetComponent<EnemyRagdoll>().AfterSlice();
        result.outObjectPos.GetComponent<EnemyRagdoll>().AfterSlice();
    }

    protected override void OnPlayerEnterRangeZone()
    {
        Bullet bullet = Instantiate(_bullet);
        bullet.transform.position = _bulletSpawnPoint.position;
        bullet.transform.forward = _bulletSpawnPoint.forward;
    }
}
