using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinsShop : MonoBehaviour
{
    [SerializeField] GameObject _menu;
    [SerializeField] float _unlockRandomPrice;
    [SerializeField] SkinUI[] _skins;
    [SerializeField] GameObject[] _skinIcons;

    int _previousSkinIconID;


    private void Start()
    {
        if(PlayerPrefs.HasKey("OpenSkin 0") == false) PlayerPrefs.SetInt("OpenSkin 0", 1);

        if(PlayerPrefs.HasKey("ChoicedSkinID")) OnChoiceSkin(PlayerPrefs.GetInt("ChoicedSkinID"));
        else OnChoiceSkin(0);

        for (int i = 0; i < _skins.Length; i++)
        {
            _skins[i].Init();

            if (PlayerPrefs.HasKey("OpenSkin " + i)) _skins[i].Open();
            else _skins[i].Close();
        }

        foreach (GameObject icon in _skinIcons)
        {
            icon.SetActive(false);
        }

        if (PlayerPrefs.HasKey("ChoicedSkinID"))
        {
            _skinIcons[PlayerPrefs.GetInt("ChoicedSkinID")].SetActive(true);
            _previousSkinIconID = PlayerPrefs.GetInt("ChoicedSkinID");
        }
        else
        {
            _skinIcons[0].SetActive(true);
            _previousSkinIconID = 0;
        }
    }
    void OnChoiceSkin(int id)
    {
        _skinIcons[_previousSkinIconID].SetActive(false);
        _previousSkinIconID = id;
        _skinIcons[id].SetActive(true);
    }
    private void OnEnable()
    {
        SkinUI.onChoiceSkin += OnChoiceSkin;
    }
    private void OnDisable()
    {
        SkinUI.onChoiceSkin -= OnChoiceSkin;
    }
    public void OpenRandom()
    {
        List<int> openedSkinsID = new List<int>();

        for (int i = 0; i < _skins.Length; i++)
        {
            if (_skins[i].isOpen == false) openedSkinsID.Add(i);
        }

        if(openedSkinsID.Count == 0)
        {
            Debug.Log("Все скины куплены");
            return;
        }

        if (Wallet.singleton.Get(_unlockRandomPrice) == false) return;

        int randomSkin = Random.Range(0, openedSkinsID.Count);

        _skins[openedSkinsID[randomSkin]].Open();
        Debug.Log("купили скин " + openedSkinsID[randomSkin]);

        PlayerPrefs.SetInt("OpenSkin " + openedSkinsID[randomSkin], 1);
    }
    public void HideMenu()
    {
        _menu.SetActive(false);
    }
    public void ShowMenu()
    {
        _menu.SetActive(true);
    }
}