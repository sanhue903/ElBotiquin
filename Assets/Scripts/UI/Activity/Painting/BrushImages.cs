using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class BrushImages : MonoBehaviour
{
    [Header("Brush Elements")]
    [SerializeField] private List<Sprite> brushes;

    public void SetImage(int index)
    {
        GetComponent<Image>().sprite = brushes[index];
    }
}