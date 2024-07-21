using JetBrains.Annotations;
using UnityEngine;

public class ChapterButton : MonoBehaviour
{
    [SerializeField] private int  dimension = 150;
    [SerializeField] private int fixDistance = 30;
    [SerializeField] private bool isRightCorner;
    [SerializeField] private bool isUpperCorner;

    void Awake()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();

        if (rectTransform == null)
        {
            Debug.LogError("No RectTransform found");
            return;
        }

        float distance = fixDistance + dimension/2;

        float x = isRightCorner ? -distance : distance;
        float y = isUpperCorner ? -distance : distance;

        rectTransform.anchoredPosition = new Vector2(x, y);
        rectTransform.sizeDelta = new Vector2(dimension, dimension);
        
        
    }
}