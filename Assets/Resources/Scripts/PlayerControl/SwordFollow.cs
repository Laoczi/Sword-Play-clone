using System;
using UnityEngine;
public class SwordFollow : MonoBehaviour
{
    public static event Action onTouchEnemy;

    [SerializeField] Transform _positionPivot;
    [SerializeField] Transform _rotationPivot;
    [SerializeField] float _smoothTime;
    [SerializeField] float _maxSpeed;

    Vector3 _velocity;

    private void Update()
    {
        //follow move
        transform.position = Vector3.SmoothDamp(transform.position, _positionPivot.position, ref _velocity, _smoothTime, _maxSpeed);
        //follow rotation
        if(Vector3.Distance(transform.position, _positionPivot.position) > 0.01f)
        {
            Vector3 direction = (transform.position + _velocity) - transform.position;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            transform.localRotation = Quaternion.Euler(_rotationPivot.localEulerAngles.x, _rotationPivot.localEulerAngles.y, angle);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy")) onTouchEnemy?.Invoke();
    }
}
