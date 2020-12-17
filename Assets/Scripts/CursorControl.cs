using UnityEngine;
using UnityEngine.EventSystems;

public class CursorControl : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler
{

    [SerializeField]
    Texture2D cursorTexture;
    Vector2 hotSpot = new Vector2(15, 0);
    CursorMode cursorMode = CursorMode.Auto; 


    public void OnPointerExit(PointerEventData eventData)
    {
        Cursor.SetCursor(null, hotSpot, CursorMode.Auto);
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        Cursor.SetCursor(cursorTexture, hotSpot, CursorMode.Auto);
    }

}
