using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDrop : EventTrigger
{
    public override void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop" + gameObject.name);

        if (eventData.pointerDrag == null)
        {
            return;
        }
        
        eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
    }
}                                                        