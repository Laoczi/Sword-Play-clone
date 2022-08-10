using System;
using UnityEngine;
using UnityEngine.UI;

public class SkinUI : MonoBehaviour
{
    public static event Action<int> onChoiceSkin;

    GameObject _openIcon;
    GameObject _closeIcon;
    public bool isOpen { get; private set; }
    [field: SerializeField] public int id { get; private set; }
    public void Init()
    {
        _openIcon = transform.GetChild(1).gameObject;
        _closeIcon = transform.GetChild(0).gameObject;

        //GetComponent<Button>().onClick.AddListener(OnClick);
    }
    public void Open()
    {
        isOpen = true;
        _openIcon.SetActive(true);
        _closeIcon.SetActive(false);
    }
    public void Close()
    {
        isOpen = false;
        _openIcon.SetActive(false);
        _closeIcon.SetActive(true);
    }

    public void OnClick()
    {
        if (isOpen)
        {
            Debug.Log("choice " + id);
            onChoiceSkin?.Invoke(id);
        }
    }
}
