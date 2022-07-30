using System;
using UnityEngine;

public class EnemyRange : MonoBehaviour
{
    public event Action onPlayerEnter;
    [field: SerializeField] public bool needSlowMove { get; private set; }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            onPlayerEnter?.Invoke();
            GetComponent<Collider>().enabled = false;
        }
    }
}
