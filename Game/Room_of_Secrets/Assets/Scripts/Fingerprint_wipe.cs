using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FingerprintEraser : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    private RectTransform rectTransform;
    private Vector2 localPoint;
    public RawImage fingerprintImage_wipe;
    public RawImage fingerprintImage;
    public RawImage eraser;
    public RawImage clueGained;

    private GameManager gameManager;
    public PlayerController playerController;
    public InteractableObject axe; 

    // Define a rectangle in local coordinates that corresponds to the specific area
    private Rect specificArea;

    private void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        rectTransform = GetComponent<RectTransform>();
        SetSpecificArea();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform.parent as RectTransform, eventData.position, eventData.pressEventCamera, out localPoint))
        {
            rectTransform.localPosition = localPoint;
            CheckIfInsideSpecificArea(localPoint);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform.parent as RectTransform, eventData.position, eventData.pressEventCamera, out localPoint))
        {
            rectTransform.localPosition = localPoint;
            CheckIfInsideSpecificArea(localPoint);
        }
    }
    private void SetSpecificArea()
    {
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        float rectWidth = screenWidth * 0.35f;
        float rectHeight = screenHeight * 0.3f;
        float rectX = -screenWidth * 0.37f;
        float rectY = -screenHeight * 0.30f;

        specificArea = new Rect(rectX, rectY, rectWidth, rectHeight);
    }
    private void CheckIfInsideSpecificArea(Vector2 point)
    {
        if (specificArea.Contains(point))
        {
            Debug.Log("Eraser is inside the specific area!");
            fingerprintImage_wipe.gameObject.SetActive(true);
            fingerprintImage.gameObject.SetActive(false);
            eraser.gameObject.SetActive(false);
            gameManager.UpdateClueNumber();
            playerController.clueGained.Add(axe);
            clueGained.gameObject.SetActive(true);
        }
    }
}
