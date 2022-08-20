using System;
using UnityEngine;

public class CollectableKey : MonoBehaviour
{
    public static event Action onCutKey;

    GameObject _keyUp;
    GameObject _keyDown;
    Rigidbody _rbKeyUp;
    Rigidbody _rbKeyDown;
    Collider _collider;
    Animator _animator;

    [SerializeField] float _pushForce;
    private void OnEnable()
    {
        _keyUp = transform.GetChild(1).gameObject;
        _keyDown = transform.GetChild(2).gameObject;

        _rbKeyUp = _keyUp.GetComponent<Rigidbody>();
        _rbKeyDown = _keyDown.GetComponent<Rigidbody>();

        _collider = GetComponent<Collider>();
        _animator = GetComponent<Animator>();
    }
    public void Cut()
    {
        _collider.enabled = false;
        _animator.enabled = false;

        _rbKeyUp.velocity = Vector3.zero;
        _rbKeyDown.velocity = Vector3.zero;

        _rbKeyUp.useGravity = true;
        _rbKeyDown.useGravity = true;

        _rbKeyUp.AddForce(Vector3.left * _pushForce, ForceMode.Impulse);
        _rbKeyDown.AddForce(Vector3.right * _pushForce, ForceMode.Impulse);

        onCutKey?.Invoke();

        Destroy(this.gameObject, 5f);
    }
}
