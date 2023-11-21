using HighlightingSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class Compasscontrol : MonoBehaviour
{
    public Transform thepos;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.LookAt(thepos.position);
        gameObject.transform.localEulerAngles =
            new Vector3(-gameObject.transform.localEulerAngles.x, gameObject.transform.localEulerAngles.y, gameObject.transform.localEulerAngles.z);
        if (gameObject.transform.localEulerAngles.y > 0 && gameObject.transform.localEulerAngles.y < 40)
        {
            gameObject.transform.GetChild(0).GetComponent<Highlighter>().enabled = true;
        }
        else if (gameObject.transform.localEulerAngles.y > 320 && gameObject.transform.localEulerAngles.y < 360)
        {
            gameObject.transform.GetChild(0).GetComponent<Highlighter>().enabled = true;
        }
        else
        {
            gameObject.transform.GetChild(0).GetComponent<Highlighter>().enabled = false;
        }
    }
}
