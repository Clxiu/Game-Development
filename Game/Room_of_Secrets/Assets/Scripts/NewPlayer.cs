using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewPlayer : MonoBehaviour
{
    public GameObject the_text,the_image;
    public bool Isbed = false,Iswall=false;
    public GameObject thecompass1, thecompass2;

    private GameObject thecol;

    public GameObject The_game;

    public InputField[] inputs;

    public Animator Ani;
    public GameObject thepos;
    public GameObject WeaponsFound;
    void Start()
    {
        
    }
    public void CheckInput()
    {
        string thestr="";
        for (int i = 0; i < inputs.Length; i++)
        {
            thestr += inputs[i].text;
        }

        if (thestr == "AXEPOISON")
        {
            Ani.enabled = true;
            WeaponsFound.SetActive(true);
            Iswall = false;
            thepos.name = "finish";
            thepos.tag = "finish";
            the_image.SetActive(false);
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name == "BedDesk")
        {
            Isbed = true;
            thecol=other.gameObject;
        }
              
        if (other.gameObject.name == "pos")
        {
            Iswall = true;
            thecol = other.gameObject;
        }
        
    }
    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.name == "BedDesk")
        {
            Isbed = false;
            thecol = null;
        }
        if (other.gameObject.name == "pos")
        {
            Iswall = false;
            thecol = null;
        }
    }
    void Update()
    {
        CheckInput();
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (Isbed&& thecompass1.activeInHierarchy)
            {
                thecompass1.SetActive(false);
                thecompass2.SetActive(true);
                thecol.name = "finish";
                thecol.tag = "finish";
                Isbed = false;
                the_text.SetActive(false);
                the_image.SetActive(true);
            }
            if (Iswall&& thecompass2.activeInHierarchy)
            {
                the_text.SetActive(false);
                The_game.SetActive(true);
            }

        }
    }
}
