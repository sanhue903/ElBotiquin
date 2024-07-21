using UnityEngine;
using UnityEngine.UI;

public class UISpriteSheetAnimation : MonoBehaviour
{

    [SerializeField] private string sheetName;
    [SerializeField] private int frameRate = 30;
    [SerializeField] private float speed = 0.05f;
    [SerializeField] private bool loop = true;


    private Image image;
    private Sprite[] sprites;

    private float timePerFrame;
    private float elapsedTime;
    private int currentFrame; 

    void Start()
    {
        timePerFrame = 0f;
        elapsedTime = 0f;
        currentFrame = 0;

        image = GetComponent<Image>();
        enabled = false;
        LoadSpriteSheet();
    }

    void Update()
    {
        elapsedTime += Time.deltaTime * speed;

        if (elapsedTime >= timePerFrame)
        {
            elapsedTime = 0f;
            SetSprite();
            ++currentFrame;

            if (currentFrame >= sprites.Length)
            {
                if (loop)
                {
                    Debug.Log("Loop");
                    currentFrame = 0;
                }
                else
                {
                    enabled = false;
                }
            }
        }
    }

    private void SetSprite()
    {
        if (currentFrame < sprites.Length && currentFrame >= 0)
        {
            image.sprite = sprites[currentFrame];
        }
    }
    private void LoadSpriteSheet()
    {
        sprites = Resources.LoadAll<Sprite>("Sprites/Animation/" + sheetName);

        if (sprites == null || sprites.Length == 0)
        {
            Debug.LogError("Sprites not found");
        }

        timePerFrame = 1f / frameRate;
        Play();
    }

    private void Play()
    {
        enabled = true;
    }
}