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

        DragBrush brush = eventData.pointerDrag.GetComponent<DragBrush>();       

        if (!brush.hasColor)
        {
            Debug.LogError("No color");
            return;
        }

        Debug.Log($"Painting: {gameObject.name}");

        QuestionData actualQuestion = ActivityManager.Instance.GetActualQuestionData();
        actualQuestion.gameObject.GetComponent<QuestionUI>().correctAnswerAudio = GetComponent<EmotionsAudios>().GetAudio(0);
        
        
        GetComponent<Image>().color = brush.color;

        AlternativeData alternative = GetComponent<AlternativeData>();
        alternative.answer = $"{actualQuestion.nombre}-{gameObject.name}-{brush.colorName}";

        ActivityManager.Instance.Answer(alternative);
    }
}