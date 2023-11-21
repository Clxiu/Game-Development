using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    // Start is called before the first frame update
    private GameManager gameManager;
    private Button button;
    void Start()
    {
        button = GetComponent<Button>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        button.onClick.AddListener(Setup);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Setup()
    {
        Debug.Log(gameObject.name + " was clicked.");
        gameManager.StartGame();
    }
}
