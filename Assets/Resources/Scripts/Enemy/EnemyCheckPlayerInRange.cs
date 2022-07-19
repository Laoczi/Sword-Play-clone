using System;
using UnityEngine;

public class EnemyCheckPlayerInRange : MonoBehaviour
{
    public event Action onPlayerEnter;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) onPlayerEnter?.Invoke();
    }
}
