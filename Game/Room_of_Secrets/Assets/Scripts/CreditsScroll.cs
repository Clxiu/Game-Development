using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreditsScroll : MonoBehaviour
{
    public float scrollSpeed = 10.0f;
    private RectTransform rectTransform;
    private TextMeshProUGUI textMesh;
    private float initialPosY;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        textMesh = GetComponent<TextMeshProUGUI>();
        initialPosY = rectTransform.anchoredPosition.y;
    }

    private void Update()
    {
        rectTransform.anchoredPosition += new Vector2(0, scrollSpeed * Time.deltaTime);

        if (rectTransform.anchoredPosition.y > textMesh.preferredHeight)
        {
            rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, initialPosY);
        }
    }
}
