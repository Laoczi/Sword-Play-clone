using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class StartTutor : MonoBehaviour, IPointerDownHandler
{
    public static event Action onClick;
    bool _isAlreadyClick;

    private void Start()
    {
        _isAlreadyClick = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_isAlreadyClick) return;
        Debug.Log("check tutor");
        _isAlreadyClick = true;
        onClick?.Invoke();
        this.gameObject.SetActive(false);
    }
}
