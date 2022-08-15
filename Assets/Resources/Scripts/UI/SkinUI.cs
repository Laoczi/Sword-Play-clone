using System;
using UnityEngine;
using UnityEngine.UI;

public class SkinUI : MonoBehaviour
{
    public static event Action<int> onChoiceSkin;

    GameObject _openIcon;
    GameObject _closeIcon;

    GameObject _frame;

    public bool isOpen { get; private set; }
    [field: SerializeField] public int id { get; private set; }
    public void Init()
    {
        _openIcon = transform.GetChild(1).gameObject;
        _closeIcon = transform.GetChild(0).gameObject;

        _frame = _openIcon.transform.GetChild(1).gameObject;

        _frame.SetActive(false);

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
    private void OnEnable()
    {
        SkinUI.onChoiceSkin += OnChoice;
    }
    private void OnDisable()
    {
        SkinUI.onChoiceSkin -= OnChoice;
    }
    void OnChoice(int id)
    {
        _frame.SetActive(this.id == id);
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
