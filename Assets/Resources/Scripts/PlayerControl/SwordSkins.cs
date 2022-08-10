using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSkins : MonoBehaviour
{
    [SerializeField] GameObject[] _skins;

    private void Start()
    {
        int childCount = transform.childCount;

        _skins = new GameObject[childCount];

        for (int i = 0; i < _skins.Length; i++)
        {
            _skins[i] = transform.GetChild(i).gameObject;
            _skins[i].SetActive(false);
        }

        if(PlayerPrefs.HasKey("ChoicedSkinID")) _skins[PlayerPrefs.GetInt("ChoicedSkinID")].SetActive(true);
        else _skins[0].SetActive(true);
    }

    void OnPickOtherSkin(int id)
    {
        _skins[PlayerPrefs.GetInt("ChoicedSkinID")].SetActive(false);
        PlayerPrefs.SetInt("ChoicedSkinID", id);
        _skins[PlayerPrefs.GetInt("ChoicedSkinID")].SetActive(true);
    }

    private void OnEnable()
    {
        SkinUI.onChoiceSkin += OnPickOtherSkin;
    }
    private void OnDisable()
    {
        SkinUI.onChoiceSkin -= OnPickOtherSkin;
    }
}
