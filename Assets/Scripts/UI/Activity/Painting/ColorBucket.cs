using UnityEngine;
using UnityEngine.EventSystems;

public class ColorBucket : EventTrigger
{
    [SerializeField] private Color color;
    [SerializeField] private string colorName;
    public override void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop" + gameObject.name);

        if (eventData.pointerDrag == null)
        {
            return;
        }

        eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
        eventData.pointerDrag.GetComponent<DragBrush>().color = color;  
        eventData.pointerDrag.GetComponent<AlternativeData>().answer += $"-{colorName}";
    }
}