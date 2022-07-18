using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordControl : MonoBehaviour
{
    [SerializeField] Transform _swordPivot;
    [Header("Turn settings")]
    [SerializeField] float _turnForce;
    [SerializeField] float _divisorBySwipeDelta;
    [Header("Angle settings")]
    [SerializeField] float _maxXAngle;
    [SerializeField] float _minXAngle;
    [SerializeField] float _maxYAngle;
    [SerializeField] float _minYAngle;

    Vector2 _rotationBeforeSwipe;
    Vector2 _tapPosition;
    Vector2 _swipeDelta;

    private void Start()
    {
        _rotationBeforeSwipe = Vector2.zero;
        _tapPosition = Vector2.zero;
        _swipeDelta = Vector2.zero;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _tapPosition = Input.mousePosition;
            _rotationBeforeSwipe = _swordPivot.localEulerAngles;
        }

        if (Input.GetMouseButton(0))
        {
            //getting swipe delta
            _swipeDelta = ((Vector2)Input.mousePosition - _tapPosition) / _divisorBySwipeDelta;
            //prepare rotation
            Vector2 nextRotation;
            nextRotation.x = _swipeDelta.y * -1 * _turnForce + _rotationBeforeSwipe.x;
            nextRotation.y = _swipeDelta.x * _turnForce + _rotationBeforeSwipe.y;
            _swordPivot.localRotation = Quaternion.Euler(nextRotation.x, nextRotation.y, 0);
            //clamp angle
            Vector2 currentRotation = _swordPivot.localEulerAngles;

            currentRotation.x = (currentRotation.x > 180) ? currentRotation.x - 360 : currentRotation.x;
            currentRotation.y = (currentRotation.y > 180) ? currentRotation.y - 360 : currentRotation.y;

            currentRotation.x = Mathf.Clamp(currentRotation.x, _minXAngle, _maxXAngle);
            currentRotation.y = Mathf.Clamp(currentRotation.y, _minYAngle, _maxYAngle);
            //set rotation
            _swordPivot.localRotation = Quaternion.Euler(currentRotation.x, currentRotation.y, 0);
        }
    }
}
