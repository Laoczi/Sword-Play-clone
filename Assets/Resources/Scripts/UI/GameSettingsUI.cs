using System;
using UnityEngine;

public enum SettingType
{
    sound,
    music,
    vibro,
}
public class GameSettingsUI : MonoBehaviour
{
    public static event Action<SettingType> onChangeSetting;

    [SerializeField] GameObject _menu;
    [Header("Button Icons")]
    [SerializeField] SettingButtonUI _sound;
    [SerializeField] SettingButtonUI _music;
    [SerializeField] SettingButtonUI _vibro;

    private void Start()
    {
        _menu.SetActive(false);

        _sound.SetState(GameSettings.sound);
        _music.SetState(GameSettings.music);
        _vibro.SetState(GameSettings.vibro);
    }

    public void Show()
    {
        _menu.SetActive(true);
    }
    public void Hide()
    {
        _menu.SetActive(false);
    }

    public void ChangeSound()
    {
        onChangeSetting?.Invoke(SettingType.sound);
        _sound.SetState(GameSettings.sound);
    }
    public void ChangeMusic()
    {
        onChangeSetting?.Invoke(SettingType.music);
        _music.SetState(GameSettings.music);
    }
    public void ChangeVibro()
    {
        onChangeSetting?.Invoke(SettingType.vibro);
        _vibro.SetState(GameSettings.vibro);
    }
}
[System.Serializable]
public class SettingButtonUI
{
    [SerializeField] GameObject _onIcon;
    [SerializeField] GameObject _offIcon;

    public void SetState(bool state)
    {
        if (state)
        {
            _onIcon.SetActive(true);
            _offIcon.SetActive(false);
        }
        else
        {
            _onIcon.SetActive(false);
            _offIcon.SetActive(true);
        }
    }
}
