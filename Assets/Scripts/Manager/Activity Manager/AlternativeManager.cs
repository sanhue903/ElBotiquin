using UnityEngine;
using RestClient.Core.Singletons;
class AlternativeManager : Singleton<AlternativeManager>
{
    void Start()
    {
    }
    public void TellAlternative(Alternative alternative)
    {
        AudioManager.Instance.PlayAudio(alternative.audio);
        StartAnimation(alternative);
    }

    public void StartAnimation(Alternative alternative)
    {
        if (alternative.button.gameObject.TryGetComponent(out Animator animation))
        {
            animation.enabled = true;
        }
    }

    public void StopAnimation(Alternative alternative)
    {
        if (alternative.button.gameObject.TryGetComponent(out Animator animation))
        {
            animation.SetBool("isLooping", false); 
        }
    }

    public void EnableAlternaitve(Alternative alternative)
    {
        alternative.button.interactable = true;
    }

    public void DisableAlternaitve(Alternative alternative)
    {
        alternative.button.interactable = false;
    }
}