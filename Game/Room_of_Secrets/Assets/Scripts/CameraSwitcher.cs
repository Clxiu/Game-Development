using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    // Start is called before the first frame update
    public Camera firstPersonCamera;
    public Camera mapCamera;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            SwitchCamera();
        }
    }
    void SwitchCamera()
    {
        if (firstPersonCamera.enabled)
        {
            firstPersonCamera.enabled = false;
            mapCamera.enabled = true;
        }
        else
        {
            firstPersonCamera.enabled = true;
            mapCamera.enabled = false;
        }
    }
}
