using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class BodyOnDrop : EventTrigger
{
    public override void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null)
        {
            return;
        }

        Debug.Log($"Painting: {gameObject.name}");

        ActivityManager.Instance.GetActualQuestionData().gameObject.GetComponent<QuestionUI>().correctAnswerAudio = GetComponent<EmotionsAudios>().GetAudio(0);
        GetComponent<Image>().color = eventData.pointerDrag.GetComponent<DragBrush>().color;       

        AlternativeData alternative = eventData.pointerDrag.GetComponent<AlternativeData>();
        alternative.answer += $"-{gameObject.name}";
        ActivityManager.Instance.Answer(alternative);
    }
}