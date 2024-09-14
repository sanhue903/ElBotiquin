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
        actualQuestion.gameObject.GetComponent<QuestionUI>().correctAnswerAudio = GetAudio(actualQuestion.id);
        
        AlternativeData alternative = GetComponent<AlternativeData>();                

        string answer = $"{actualQuestion.nombre}-{gameObject.name}-{brush.colorName}";

        if (alternative.answer == answer){
            return;
        } 

        GetComponent<Image>().color = brush.color;

        alternative.answer = answer;

        ActivityManager.Instance.Answer(alternative);
    }

    private AudioSource GetAudio(string id)
    {
        AudioSource audio;
        
        switch (id)
        {
            case "AUTE21":
                audio = GetComponent<EmotionsAudios>().GetAudio(0);
                break;
            case "AUTE22":
                audio = GetComponent<EmotionsAudios>().GetAudio(1);
                break;
            case "AUTE23":
                audio = GetComponent<EmotionsAudios>().GetAudio(2);
                break;
            case "AUTE24":
                audio = GetComponent<EmotionsAudios>().GetAudio(3);
                break;
            default:
                audio = null;
                break;
        }

        return audio;     


    }
}