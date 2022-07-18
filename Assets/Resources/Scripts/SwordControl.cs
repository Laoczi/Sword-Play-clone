using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordControl : MonoBehaviour
{
    [SerializeField] Transform _swordPivot;
    [SerializeField] float _turnForce;
    [SerializeField] float _divisorBySwipeDelta;
    [SerializeField] float _maxXAngle;
    [SerializeField] float _minXAngle;
    [SerializeField] float _maxYAngle;
    [SerializeField] float _minYAngle;

    Vector2 _rotationBeforeSwipe;
    Vector2 _tapPosition;
    Vector2 _swipeDelta;
    bool _isSwiping;

    private void Start()
    {
        _rotationBeforeSwipe = Vector2.zero;
        _tapPosition = Vector2.zero;
        _swipeDelta = Vector2.zero;
        _isSwiping = false;
    }

    private void Update()
    {
        if(_isSwiping == false)
        {
            if (Input.GetMouseButtonDown(0))
            {
                _isSwiping = true;
                _tapPosition = Input.mousePosition;
                _rotationBeforeSwipe = _swordPivot.rotation.eulerAngles;
            }
        }
        else
        {
            //getting swipe delta
            _swipeDelta = ((Vector2)Input.mousePosition - _tapPosition) / _divisorBySwipeDelta;
            //set rotation
            Vector2 nextRotation;
            nextRotation.x = _swipeDelta.y * -1 * _turnForce + _rotationBeforeSwipe.x;
            nextRotation.y = _swipeDelta.x * _turnForce + _rotationBeforeSwipe.y;
            _swordPivot.rotation = Quaternion.Euler(nextRotation.x, nextRotation.y, 0);
            //clamp angle
            Vector2 currentRotation = transform.rotation.eulerAngles;
            currentRotation.x = Mathf.Clamp(currentRotation.x, _minXAngle, _maxXAngle);
            currentRotation.y = Mathf.Clamp(currentRotation.y, _minYAngle, _maxYAngle);
            transform.rotation = Quaternion.Euler(currentRotation.x, currentRotation.y, 0);

            if (Input.GetMouseButtonUp(0)) ResetSwipe();
        }
    }
    void ResetSwipe()
    {
        _rotationBeforeSwipe = Vector2.zero;
        _tapPosition = Vector2.zero;
        _swipeDelta = Vector2.zero;
        _isSwiping = false;
    }
}
