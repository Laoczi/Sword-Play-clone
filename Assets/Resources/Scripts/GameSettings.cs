using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour
{
    public static GameSettings singleton;

    public bool sound { get; private set; }
    public bool music { get; private set; }
    public bool vibro { get; private set; }

    private void Awake()
    {
        if (singleton == null) singleton = this;
        else Destroy(this.gameObject);

        sound = true;
        music = true;
        vibro = true;

        if (PlayerPrefs.HasKey("sound")) sound = PlayerPrefs.GetInt("sound") == 1;
        if (PlayerPrefs.HasKey("music")) music = PlayerPrefs.GetInt("music") == 1;
        if (PlayerPrefs.HasKey("vibro")) vibro = PlayerPrefs.GetInt("vibro") == 1;
    }


    void OnSettingsChanged(SettingType type)
    {
        switch (type)
        {
            case SettingType.sound:
                sound = !sound;
                PlayerPrefs.SetInt("sound", sound == true ? 1 : 0);
                break;
            case SettingType.music:
                music = !music;
                PlayerPrefs.SetInt("music", music == true ? 1 : 0);
                break;
            case SettingType.vibro:
                vibro = !vibro;
                PlayerPrefs.SetInt("vibro", vibro == true ? 1 : 0);
                break;
        }
    }
    private void OnEnable()
    {
        GameSettingsUI.onChangeSetting += OnSettingsChanged;
    }
    private void OnDisable()
    {
        GameSettingsUI.onChangeSetting += OnSettingsChanged;
    }
}
