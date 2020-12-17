using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Rotator : MonoBehaviour
{

    [SerializeField]
    int _speed = 12;
    [SerializeField]
    float _friction = 0.5f;
    [SerializeField]
    float _lerpSpeed = 1.5f;
    [SerializeField]
    float _lerpZoomSpeed = 1.5f;
    [SerializeField]
    float _zoomDelta = 0.5f;

    public static float _minZoom = -4;
    public static float _maxZoom = 22;

    bool _executeRotation = false;
    bool _executeScale = false;
    bool _firstClick = false;
    bool _firstScroll = false;
    bool _allowDrag = false;
    bool _pointerDown = false;
    bool _allowInertialDrag = false;

    float _degNumber = 0;
    float _positionZ = 5.8f;
    float _xDeg = 205f;
    float _yDeg;

    Vector3 _toPosition;
    Vector2 firstpoint, secondpoint;


    public void OnEnable()
    {
        DragControlls.OnDragAction += OnDragAction;
        DragControlls.OnPointerAction += OnPointerAction;
    }


    private void OnDisable()
    {
        DragControlls.OnDragAction -= OnDragAction;
        DragControlls.OnPointerAction -= OnPointerAction;
    }


    public void OnDragAction(bool executeRotation)
    {
        _executeRotation = executeRotation;
    }


    public void OnPointerAction(bool allow)
    {
        _executeScale = allow;
    }


    private void Start()
    {
        if (UI.mobileSupport)
        {
            gameObject.transform.position = new Vector3(-5.7f, -2.2f, 44);
            Camera.main.transform.eulerAngles = new Vector3(3, -4, 0);
            _maxZoom = 22;
            _minZoom = 88;
        }
    }


    void Update()
    {
        RotateModel(Time.deltaTime);
        ZoomModel(Time.deltaTime, Input.mouseScrollDelta.y);
    }


    private void RotateModel(float deltaTime)
    {
        if (UI.mobileSupport)
        {
            //Count touches
            if (Input.touchCount == 1 || _allowInertialDrag)
            {
                if (Input.touchCount == 1 && _executeRotation)
                {
                    if (Input.GetTouch(0).phase == TouchPhase.Began)
                    {
                        firstpoint = Input.GetTouch(0).position;
                    }
                    //Move finger
                    if (Input.GetTouch(0).phase == TouchPhase.Moved)
                    {
                        secondpoint = Input.GetTouch(0).position;
                        _xDeg = -(secondpoint.x - firstpoint.x) * _speed * _friction * 0.0025f;
                        _yDeg = (secondpoint.y - firstpoint.y) * _speed * _friction * 0.005f;
                        _firstClick = true;
                    }
                }
            }
        }
        else
        {
            if (Input.GetMouseButton(0) || _allowInertialDrag)
            {
                if (Input.GetMouseButton(0) && _executeRotation)
                {
                    _xDeg = -Mathf.Clamp(Input.GetAxis("Mouse X"), -5, 5) * _speed * 0.3f * _friction;
                    _yDeg = Mathf.Clamp(Input.GetAxis("Mouse Y"), -5, 5) * _speed * 0.3f * _friction;
                    _firstClick = true;
                }
            }
        }

        if (_firstClick)
        {
            _xDeg *= 0.95f;

            transform.Rotate(Vector3.forward, _xDeg, Space.Self);

            _yDeg *= 0.95f;
            _degNumber += _yDeg;

            if (_degNumber > -70 && _degNumber < 11)
            {
                transform.Rotate(Vector3.right, _yDeg, Space.World);
            }
            else
            {
                float tempDegNumber = _degNumber;                
                _degNumber = Mathf.Clamp(_degNumber, -70, 11);
                tempDegNumber = _yDeg - (tempDegNumber - _degNumber);
                transform.Rotate(Vector3.right, tempDegNumber, Space.World);
                _yDeg = 0;
            }

            if (Mathf.Abs(_xDeg) <= 0.1f && Mathf.Abs(_yDeg) <= 0.1f)
            {
                _allowInertialDrag = false;
            }
            else
            {
                _allowInertialDrag = true;
            }                   
        }
    }


    private void ZoomModel(float deltaTime, float scroolPosition)
    {
        if (UI.mobileSupport)
        {
            if (_executeScale)
            {
                //Pinch zoom
                if (Input.touchCount == 2)
                {
                    Touch touchZero = Input.GetTouch(0);
                    Touch touchOne = Input.GetTouch(1);

                    Vector2 touchZeroPreviousPosition = touchZero.position - touchZero.deltaPosition;
                    Vector2 touchOnePreviousPosition = touchOne.position - touchOne.deltaPosition;

                    float prevTouchDeltaMag = (touchZeroPreviousPosition - touchOnePreviousPosition).magnitude;
                    float TouchDeltaMag = (touchZero.position - touchOne.position).magnitude;
                    float deltaMagDiff = prevTouchDeltaMag - TouchDeltaMag;

                    if (deltaMagDiff > 1)
                    {
                        _positionZ = Mathf.Clamp(_positionZ + _zoomDelta * 88, _minZoom, _maxZoom);
                        _firstScroll = true;
                    }
                    else if (deltaMagDiff < -1)
                    {
                        _positionZ = Mathf.Clamp(_positionZ - _zoomDelta * 88, _minZoom, _maxZoom);
                        _firstScroll = true;
                    }

                    if (_firstScroll)
                    {
                        _toPosition = transform.localPosition;
                        _toPosition.z = _positionZ;
                        transform.localPosition = Vector3.Lerp(transform.localPosition, _toPosition, deltaTime * _lerpZoomSpeed);
                    }
                }               
            }         
        }
        else
        {
            if (_executeScale)
            {
                if (scroolPosition >= 1)
                {
                    _positionZ = Mathf.Clamp(_positionZ - _zoomDelta, _minZoom, _maxZoom);
                    _firstScroll = true;
                }
                else if (scroolPosition <= -1)
                {
                    _positionZ = Mathf.Clamp(_positionZ + _zoomDelta, _minZoom, _maxZoom);
                    _firstScroll = true;
                }

                if (_firstScroll)
                {
                    _toPosition = transform.localPosition;
                    _toPosition.z = _positionZ;
                    transform.localPosition = Vector3.Lerp(transform.localPosition, _toPosition, deltaTime * _lerpZoomSpeed);
                }
            }
        }     
    }

}