using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shuriken : MonoBehaviour
{
    [SerializeField] float _baseMoveSpeed;
    [SerializeField] float _deflectedMoveSpeed;
    float _currentMoveSpeed;
    

    Vector3 direction;

    bool _isReflected;

    private void OnEnable()
    {
        _currentMoveSpeed = _baseMoveSpeed;
        direction = transform.forward;
    }

    private void Update()
    {
        transform.position += direction * _currentMoveSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Sword") && _isReflected == false)
        {
            Debug.Log("попал по сюрикену");
            _currentMoveSpeed = _deflectedMoveSpeed;
            direction *= -1;
            this.tag = "Sword";
            _isReflected = true;
        }
        if(other.CompareTag("Enemy") && _isReflected)
        {
            Destroy(this.gameObject);
        }
    }
}
