using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class PaintBody : EventTrigger
{
    [SerializeField] private GameObject nextButton;
    private bool isPainted;
    private Image image;
    void Start()
    {
        image = GetComponent<Image>();
        isPainted = false;
    }   
    public override void OnDrop(PointerEventData eventData)
    {
        if (isPainted)
        {
            return;
        }

        if (eventData.pointerDrag == null)
        {
            return;
        }
       image.color = eventData.pointerDrag.GetComponent<DragBrush>().color;
       isPainted = true;

        Debug.Log("OnDrop " + gameObject.name + " " + image.color);
         if (nextButton != null)
         {
              nextButton.SetActive(true);
         }
    }

    public void ResetPaint()
    {
        isPainted = false;
    }

}