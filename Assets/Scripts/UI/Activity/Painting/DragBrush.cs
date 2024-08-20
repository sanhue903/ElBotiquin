using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class DragBrush : DragItem
{
    public Color color;
    public string colorName;

    public bool hasColor;
    
    private Image image;
    private Image defaultImage;

    void Start()
    {
        image = gameObject.transform.GetChild(0).GetComponent<Image>();
        defaultImage = image;
        hasColor = false;
    }

    
    public void ResetBrush()
    {
        color = Color.white;
        image.sprite = defaultImage.sprite;
        hasColor = false;
    }

    public void SetBrush(Sprite image, Color color, string colorName)
    {
        this.image.sprite = image;
        this.color = color;
        this.colorName = colorName;
        this.hasColor = true;
    }
}
