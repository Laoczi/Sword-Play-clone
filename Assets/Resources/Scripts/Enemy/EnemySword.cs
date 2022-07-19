using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BzKovSoft.ObjectSlicer;

public class EnemySword : Enemy
{
    Vector3 _swordDirection;
    protected override void DealDamage(Transform swordTransform)
    {
        _swordDirection = swordTransform.right;

        _animator.StopPlayback();
        _animator.enabled = false;
        _triggerRange.enabled = false;
        GetComponent<Collider>().enabled = false;
        //slice mesh
        IBzSliceable sliceTarget = _sliceTarget.GetComponent<IBzSliceable>();
        Plane slicePlane = new Plane(swordTransform.up,swordTransform.position);
        sliceTarget.Slice(slicePlane, OnEndSlice);
    }
    void OnEndSlice(BzSliceTryResult result)
    {
        result.outObjectNeg.GetComponent<EnemyRagdoll>().AfterSlice(_swordDirection);
        result.outObjectPos.GetComponent<EnemyRagdoll>().AfterSlice(_swordDirection);
    }

    protected override void OnPlayerEnterRangeZone()
    {
        _triggerRangePlayerCheck.enabled = false;
        _animator.SetTrigger("Attack");
    }
}
