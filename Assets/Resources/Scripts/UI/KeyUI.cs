using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BoxType
{
    reward,
    skin,
}

public class KeyUI : MonoBehaviour
{
    public static KeyUI singleton;

    [SerializeField] GameObject _panel;
    [SerializeField] GameObject[] _topPrizeIcons;
    [SerializeField] GameObject[] _currentKeysIcons;

    public int topPrizeSkinId { get; private set; }
    public int reward { get { return 25 * Random.Range(1, 5); } }

    bool _skinIsOpen;

    public BoxType boxType 
    { 
        get 
        {
            if(Random.Range(0, 2) > 0)
            {
                return BoxType.reward;
            }
            else
            {
                if (_skinIsOpen || topPrizeSkinId == 0)
                {
                    return BoxType.reward;
                }
                {
                    _skinIsOpen = true;
                    return BoxType.skin;
                }
            }
        } 
    }
    public int openedBoxesCount { get; private set; }

    private void Awake()
    {
        singleton = this;
    }
    public void ShowMenu()
    {
        int keyCount = PlayerPrefs.GetInt("KeyCount");

        if (keyCount < 3) return;

        _panel.SetActive(true);
        SelectTopPrize();
        UpdateKeyIcons();
    }
    public void Hide()
    {
        PlayerPrefs.SetInt("KeyCount", 0);
        LevelProgress.singleton.GoToNextLevel();
    }
    void OnOpenChest()
    {
        openedBoxesCount++;
    }
    void UpdateKeyIcons()
    {
        int keyCount = PlayerPrefs.GetInt("KeyCount");

        for (int i = 0; i < 3; i++)
        {
            _currentKeysIcons[i].SetActive(keyCount - 1 >= i);
        }
        for (int i = 3; i < _currentKeysIcons.Length; i++)
        {
            _currentKeysIcons[i].SetActive(keyCount - 1 < i - 3);
        }
    }
    private void OnEnable()
    {
        CameraMovement.onReachedFinish += ShowMenu;
        KeyBox.onOpenChest += UpdateKeyIcons;
        KeyBox.onOpenChest += OnOpenChest;
    }
    private void OnDisable()
    {
        CameraMovement.onReachedFinish -= ShowMenu;
        KeyBox.onOpenChest -= UpdateKeyIcons;
        KeyBox.onOpenChest -= OnOpenChest;
    }
    void SelectTopPrize()
    {
        List<int> openedSkinsIds = new List<int>();

        for (int i = 0; i < _topPrizeIcons.Length; i++)
        {
            if (PlayerPrefs.HasKey("OpenSkin " + i) == false) openedSkinsIds.Add(i);
        }

        if (openedSkinsIds.Count == 0) return;

        topPrizeSkinId = openedSkinsIds[Random.Range(0, openedSkinsIds.Count)];

        for (int i = 0; i < _topPrizeIcons.Length; i++)
        {
            _topPrizeIcons[i].SetActive(topPrizeSkinId == i);
        }
    }
}
