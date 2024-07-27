using UnityEngine;
using UnityEngine.EventSystems;
public class DragBrush : DragItem
{
    public Color color;
    public string colorName;

    public new void OnBeginDrag(PointerEventData eventData)
    {
        base.OnBeginDrag(eventData);
        gameObject.GetComponent<AlternativeData>().answer = ActivityManager.Instance.GetAcualQuestionName();
    }
}
