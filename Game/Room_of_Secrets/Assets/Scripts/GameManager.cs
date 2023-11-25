using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Security.Cryptography;
using System.IO;

public class GameManager : MonoBehaviour
{
    public bool isGameActive;
    public TextMeshProUGUI gameOverText;
    public Button restartButton;
    public GameObject titleScreen;
    public GameObject gameInterface;

    public int keyNumber;
    public int clueNumber;
    private int hintNumber = 3;
    private float timeRemaining = 900;
    public bool timerIsRunning = false;

    public TextMeshProUGUI clueText;
    public TextMeshProUGUI keyText;
    public TextMeshProUGUI hintText;
    public TextMeshProUGUI countdownText;
    public Dictionary<string, string> cluePanelMapper; 
    // Start is called before the first frame update
    void Start()
    {
        cluePanelMapper = readCSV();
    }

    // Update is called once per frame
    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                timeRemaining = 0;
                timerIsRunning = false;
                GameOver();
            }
        }
    }
    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        countdownText.text = string.Format("Time Left: {0:00}:{1:00}", minutes, seconds);
    }
    public void UpdateKeyNumber()
    {
        keyNumber ++;
        keyText.text = "Keys: " + keyNumber;
    }
    public void UpdateClueNumber()
    {
        clueNumber ++;
        clueText.text = "Clues: " + clueNumber + " / 11 ";
    }
    public void UpdateHintNumber()
    {
        
            hintNumber -= 1;
            hintText.text = "Hints: " + hintNumber;
        
    }
    public void StartGame()
    {
        isGameActive = true;
        timerIsRunning = true;
        keyNumber = 0;
        clueNumber = 0;
        hintNumber = 3;
        titleScreen.gameObject.SetActive(false);
        gameInterface.gameObject.SetActive(true);
    }
    public void ResartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void GameOver()
    {
        isGameActive = false;
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        gameInterface.gameObject.SetActive(false);
    }
    public void GameSuccess()
    {
        isGameActive = false;
        gameInterface.gameObject.SetActive(false);
    }
    // Read file containing the names of the prefab mapped to their respective text 
    public Dictionary<string,string> readCSV() {
        //Initialise a null map containing the object pairing
        Dictionary<string,string> clueMap = new Dictionary<string,string>();
        StreamReader strReader = new StreamReader("Assets/Clue_Map/Clue_Map.csv");
        Debug.Log("The file is read");
        bool eof = false;
        while (!eof)
        {
            string dataString = strReader.ReadLine();
            if (dataString == null)
            {
                eof = true;
                break;
            }
            else { 
                //Since there are 2 columns in the text file
                string[] data = new string[2];
                data = dataString.Split(',');
                //Skip the header line
                if (!data[0].Equals("GameElement")) {
                    clueMap.Add(data[0], data[1]);
                    Debug.Log("Game element " + data[0] + " is added");
                }
            }

        }
        return clueMap;
    }
}
