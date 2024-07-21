using UnityEngine;
using UnityEngine.EventSystems;

public class ColorBucket : EventTrigger
{
    [SerializeField] private Color color;

    public override void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop" + gameObject.name);

        if (eventData.pointerDrag == null)
        {
            return;
        }

        eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
        eventData.pointerDrag.GetComponent<DragBrush>().color = color;  
    }
}