using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinsShop : MonoBehaviour
{
    [SerializeField] float _unlockRandomPrice;
    [SerializeField] GameObject _lockedSkinsParent;
    [SerializeField] GameObject _unlockedSkinsParent;

    GameObject[] _lockedSkins;
    GameObject[] _unlockedSkins;

    private void Start()
    {
        _lockedSkins = _lockedSkinsParent.GetComponentsInChildren<GameObject>();
        _unlockedSkins = _unlockedSkinsParent.GetComponentsInChildren<GameObject>();

        for (int i = 0; i < _lockedSkins.Length; i++)
        {
            if (PlayerPrefs.HasKey("OpenSkin " + i))
            {
                _lockedSkins[i].SetActive(false);
                _unlockedSkins[i].SetActive(true);
            }
            else
            {
                _lockedSkins[i].SetActive(true);
                _unlockedSkins[i].SetActive(false);
            }
        }
    }
    public void OpenRandom()
    {
        List<int> openedSkinsID = new List<int>();

        for (int i = 0; i < _unlockedSkins.Length; i++)
        {
            if (_unlockedSkins[i].activeInHierarchy) openedSkinsID.Add(i);
        }

        if(openedSkinsID.Count == 0)
        {
            Debug.Log("Все скины куплены");
            return;
        }

        if (Wallet.singleton.Get(_unlockRandomPrice) == false) return;

        int randomSkin = Random.Range(0, openedSkinsID.Count);

        _lockedSkins[openedSkinsID[randomSkin]].SetActive(false);
        _unlockedSkins[openedSkinsID[randomSkin]].SetActive(true);
        PlayerPrefs.SetInt("OpenSkin " + openedSkinsID[randomSkin], 1);
    }
}
