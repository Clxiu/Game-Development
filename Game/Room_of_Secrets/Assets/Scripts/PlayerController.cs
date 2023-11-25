using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameManager gameManager;
    public LockerManager lockerManager;

    private float speed = 2.5f;
    private float turnSpeed = 50.0f;

    private Rigidbody playerRigidBody;
    private CameraFollow cameraFollow;
    public float ForwardForce = 10.0f;
    public float SideForce = 0.5f;
    private float forwardInput;
    public float turningInput;
    private float horizontalInput;

    private InteractableObject currentInteractableObject;
    private string interactionMessage = "You have interacted with the ";

    private Vector3 lastMousePosition;
    private float totalMouseMovement = 0;
    private TextMeshProUGUI glassPanelText;
    private bool buttonCooldown = false;

    public MeshRenderer glassRenderer;
    public Material dirtyMaterial;
    public Material cleanMaterial;
    private CameraZoom cameraZoom;
    private bool hasClickedCorrectly = false;
    private Coroutine reminderCoroutine;
    public TextMeshProUGUI axeHint;
    public TextMeshProUGUI windowHint;
    public TextMeshProUGUI bedHint;
    public TextMeshProUGUI keyHint;
    public TextMeshProUGUI fireplaceHint;
    public TextMeshProUGUI doorLocked;
    public TextMeshProUGUI moreClues;

    public GameObject deadbodyPanel;
    public GameObject axePanel;
    public RawImage axeImage;
    public RawImage fingerprintImage;
    public RawImage eraser;
    public GameObject dairyPanel;
    public GameObject bedPanel;
    public InteractableObject bed;
    public GameObject bedClueGained;
    public GameObject windowPanel;
    public RawImage windowImage;
    public InteractableObject window;
    public GameObject windowClueGained;
    public GameObject glassPanel;
    public GameObject closetPanel;
    public GameObject helpPanel;
    public GameObject clueBagPanel;
    public GameObject mirrorComodePanel;
    public GameObject glassClueGained;
    public GameObject fireplacePanel;
    public InteractableObject fireplace;
    public GameObject fireplaceGained;
    public RawImage fireplaceImage;
    public GameObject smallClosetPanel;
    public RawImage keyImage;
    public GameObject keyGained;
    public InteractableObject smallCloset;
    public InteractableObject testament;
    public GameObject testamentPanel;
    public GameObject successPanel;
    public GameObject creditsPanel;
    public GameObject canInteract;
    public GameObject endingPanel;

    public Button deadbodyCloseButton;
    public Button axeCloseButton;
    public Button dairyCloseButton;
    public Button bedCloseButton;
    public Button windowCloseButton;
    public Button glassCloseButton;
    public Button closetCloseButton;
    public Button helpCloseButton;
    public Button bagCloseButton;
    public Button helpButton;
    public Button hintButton;
    public Button mirrorComodeCloseButton;
    public Button smallClosetCloseButton;
    public Button testamentCloseButton;
    public Button fireplaceCloseButton;
    public Button successCloseButton;

    public GameObject swipePrompt;

    private bool deadbodyOpened;
    private bool axeOpened;
    private bool dairyOpened;
    private bool bedOpened;
    private bool windowOpened;
    private bool closetOpened;
    private bool glassOpened;
    private bool mirrorComodeOpened; 
    private bool smallClosetOpened;
    private bool fireplaceOpened;
    private bool testamentOpened;

    public List<InteractableObject> clueGained;
    public TextMeshProUGUI clueText;
    public GameObject interactMessage;
    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        lockerManager = GameObject.Find("LockerManager").GetComponent<LockerManager>();
        deadbodyOpened = false;
        axeOpened = false;
        dairyOpened = false;
        bedOpened = false;
        windowOpened = false;
        glassOpened = false;
        mirrorComodeOpened = false;
        smallClosetOpened = false;
        fireplaceOpened = false;
        testamentOpened = false;
        clueGained = new List<InteractableObject>();
        swipePrompt.SetActive(false);
        glassPanelText = glassPanel.transform.Find("ClueText").GetComponent<TextMeshProUGUI>();
        fingerprintImage.gameObject.SetActive(false);
        eraser.gameObject.SetActive(false);

        helpButton.onClick.AddListener(HelpButton);
        hintButton.onClick.AddListener(HintButton);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.timerIsRunning)
        {
            horizontalInput = Input.GetAxis("Horizontal");
            forwardInput = Input.GetAxis("Vertical");

            transform.Rotate(Vector3.up, horizontalInput * turnSpeed * Time.deltaTime);
            transform.Translate(Vector3.forward * Time.deltaTime * speed * forwardInput);

            Vector3 move = transform.forward * forwardInput * speed * Time.deltaTime;
            playerRigidBody.MovePosition(playerRigidBody.position + move);

            if (Input.GetKeyDown(KeyCode.F) && currentInteractableObject != null)
            {
                Debug.Log("Player choose to interact with object: " + currentInteractableObject.name);
                Interact(currentInteractableObject);
            }

            if (Input.GetKeyDown(KeyCode.G))
            {
                Debug.Log("Player open clue bag.");
                ShowPanel(clueBagPanel);
                clueText.text = "";
                for (int i = 0; i < clueGained.Count; i++)
                {
                    if (clueGained[i].gameObject.name == "Fireplace")
                    {
                        clueText.text += "- Lady Catherine's Letter to Assassin;\n";
                    }
                    else if (clueGained[i].gameObject.name == "Window")
                    {
                        clueText.text += "- Muddy Footprint Leading to Lady Catherine's Chambers;\n";
                    }
                    else if (clueGained[i].gameObject.name == "Bed")
                    {
                        clueText.text += "- Lady Catherine's Letter to Assassin;\n";
                    }
                    else if (clueGained[i].gameObject.name == "Axe")
                    {
                        clueText.text += "- Lady Catherine's fingerprint on axe;\n";
                    }
                    else if (clueGained[i].gameObject.name == "Dead body")
                    {
                        clueText.text += "- Sir Alexandria's Dead Body, Poisoned, Injured by Axe;\n";
                    }
                    else if (clueGained[i].gameObject.name == "Glass")
                    {
                        clueText.text += "- Butler's Fingerprints on The Glass of Poison;\n";
                    }
                    else if (clueGained[i].gameObject.name == "BigCloset")
                    {
                        clueText.text += "- Sir Alexandria's Letter, Clue to Open the Hidden Room;\n";
                    }
                    else if (clueGained[i].gameObject.name == "SmallCloset")
                    {
                        clueText.text += "- Key to the Main Door;\n";
                    }
                    else if (clueGained[i].gameObject.name == "Dairy")
                    {
                        clueText.text += "- Sir Alexandria's Diary, Wardrobe Password 3527;\n";
                    }
                    else if (clueGained[i].gameObject.name == "Mirror Commode Panel")
                    {
                        clueText.text += "- Sir Alexandria's Letter to Friend;\n";
                    }
                    else if (clueGained[i].gameObject.name == "RoundTable_hidden")
                    {
                        clueText.text += "- Sir Alexandria's Testament;\n";
                    }
                }
                bagCloseButton.onClick.AddListener(() => HidePanel(clueBagPanel));
            }
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        InteractableObject interactableObject = collision.gameObject.GetComponent<InteractableObject>();
        if (interactableObject != null)
        {
            currentInteractableObject = interactableObject;
            interactMessage.SetActive(true);
        }
    }

    void OnCollisionExit(Collision collision)
    {
        InteractableObject interactableObject = collision.gameObject.GetComponent<InteractableObject>();
        if (interactableObject != null)
        {
            currentInteractableObject = null;
            interactMessage.SetActive(false);
        }
    }
    public void Interact(InteractableObject gameObject)
    {
        Debug.Log(interactionMessage + gameObject.name);
        //Debug.Log("The player has interacted with the son's symbols " + FindFirstObjectByType<InteractSonsSymbols>().hasInteracted);
        if (gameObject.name == "Dead body")
        {
            ShowPanel(deadbodyPanel);
            if (deadbodyOpened == false)
            {
                deadbodyOpened = true;
                gameManager.UpdateClueNumber();
                clueGained.Add(gameObject);
            }
            deadbodyCloseButton.onClick.AddListener(() => HidePanel(deadbodyPanel));
        }
        else if (gameObject.name == "Axe")
        {
            if (axeOpened == false)
            {
                StartReminder(axeHint);
                axeOpened = true;
            }
            ShowPanel(axePanel);
            axeCloseButton.onClick.AddListener(() => HidePanel(axePanel));

        }
        else if (gameObject.name == "Dairy")
        {
            ShowPanel(dairyPanel);
            if (dairyOpened == false)
            {
                dairyOpened = true;
                gameManager.UpdateClueNumber();
                clueGained.Add(gameObject);
            }
            dairyCloseButton.onClick.AddListener(() => HidePanel(dairyPanel));

        }
        else if (gameObject.name == "Bed")
        {
            ShowPanel(bedPanel);
            if (bedOpened == false)
            {
                StartReminder(bedHint);
                bedOpened = true;
            }
            bedCloseButton.onClick.AddListener(() => HidePanel(bedPanel));

        }
        else if (gameObject.name == "Window")
        {
            ShowPanel(windowPanel);

            if (windowOpened == false)
            {
                StartReminder(windowHint);
                windowOpened = true;
            }
            windowCloseButton.onClick.AddListener(() => HidePanel(windowPanel));
        }
        else if (gameObject.name == "Glass")
        {
            ShowPanel(glassPanel);

            if (!glassOpened) 
            {
                glassPanelText.text = "Move your mouse in circles to clean the glass";
                cameraZoom = Camera.main.GetComponent<CameraZoom>();
                cameraZoom.ZoomIn();
                StartCoroutine(CheckForCircularMouseMovement());
                cameraZoom.ZoomOut(); 
                glassOpened = true;
                gameManager.UpdateClueNumber();
                clueGained.Add(gameObject);
            }

            glassCloseButton.onClick.AddListener(() => HidePanel(glassPanel));
        }
        else if (gameObject.name == "MirrorComode")
        {
            ShowPanel(mirrorComodePanel);

            if (mirrorComodeOpened == false)
            {
                mirrorComodeOpened = true;
                gameManager.UpdateClueNumber();
                clueGained.Add(gameObject);
            }
            mirrorComodeCloseButton.onClick.AddListener(() => HidePanel(mirrorComodePanel));
        }
        else if (gameObject.name == "BigCloset")
        {
            ShowPanel(closetPanel);
            if (lockerManager.opened && !closetOpened)
            {
                closetOpened = true;
                gameManager.UpdateClueNumber();
                clueGained.Add(gameObject);
            }
            closetCloseButton.onClick.AddListener(() => HidePanel(closetPanel));

        }
        else if (gameObject.name == "Fireplace")
        {
            ShowPanel(fireplacePanel);
            if (fireplaceOpened == false)
            {
                StartReminder(fireplaceHint);
                fireplaceOpened = true;
            }
            fireplaceCloseButton.onClick.AddListener(() => HidePanel(fireplacePanel));
        }
        else if (gameObject.name == "SmallCloset")
        {
            //bool clueFoundStatue = FindObjectOfType<InteractSonsSymbols>().hasInteracted;
            //bool clueFoundSymbols = FindObjectOfType<InteractStatue>().hasInteracted;
            //if (clueFoundStatue && clueFoundSymbols)
            //{
            //    ShowPanel(smallClosetPanel);
            //    if (smallClosetOpened == false)
            //    {
            //        StartReminder(keyHint);
            //        smallClosetOpened = true;
            //    }
            //    smallClosetCloseButton.onClick.AddListener(() => HidePanel(smallClosetPanel));
            //}
            //else
            //{
            //    Debug.Log("First get the clues from the son's symbols");
            //}
            //testamentCloseButton.onClick.AddListener(() => HidePanel(testamentPanel));
            ShowPanel(smallClosetPanel);
            if (smallClosetOpened == false)
            {
                StartReminder(keyHint);
                smallClosetOpened = true;
            }
            smallClosetCloseButton.onClick.AddListener(() => HidePanel(smallClosetPanel));

        }
        else if (gameObject.name == "RoundTable_hidden")
        {
            ShowPanel(testamentPanel);

            if (testamentOpened == false)
            {
                testamentOpened = true;
                gameManager.UpdateClueNumber();
                clueGained.Add(testament);
            }
            testamentCloseButton.onClick.AddListener(() => HidePanel(testamentPanel));
        }

        else if (gameObject.name == "MainDoor")
        {
            if (gameManager.keyNumber >= 1 && gameManager.clueNumber >= 11)
            {
                gameManager.timerIsRunning = false;
                gameManager.GameSuccess();
                successPanel.gameObject.SetActive(true);
            }
            else if (gameManager.keyNumber < 1)
            {
                doorLocked.gameObject.SetActive(true);
            }
            else if (gameManager.clueNumber < 11)
            {
                moreClues.gameObject.SetActive(true);
            }
        }
    }
    public void ShowPanel(GameObject panel)
    {
        panel.SetActive(true);
        interactMessage.SetActive(false);
    }
    public void HidePanel(GameObject panel)
    {
        panel.SetActive(false);
    }
    public void HelpButton()
    {
        helpPanel.SetActive(true);
        helpCloseButton.onClick.AddListener(() => HidePanel(helpPanel));
    }
    private IEnumerator ButtonCooldownRoutine()
    {
        buttonCooldown = true;
        yield return new WaitForSeconds(1.0f);
        buttonCooldown = false;
    }
    public void HintButton()
    {
        if (!buttonCooldown)
        {
            StartCoroutine(ButtonCooldownRoutine());
            gameManager.UpdateHintNumber();
        }
    }

    private IEnumerator CheckForCircularMouseMovement()
    {
        // Start with the dirty material
        glassRenderer.material = dirtyMaterial;

        lastMousePosition = Input.mousePosition;
        float initialMouseMovement = totalMouseMovement;

        while (totalMouseMovement - initialMouseMovement < 3000f)
        {
            Vector3 currentMousePosition = Input.mousePosition;
            totalMouseMovement += Vector3.Distance(lastMousePosition, currentMousePosition);
            lastMousePosition = currentMousePosition;

            float lerpValue = (totalMouseMovement - initialMouseMovement) / 3000f;

            if (lerpValue >= 1f)
            {
                // Once enough movement is detected, change to the clean material
                glassRenderer.material = cleanMaterial;

                // Hide the mouse movement prompt
                swipePrompt.SetActive(false);

                // Transition the glass to reveal the clue
                StartCoroutine(PresentClue());
                break;
            }
            yield return null;
        }
    }

    private IEnumerator PresentClue()
    {
        glassPanelText.text = "";
        glassClueGained.gameObject.SetActive(true);
        yield return null;
    }


    public void OnAxeImageClicked(BaseEventData eventData)
    {
        axeHint.gameObject.SetActive(false);
        // Convert the mouse position to local coordinates within the image
        Vector2 localCursor;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(windowImage.rectTransform, ((PointerEventData)eventData).position, ((PointerEventData)eventData).pressEventCamera, out localCursor))
        {
            Debug.Log("Local Position: " + localCursor);
        }

        // Define a rectangle in local coordinates that corresponds to the specific area
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        float rectWidth = screenWidth * 0.24f;
        float rectHeight = screenHeight * 0.25f;
        float rectX = -screenWidth * 0.37f;
        float rectY = -screenHeight * 0.25f;

        Rect specificArea = new Rect(rectX, rectY, rectWidth, rectHeight);

        // Check if the mouse click position is within the specific area
        if (specificArea.Contains(localCursor))
        {
            axeHint.gameObject.SetActive(false);
            hasClickedCorrectly = true;
            Debug.Log("Specific area of the image clicked!");
            axeImage.gameObject.SetActive(false);
            fingerprintImage.gameObject.SetActive(true);
            eraser.gameObject.SetActive(true);
        }
    }
    public void OnWindowImageClicked(BaseEventData eventData)
    {
        windowHint.gameObject.SetActive(false);
        // Convert the mouse position to local coordinates within the image
        Vector2 localCursor;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(windowImage.rectTransform, ((PointerEventData)eventData).position, ((PointerEventData)eventData).pressEventCamera, out localCursor))
        {
            Debug.Log("Local Position: " + localCursor);
        }

        // Define a rectangle in local coordinates that corresponds to the specific area
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        float rectWidth = screenWidth * 0.3f;
        float rectHeight = screenHeight * 0.8f;
        float rectX = -screenWidth * 0.4f;
        float rectY = -screenHeight * 0.4f;

        Rect specificArea = new Rect(rectX, rectY, rectWidth, rectHeight);

        // Check if the mouse click position is within the specific area
        if (specificArea.Contains(localCursor))
        {
            windowHint.gameObject.SetActive(false);
            gameManager.UpdateClueNumber();
            clueGained.Add(window);
            hasClickedCorrectly = true;
            Debug.Log("Specific area of the image clicked!");
            windowClueGained.gameObject.SetActive(true);
        }
    }
    public void OnBedImageClicked(BaseEventData eventData)
    {
        bedHint.gameObject.SetActive(false);
        // Convert the mouse position to local coordinates within the image
        Vector2 localCursor;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(windowImage.rectTransform, ((PointerEventData)eventData).position, ((PointerEventData)eventData).pressEventCamera, out localCursor))
        {
            Debug.Log("Local Position: " + localCursor);
        }

        // Define a rectangle in local coordinates that corresponds to the specific area
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        float rectWidth = screenWidth * 0.5f;
        float rectHeight = screenHeight * 0.5f;
        float rectX = -screenWidth * 0.25f;
        float rectY = -screenHeight * 0.25f;

        Rect specificArea = new Rect(rectX, rectY, rectWidth, rectHeight);

        // Check if the mouse click position is within the specific area
        if (specificArea.Contains(localCursor))
        {
            bedHint.gameObject.SetActive(false);
            gameManager.UpdateClueNumber();
            clueGained.Add(bed);
            hasClickedCorrectly = true;
            Debug.Log("Specific area of the image clicked!");
            bedClueGained.gameObject.SetActive(true);
        }
    }
    public void OnKeyImageClicked(BaseEventData eventData)
    {
        keyHint.gameObject.SetActive(false);
        // Convert the mouse position to local coordinates within the image
        Vector2 localCursor;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(keyImage.rectTransform, ((PointerEventData)eventData).position, ((PointerEventData)eventData).pressEventCamera, out localCursor))
        {
            Debug.Log("Local Position: " + localCursor);
        }

        // Define a rectangle in local coordinates that corresponds to the specific area
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        float rectWidth = screenWidth * 0.3f;
        float rectHeight = screenHeight * 0.4f;
        float rectX = -screenWidth * 0.25f;
        float rectY = -screenHeight * 0.25f;

        Rect specificArea = new Rect(rectX, rectY, rectWidth, rectHeight);

        // Check if the mouse click position is within the specific area
        if (specificArea.Contains(localCursor))
        {
            keyHint.gameObject.SetActive(false);
            gameManager.UpdateClueNumber();
            gameManager.UpdateKeyNumber();
            hasClickedCorrectly = true;
            clueGained.Add(smallCloset);
            Debug.Log("Specific area of the image clicked!");
            keyGained.gameObject.SetActive(true);
        }
    }
    public void OnFireplaceImageClicked(BaseEventData eventData)
    {
        fireplaceHint.gameObject.SetActive(false);
        // Convert the mouse position to local coordinates within the image
        Vector2 localCursor;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(keyImage.rectTransform, ((PointerEventData)eventData).position, ((PointerEventData)eventData).pressEventCamera, out localCursor))
        {
            Debug.Log("Local Position: " + localCursor);
        }

        // Define a rectangle in local coordinates that corresponds to the specific area
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        float rectWidth = screenWidth * 0.3f;
        float rectHeight = screenHeight * 0.2f;
        float rectX = -screenWidth * 0.2f;
        float rectY = -screenHeight * 0.2f;

        Rect specificArea = new Rect(rectX, rectY, rectWidth, rectHeight);

        // Check if the mouse click position is within the specific area
        if (specificArea.Contains(localCursor))
        {
            fireplaceHint.gameObject.SetActive(false);
            gameManager.UpdateClueNumber();
            hasClickedCorrectly = true;
            clueGained.Add(fireplace);
            Debug.Log("Specific area of the image clicked!");
            fireplaceGained.gameObject.SetActive(true);
        }
    }
    public void StartReminder(TextMeshProUGUI hintMessage)
    {
        Debug.Log("Start timing ...");
        if (reminderCoroutine != null)
        {
            StopCoroutine(reminderCoroutine);
        }
        hasClickedCorrectly = false;
        reminderCoroutine = StartCoroutine(ReminderAfterDelay(10.0f, hintMessage));
    }
    private IEnumerator ReminderAfterDelay(float delay, TextMeshProUGUI hintMessage)
    {
        yield return new WaitForSeconds(delay);
        if (!hasClickedCorrectly)
        {
            hintMessage.gameObject.SetActive(true);
            Debug.Log("Show Hint!");
        }
    }
    public void onSuccessCloseButtonClick()
    {
        gameManager.timerIsRunning = false;
        canInteract.gameObject.SetActive(false);
        successPanel.gameObject.SetActive(false);
        creditsPanel.gameObject.SetActive(true);
    }
    public void onEndingClick()
    {
        gameManager.timerIsRunning = false;
        creditsPanel.gameObject.SetActive(false);
        endingPanel.gameObject.SetActive(true);
    }
}
