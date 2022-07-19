using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRagdoll : MonoBehaviour
{
    [SerializeField] float _pushVelocity;
    [SerializeField] Rigidbody _mainRigidBody;

    public void AfterSlice(Vector3 pushDirection)
    {
        _mainRigidBody.isKinematic = false;
        _mainRigidBody.AddForce(pushDirection * _pushVelocity, ForceMode.Impulse);
    }
}
