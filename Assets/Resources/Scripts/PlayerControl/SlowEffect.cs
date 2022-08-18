using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowEffect : MonoBehaviour
{
    public static SlowEffect singleton;
    [SerializeField] float _duration;
    [SerializeField] float _slowTime;

    private void Awake()
    {
        if (singleton == null) singleton = this;
    }
    public void Play()
    {
        StopAllCoroutines();
        StartCoroutine(EffectProcess());
    }
    IEnumerator EffectProcess()
    {
        Time.timeScale = _slowTime;
        yield return new WaitForSeconds(_duration);
        Time.timeScale = 1;
    }
    private void OnEnable()
    {
        ComboSystem.onStartCombo += Play;
    }
    private void OnDisable()
    {
        ComboSystem.onStartCombo -= Play;
    }
}
