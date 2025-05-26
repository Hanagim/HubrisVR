using UnityEngine;
using TMPro;
using System.Collections;

public class SubtitleManager : MonoBehaviour
{
    public TextMeshProUGUI subtitleText; // Drag your TextMeshProUGUI here in the Inspector
    public float defaultDisplayDuration = 4f;

    private Coroutine subtitleCoroutine;

    private void Awake()
    {
        if (subtitleText != null)
            subtitleText.text = "";
    }

    public void ShowSubtitle(string message)
    {
        if (subtitleCoroutine != null)
            StopCoroutine(subtitleCoroutine);

        subtitleCoroutine = StartCoroutine(DisplaySubtitle(message, defaultDisplayDuration));
    }

    public void ShowSubtitleWithDuration(string messageAndDuration)
    {
        // Format: "This is a line|5" where 5 is the duration in seconds
        string[] parts = messageAndDuration.Split('|');
        string text = parts[0];
        float duration = defaultDisplayDuration;

        if (parts.Length > 1 && float.TryParse(parts[1], out float parsedDuration))
            duration = parsedDuration;

        if (subtitleCoroutine != null)
            StopCoroutine(subtitleCoroutine);

        subtitleCoroutine = StartCoroutine(DisplaySubtitle(text, duration));
    }

    private IEnumerator DisplaySubtitle(string message, float duration)
    {
        subtitleText.text = message;
        yield return new WaitForSeconds(duration);
        subtitleText.text = "";
    }

    public void HideSubtitle()
    {
        if (subtitleCoroutine != null)
            StopCoroutine(subtitleCoroutine);

        subtitleText.text = "";
    }
}
