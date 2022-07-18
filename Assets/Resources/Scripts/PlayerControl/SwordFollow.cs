using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordFollow : MonoBehaviour
{
    [SerializeField] Transform _target;
    [SerializeField] float _smoothTime;
    [SerializeField] float _maxSpeed;

    Vector3 _velocity;

    private void Update()
    {
        //follow move
        transform.position = Vector3.SmoothDamp(transform.position, _target.position, ref _velocity, _smoothTime, _maxSpeed);
        //follow rotation
        Vector3 direction = (transform.position + _velocity) - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.localRotation = Quaternion.Euler(_target.eulerAngles.x, _target.eulerAngles.y, angle);
    }
}
