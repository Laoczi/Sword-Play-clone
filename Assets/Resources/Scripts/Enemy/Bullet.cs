using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BzKovSoft.ObjectSlicer;

public class Bullet : MonoBehaviour
{
    [SerializeField] Rigidbody _rigidBody;
    [SerializeField] float _speed;
    bool _isSliced;
    private void Awake()
    {
        _isSliced = false;
    }
    void Update()
    {
        if (_isSliced) return;
        transform.position += transform.forward * Time.deltaTime;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Sword"))
        {
            _isSliced = true;
            tag = "Untagged";
            _rigidBody.isKinematic = false;

            IBzSliceable sliceTarget = GetComponent<IBzSliceable>();
            Plane slicePlane = new Plane(other.transform.up, other.transform.position);
            sliceTarget.Slice(slicePlane, null);
        }
    }
}
