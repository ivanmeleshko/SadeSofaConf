using UnityEngine;
using UnityEngine.EventSystems;


public class DragControlls : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler, IPointerDownHandler, IPointerUpHandler
{

    public delegate void dragDelegate(bool allow);
    public static dragDelegate OnDragAction;
    public static dragDelegate OnPointerAction;
    private bool _allowDrag = false;
    private bool _pointerDown = false;


    public void OnPointerExit(PointerEventData eventData)
    {
        _allowDrag = false;
        _pointerDown = false;
        OnDragAction?.Invoke(false);
        OnPointerAction?.Invoke(false);
    }
     

    public void OnPointerEnter(PointerEventData eventData)
    {
        _allowDrag = true;
        _pointerDown = true;
        OnPointerAction?.Invoke(true);
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        _pointerDown = true;
        if (_allowDrag && _pointerDown)
            OnDragAction?.Invoke(true);
    }


    public void OnPointerUp(PointerEventData eventData)
    {
        OnDragAction?.Invoke(false);
    }

}

