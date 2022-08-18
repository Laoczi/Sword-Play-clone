using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowEffect : MonoBehaviour
{
    public static SlowEffect singleton;
    [SerializeField] float _duration;
    [SerializeField] float _slowTime;

    Coroutine process;

    private void Awake()
    {
        if (singleton == null) singleton = this;
    }
    public void Play()
    {
        if (process == null) process = StartCoroutine(EffectProcess());
    }
    IEnumerator EffectProcess()
    {
        Time.timeScale = _slowTime;
        yield return new WaitForSeconds(_duration);
        Time.timeScale = 1;
        process = null;
    }
}
