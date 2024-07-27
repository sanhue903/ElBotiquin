using UnityEngine;
using UnityEngine.UI;

public class AlternativeButton : MonoBehaviour
{
    public void OnEnable()
    {
        Button button = GetComponent<Button>();
        if (gameObject.TryGetComponent<AlternativeData>(out var alternativeData))
        {
            button.onClick.AddListener(() => ActivityManager.Instance.Answer(alternativeData));
        }
    }

    public void OnDisable()
    {
        Button button = GetComponent<Button>();
        button.onClick.RemoveAllListeners();
    }
}