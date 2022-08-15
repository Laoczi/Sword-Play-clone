using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sounds : MonoBehaviour
{
    public static Sounds singleton;
    [SerializeField, Range(0, 1)] float _volume;
    [SerializeField] AudioClip[] sounds;
    private void Awake()
    {
        if (singleton == null) singleton = this;
        else Destroy(this.gameObject);

        DontDestroyOnLoad(this.gameObject);
    }
    void PlaySwordAttack()
    {
        if(GameSettings.sound) SpawnAudioClip(sounds[0]);
    }
    void PlayEnemyAttack()
    {
        if (GameSettings.sound) SpawnAudioClip(sounds[1]);
    }
    void PlayEndLevel()
    {
        if (GameSettings.sound) SpawnAudioClip(sounds[2]);
    }
    void PlaySkinkUnlock(int id)
    {
        if (GameSettings.sound) SpawnAudioClip(sounds[3]);
    }
    void SpawnAudioClip(AudioClip clip)
    {
        GameObject audio = new GameObject();
        audio.transform.SetParent(Camera.main.transform);
        audio.transform.localPosition = Vector3.zero;
        AudioSource source = audio.AddComponent<AudioSource>();
        source.clip = clip;
        source.volume = _volume;
        source.Play();
        Destroy(audio, clip.length);
    }
    private void OnEnable()
    {
        CameraPathMoveControl.onReachedFinish += PlayEndLevel;
        SkinProgress.onOpenSkin += PlaySkinkUnlock;
        EnemyPistol.onShoot += PlayEnemyAttack;
        SwordFollow.onTouchEnemy += PlaySwordAttack;
    }
    private void OnDisable()
    {
        CameraPathMoveControl.onReachedFinish -= PlayEndLevel;
        SkinProgress.onOpenSkin -= PlaySkinkUnlock;
        EnemyPistol.onShoot -= PlayEnemyAttack;
        SwordFollow.onTouchEnemy -= PlaySwordAttack;
    }
}
