using UnityEngine;
using UnityEngine.UI;

public class AlternativeUI : MonoBehaviour
{

    [Header("Audio Elements")]
    [SerializeField] private AudioSource answerAudio;
    public float GetAudioLength()
    {
        return answerAudio.clip.length;
    }

    public void ReadAlternative()
    {
        AudioManager.Instance.PlayAudio(answerAudio);
        StartAnimation();
    }

    private void StartAnimation()
    {
        if (gameObject.TryGetComponent<Animator>(out Animator animation))
        {
            animation.enabled = true;
        }
    }

    public void StopAnimation()
    {
        if (gameObject.TryGetComponent<Animator>(out Animator animation))
        {
            animation.SetBool("isLooping", false);
        }
    }

    public void EnableAlternative()
    {
        if (gameObject.TryGetComponent<Button>(out Button button))
        {
            button.interactable = true;
        }
    }

    public void DisableAlternative()
    {
        if (gameObject.TryGetComponent<Button>(out Button button))
        {
            button.interactable = false;
        }
    }
}