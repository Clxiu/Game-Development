using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// A UI Panel that is attached to each of my Clues i.e 
/// Rotating statue and son's symbols
/// </summary>
public class ClueUI : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject cluePanel;
    public GameObject titleImage;
    public GameObject clueSolution;
    public Button closeButton;
    public Sprite SonSymbol;
    public Sprite rotatingDancer;
    void Start()
    {
        closeButton.onClick.AddListener(Hide);
        cluePanel.SetActive(false);
    }

    

    void Hide() {
        cluePanel.SetActive(false);    
    }

    //Display the clue; The switch statements are faster and easier to read
    public void displayClue(string clueTitle) {
        Image titleImageComponent = titleImage.GetComponent<Image>();
        switch (clueTitle)
        {
            case "Son's Symbols":
                //Set the titleImage to be the image for this clue
                cluePanel.SetActive(true);
                titleImage.SetActive(true);
                titleImageComponent.sprite = SonSymbol;
                break;

            case "Rotating Dancer":
                cluePanel.SetActive(true);
                titleImageComponent.sprite = rotatingDancer;
                Debug.Log("Rotating dancer case" + titleImageComponent);
                titleImage.SetActive(true);
                break;
        }
    }

    //Stop the display
    public void stopDisplayClue() {
        cluePanel.SetActive(false);
    }

}
