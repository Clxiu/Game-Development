using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float rotationSpeed = 10f;
    public float minRotation = -30f;
    public float maxRotation = 30f;

    private float speed = 2.5f;
    private float turnSpeed = 50.0f;

    private float horizontalInput;
    private float forwardInput;

    // Separate rotation values
    private float pitch = 0.0f; // Rotation around X-axis
    private float yaw = 0.0f;   // Rotation around Y-axis

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");

        // Update the yaw based on horizontal input
        yaw += horizontalInput * turnSpeed * Time.deltaTime;

        // Adjust the pitch based on scroll input and clamp it
        pitch -= scrollInput * rotationSpeed;
        pitch = Mathf.Clamp(pitch, minRotation, maxRotation);

        // Apply the rotations to the camera
        transform.eulerAngles = new Vector3(pitch, yaw, 0);

        // Translate forward based on vertical input
        transform.Translate(Vector3.forward * Time.deltaTime * speed * forwardInput);
    }
}
