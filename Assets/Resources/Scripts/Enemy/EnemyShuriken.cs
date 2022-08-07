using UnityEngine;
using System.Collections;

public class EnemyShuriken : Enemy
{
    [SerializeField] float _delayBeforeSpawnShuriken;
    [SerializeField] Shuriken _shuriken;
    [SerializeField] Transform _shurikenSpawnPoint;
    protected override void DealDamage(Transform swordTransform)
    {
        _animator.StopPlayback();
        _animator.enabled = false;

        Collider[] ourColliders = GetComponents<Collider>();
        foreach (Collider collider in ourColliders)
        {
            collider.enabled = false;
        }

        CallOnDeathEvent();
        _deathEffect.Play();

        _slicer.Cut(swordTransform);
    }
    protected override void OnPlayerEnterRangeZone()
    {
        StartCoroutine(AttackProcess());
    }
    IEnumerator AttackProcess()
    {
        _animator.SetTrigger("Attack");
        yield return new WaitForSeconds(_delayBeforeSpawnShuriken);

        Shuriken shuriken = Instantiate(_shuriken);
        shuriken.transform.position = _shurikenSpawnPoint.position;
        shuriken.transform.forward = _shurikenSpawnPoint.forward;
    }
}
