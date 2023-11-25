using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LockerManager : MonoBehaviour
{
    // Start is called before the first frame update
    private string correctWardrobe = "3527";
    public InteractableObject bigCloset;
    public GameObject wardrobePasswordInput;
    public GameObject submitButton;
    public GameObject openDiary;
    public GameObject diary;
    public TextMeshProUGUI feedback;
    public bool opened;
    public PlayerController playerController;
    public GameManager gameManager;
    void Start()
    {
        opened = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void CheckPassword()
    {
        string entered = wardrobePasswordInput.GetComponent<TMP_InputField>().text;
        if (entered == correctWardrobe)
        {
            opened = true;
            UnlockLocker();
        }
        else
        {
            feedback.text = "Incorrect password. Try again.";
        }
    }
    public void UnlockLocker()
    {
        feedback.text = "Locker unlocked!";
        Debug.Log("Locker unlocked!");
        wardrobePasswordInput.SetActive(false);
        submitButton.SetActive(false);
        diary.SetActive(true); 
        openDiary.SetActive(true);
    }
}
