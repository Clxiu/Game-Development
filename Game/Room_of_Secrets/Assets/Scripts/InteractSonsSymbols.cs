using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InteractSonsSymbols: Interact
{
    // Start is called before the first frame update
    public bool hasInteracted;
    public GameObject clueSolution;
    private TMP_InputField clueInput;
    public GameObject clueProgression;
    public float clueTime;
    public Sprite sonsSymbols;

    void Start()
    {
        base.Start();
        clueInput = clueSolution.GetComponent<TMP_InputField>();
        

    }

    // Update is called once per frame
    void Update()
    {
        bool interactInput = Input.GetKeyDown(KeyCode.F);
        if (interactInput && canInteract && !hasInteracted)
        {
            interactWithClue();
        }
    }

    //What happens after F is pressed

    public override void interactWithClue()
    {
        clueUI.displayClue("Son's Symbols");
        clueSolution.SetActive(true);
        clueInput.onEndEdit.AddListener(delegate { checkClue(clueInput.text); });

    }

    //Check what happens when the check button is pressed
    void checkClue(string solution)
    {
        Debug.Log("Pressed Enter");
        if (solution.ToLower().Equals("bed")) {
            clueSolution.SetActive(false);
            clueUI.stopDisplayClue();
            hasInteracted = true;
            StartCoroutine(nextClue());
            FindObjectOfType<GameManager>().UpdateClueNumber();
        }
    }

    //Show the Info related to the son's symbols 
    //This info would make for the input for the next clue
    //Show the image only for a few `clueTime` amount of time
    IEnumerator nextClue() { 
       clueProgression.SetActive(true);
        Image clueProgressionImage = clueProgression.GetComponent<Image>();
        clueProgressionImage.sprite = sonsSymbols;
        yield return new  WaitForSeconds(clueTime);
        clueProgression.SetActive(false);
    }



}
