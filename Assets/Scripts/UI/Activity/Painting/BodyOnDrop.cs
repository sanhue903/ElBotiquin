using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class BodyOnDrop : EventTrigger
{
    private Image image;
    void Start()
    {
        image = GetComponent<Image>();
    }   
    public override void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null)
        {
            return;
        }

        gameObject.GetComponent<QuestionUI>().correctAnswerAudio = gameObject.GetComponent<BodyData>().GetAudio(0);
        image.color = eventData.pointerDrag.GetComponent<DragBrush>().color;       

        AlternativeData alternative = eventData.pointerDrag.GetComponent<AlternativeData>();
        alternative.answer += $"-{gameObject.name}";
        ActivityManager.Instance.Answer(alternative);
    }
}