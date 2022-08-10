using System;
using UnityEngine;
using DynamicMeshCutter;

public class BazookaBullet : MonoBehaviour
{
    public static event Action onDead;

    [SerializeField] float _flySpeed;
    [SerializeField] float _divisorByGravity;
    [SerializeField] float _lifeTime;
    [SerializeField] PlaneBehaviour _slicer;

    bool _isSliced;
    Vector3 _startPosition;
    float time = 0;

    private void OnEnable()
    {
        Destroy(this.gameObject, _lifeTime);

        _startPosition = transform.position;
        _isSliced = false;
    }
    private void Update()
    {
        //transform.position += (transform.forward * _flySpeed + Vector3.up * _gravityCurve * Time.deltaTime) * Time.deltaTime;

        time += Time.deltaTime;

        transform.position = _startPosition + (transform.forward.normalized * _flySpeed) * time + ((Physics.gravity * time * time / 2.5f) / _divisorByGravity);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (_isSliced) return;
        if (other.CompareTag("Sword"))
        {
            _isSliced = true;
            tag = "Untagged";
            onDead?.Invoke();
            _slicer.Cut(other.transform);
        }
    }
}
