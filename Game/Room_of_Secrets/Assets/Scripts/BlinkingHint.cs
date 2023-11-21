using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BlinkingHint : MonoBehaviour
{
    public TextMeshProUGUI textToBlink;
    public float fadeInTime = 1.0f;
    public float fadeOutTime = 1.0f;

    private void Start()
    {
        StartCoroutine(FadeText());
    }

    IEnumerator FadeText()
    {
        while (true)
        {
            // Fade out
            for (float t = 0; t < fadeOutTime; t += Time.deltaTime)
            {
                Color textColor = textToBlink.color;
                textColor.a = Mathf.Lerp(1, 0, t / fadeOutTime);
                textToBlink.color = textColor;
                yield return null;
            }

            // Fade in
            for (float t = 0; t < fadeInTime; t += Time.deltaTime)
            {
                Color textColor = textToBlink.color;
                textColor.a = Mathf.Lerp(0, 1, t / fadeInTime);
                textToBlink.color = textColor;
                yield return null;
            }
        }
    }
}

