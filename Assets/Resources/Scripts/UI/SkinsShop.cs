using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinsShop : MonoBehaviour
{
    [SerializeField] GameObject _menu;
    [SerializeField] float _unlockRandomPrice;
    [SerializeField] SkinUI[] _skins;


    private void Start()
    {
        for (int i = 0; i < _skins.Length; i++)
        {
            if (PlayerPrefs.HasKey("OpenSkin " + i)) _skins[i].Open();
            else _skins[i].Close();
        }
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