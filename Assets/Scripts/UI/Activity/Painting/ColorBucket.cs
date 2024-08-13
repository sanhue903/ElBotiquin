using UnityEngine;
using UnityEngine.EventSystems;

public class ColorBucket : EventTrigger
{
    [SerializeField] private Color color;
    [SerializeField] private int index;
    [SerializeField] private string colorName;
    public override void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop" + gameObject.name);

        if (eventData.pointerDrag == null)
        {
            return;
        }
        Debug.Log($"Get color: {colorName}");

        eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
        eventData.pointerDrag.GetComponent<DragBrush>().color = color; 
        eventData.pointerDrag.transform.GetChild(0).GetComponent<BrushImages>().SetImage(index); 
        eventData.pointerDrag.GetComponent<AlternativeData>().answer = $"{ActivityManager.Instance.GetActualQuestionData().name}-{colorName}";
    }
}