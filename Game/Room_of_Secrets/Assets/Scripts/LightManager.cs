using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

public class LightManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Light directionalLight;
    private float dayCycleDuration = 900f;

    private float rotationSpeed;
    public GameManager manager;
    void Start()
    {
        manager = GameObject.Find("Game Manager").GetComponent<GameManager>(); ;
        rotationSpeed = 360f / dayCycleDuration;
    }

    // Update is called once per frame
    void Update()
    {
        if (manager.isGameActive == true)
        {
            float angle = rotationSpeed * Time.deltaTime;
            directionalLight.transform.Rotate(new Vector3(angle, 0, 0));
        }
        
    }
}
