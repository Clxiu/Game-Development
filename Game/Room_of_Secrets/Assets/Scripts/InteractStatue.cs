using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InteractStatue : Interact
{
    // Start is called before the first frame update
    public bool hasInteracted;
    public GameObject clueSolution;
    private TMP_InputField clueInput;

    


    void Start()
    {
        base.Start();
        clueInput = clueSolution.GetComponent<TMP_InputField>();

    }

    // Update is called once per frame
    void Update()
    {
        bool interactInput = Input.GetKeyDown(KeyCode.F);
        if (interactInput && canInteract && !hasInteracted ) {
            if (FindObjectOfType<InteractSonsSymbols>().hasInteracted) { interactWithClue(); }
            else {
                Debug.Log("What are the son's symbols");
            }
        }   
    }

    public override void interactWithClue()
    {
        clueUI.displayClue("Rotating Dancer");
        clueSolution.SetActive(true);
        clueInput.onEndEdit.AddListener(delegate { checkClue(clueInput.text); });

    }

    //Check what happens when the check button is pressed
    void checkClue(string solution) {
        if (solution.ToLower().Equals("deer"))
        {
            clueSolution.SetActive(false);
            clueUI.stopDisplayClue();
            hasInteracted = true;
            FindObjectOfType<GameManager>().UpdateClueNumber();
            
        }

    }

}
