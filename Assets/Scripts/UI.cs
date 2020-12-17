using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class UI : MonoBehaviour
{

    public Canvas canvas, canvasMobile;
    public Image[] outlinesColor, outlinesBase, outlinesMod;
    const string COLORMAGHEX = "#E6194D";
    const string COLORTRANSP = "#00000000";
    public static bool mobileSupport;


    private void Awake()
    {
        mobileSupport = !(SystemInfo.operatingSystem.Contains("Windows") || SystemInfo.operatingSystem.Contains("Mac"));
        canvasMobile.gameObject.SetActive(mobileSupport);
        canvas.gameObject.SetActive(!mobileSupport);
    }


    public void SetBorder(Image imgOutline)
    {
            foreach (Image img in outlinesColor)
            {
                if (img.name.Equals(imgOutline.name))
                {
                    Color color;
                    if (ColorUtility.TryParseHtmlString(COLORMAGHEX, out color))
                    {
                        img.color = color;
                    }    
                }
            else
            {
                Color color;
                if (ColorUtility.TryParseHtmlString(COLORTRANSP, out color))
                {
                    img.color = color;
                }
            }
        }
    }


    public static bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current)
        {
            position = new Vector2(Input.mousePosition.x, Input.mousePosition.y)
        };
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }

}