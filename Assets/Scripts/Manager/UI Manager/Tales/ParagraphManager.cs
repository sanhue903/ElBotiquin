using UnityEngine;

public class ParagraphManager : MonoBehaviour
{
    [SerializeField] private GameObject paragraphParent; 
    [SerializeField] private GameObject AudioParent;
    [SerializeField] private string nextScene;

    private int index;
    private int totalParagraphs;
    void Start()
    {
        index = 0;
        totalParagraphs = paragraphParent.transform.childCount;
    }

    public void ShowPreviousParagraph()
    {
        if (index == 0)
        {
            return;
        }
        
        paragraphParent.transform.GetChild(index).gameObject.SetActive(false);
        index--;
        paragraphParent.transform.GetChild(index).gameObject.SetActive(true);
        AudioManager.Instance.PlayAudio(AudioParent.transform.GetChild(index).GetComponent<AudioSource>());
        
    }

    public void ShowNextParagraph()
    {
        if (index >= totalParagraphs - 1)
        {
            SceneManager.Instance.LoadScene(nextScene);
            return;
        }

        paragraphParent.transform.GetChild(index).gameObject.SetActive(false);
        index++;
        paragraphParent.transform.GetChild(index).gameObject.SetActive(true);
        AudioManager.Instance.PlayAudio(AudioParent.transform.GetChild(index).GetComponent<AudioSource>());
    }
}