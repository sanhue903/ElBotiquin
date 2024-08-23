using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class DragBrush : DragItem
{
    public Color color;
    public string colorName;

    public bool hasColor;
    
    private Image image;
    [SerializeField] private Sprite defaultImage;

    void Start()
    {
        image = gameObject.transform.GetChild(0).GetComponent<Image>();
        hasColor = false;
    }

    
    public void ResetBrush()
    {
        Debug.Log("Reset Brush");

        color = Color.white;
        image.sprite = defaultImage;
        hasColor = false;
    }

    public void SetBrush(Sprite image, Color color, string colorName)
    {
        this.image.sprite = image;
        this.color = color;
        this.colorName = colorName;
        hasColor = true;
    }
}
