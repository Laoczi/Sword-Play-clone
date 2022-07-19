using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRagdoll : MonoBehaviour
{
    [SerializeField] float _pushVelocity;
    [SerializeField] Rigidbody _mainRigidBody;

    public void AfterSlice()
    {
        _mainRigidBody.isKinematic = false;
        _mainRigidBody.AddForce(Vector3.forward * _pushVelocity, ForceMode.Impulse);
    }
}
