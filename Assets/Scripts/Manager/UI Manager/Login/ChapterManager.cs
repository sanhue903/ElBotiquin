using UnityEngine;
using UnityEngine.UI;

public class ChapterManager : MonoBehaviour
{
    [SerializeField] Button[] chaptersButtons;
    // Start is called before the first frame update
    void Start()
    {
        UnlockLastChapter();
    }

    private void UnlockLastChapter()
    {
        int lastChapter = APIManager.Instance.actualStudent.lastCompletedChapter;

        for (int i = 0; i < lastChapter; i++){
            chaptersButtons[i].interactable = true;
        }
    }
}
