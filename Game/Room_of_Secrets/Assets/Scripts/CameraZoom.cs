using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public Transform target; // the Glass object
    public float zoomSpeed = 0.5f;
    public float targetZoom = 5.0f;
    private Camera cam;
    private float initialZoom;

    private void Start()
    {
        cam = GetComponent<Camera>();
        initialZoom = cam.fieldOfView;
    }

    public void ZoomIn()
    {
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, targetZoom, zoomSpeed * Time.deltaTime);
        transform.LookAt(target);
    }

    public void ZoomOut()
    {
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, initialZoom, zoomSpeed * Time.deltaTime);
    }
}

