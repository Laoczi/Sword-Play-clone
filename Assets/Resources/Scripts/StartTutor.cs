using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class StartTutor : MonoBehaviour, IPointerDownHandler
{
    public static event Action onClick;
    bool _isAlreadyClick;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_isAlreadyClick) return;
        _isAlreadyClick = true;
        onClick?.Invoke();
        this.gameObject.SetActive(false);
    }
}
