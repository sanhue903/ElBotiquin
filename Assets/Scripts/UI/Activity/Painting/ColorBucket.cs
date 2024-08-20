using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ColorBucket : EventTrigger
{
    [SerializeField] private Color color;
    [SerializeField] private int index;
    [SerializeField] private string colorName;
    [SerializeField] private Sprite brush;
    public override void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop" + gameObject.name);

        if (eventData.pointerDrag == null)
        {
            return;
        }
        Debug.Log($"Get color: {colorName}");

        eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
        eventData.pointerDrag.GetComponent<DragBrush>().SetBrush(brush, color, colorName);
    }
}