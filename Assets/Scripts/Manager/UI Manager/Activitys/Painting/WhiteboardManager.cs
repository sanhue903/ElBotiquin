using UnityEngine;
using UnityEngine.UI;

public class WhiteboardManager : MonoBehaviour
{
    [Header("Bucket")]
    [SerializeField] private Sprite[] bucketSprites;
    [SerializeField] private Image bucket;

    [Header("Brush")]
    [SerializeField] private Sprite[] brushSprites;
    [SerializeField] private Color[] brushColors;
    [SerializeField] private GameObject brushGameObject;

    private DragBrush brushColor;
    private Image brushImage;
    private int it;

    [Header("Body Parts")]
    [SerializeField] private PaintBody[] bodyParts;
    [Header("Next Scene")]
    [SerializeField] private string nextScene;
    void Start()
    {
        brushColor = brushGameObject.GetComponent<DragBrush>();
        brushImage = brushGameObject.GetComponent<Image>();

        it = 0;

        brushImage.sprite = brushSprites[it];
        brushColor.color = brushColors[it];
        bucket.sprite = bucketSprites[it];
    }

    public void NextColor()
    {
        it++;

        if (it >= brushSprites.Length)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(nextScene);
            return;
        }

        brushImage.sprite = brushSprites[it];
        brushColor.color = brushColors[it];
        bucket.sprite = bucketSprites[it];

        foreach (PaintBody bodyPart in bodyParts)
        {
            bodyPart.ResetPaint();
        }
    }
}