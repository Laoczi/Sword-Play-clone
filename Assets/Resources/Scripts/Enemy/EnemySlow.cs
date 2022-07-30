using System;
using UnityEngine;

public class EnemySlow : MonoBehaviour
{
    public event Action onPlayerEnter;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GetComponent<Collider>().enabled = false;
            onPlayerEnter?.Invoke();
        }
    }
}
