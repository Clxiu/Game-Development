using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateStatue : MonoBehaviour
{
    // Start is called before the first frame update
    public float RotationSpeed = 10.0f;
    void Start()
    {
        
    }

    //Rotate the statue with `RotationSpeed`
    void Update()
    {
        transform.Rotate(Vector3.up,RotationSpeed*Time.deltaTime);
    }
}
