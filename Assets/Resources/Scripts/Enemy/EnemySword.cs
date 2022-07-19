using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BzKovSoft.ObjectSlicer;

public class EnemySword : Enemy
{
    protected override void DealDamage(Transform swordTransform)
    {
        _animator.StopPlayback();
        _animator.enabled = false;
        _triggerRange.enabled = false;
        GetComponent<Collider>().enabled = false;
        //slice mesh
        IBzSliceable sliceTarget = _sliceTarget.GetComponent<IBzSliceable>();
        Plane slicePlane = new Plane(swordTransform.up,swordTransform.position);
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
        _triggerRangePlayerCheck.enabled = false;
        _animator.SetTrigger("Attack");
    }
}
