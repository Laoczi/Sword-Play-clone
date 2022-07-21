using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BzKovSoft.ObjectSlicer;

public class EnemyRagdoll : MonoBehaviour
{
    [SerializeField] float _pushVelocity;
    [SerializeField] Rigidbody _mainRigidBody;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Sword"))
        {
            _mainRigidBody.isKinematic = false;
            _mainRigidBody.AddForce(Vector3.forward * _pushVelocity, ForceMode.Impulse);

            IBzSliceable sliceTarget = GetComponent<IBzSliceable>();
            Plane slicePlane = new Plane(other.transform.up, other.transform.position);
            sliceTarget.Slice(slicePlane, null);
        }
    }
    void OnEndSlice(BzSliceTryResult result)
    {
        result.outObjectNeg.GetComponent<EnemyRagdoll>().AfterSlice();
        result.outObjectPos.GetComponent<EnemyRagdoll>().AfterSlice();
    }
    public void AfterSlice()
    {
        _mainRigidBody.isKinematic = false;
        _mainRigidBody.AddForce(Vector3.forward * _pushVelocity, ForceMode.Impulse);
    }
}
