using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    [SerializeField] private float zoomSpeed = 5f; // Zoom speed
    [SerializeField] private float minZoom = 1f; // Minimum zoom level
    [SerializeField] private float maxZoom = 10f; // Maximum zoom level

    private Camera cam;

    private void Start()
    {
        // Get the camera component attached to this game object
        cam = GetComponent<Camera>();
    }

    private void Update()
    {
        // Get the scroll wheel input
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        // If the scroll wheel has been moved
        if (scroll != 0f)
        {
            // Calculate the new orthographic size based on the zoom speed and scroll input
            float newSize = cam.orthographicSize - (scroll * zoomSpeed);

            // Clamp the new size between the min and max zoom levels
            newSize = Mathf.Clamp(newSize, minZoom, maxZoom);

            // Set the new orthographic size of the camera
            cam.orthographicSize = newSize;
        }
    }
    public void DisableScript()
    {
        GameObject camera = Camera.main.gameObject;
        CameraZoom script = Camera.main.GetComponent<CameraZoom>();
        script.enabled = false;
    }
    public void EnableScript()
    {
        GameObject camera = Camera.main.gameObject;
        CameraZoom script = Camera.main.GetComponent<CameraZoom>();
        script.enabled = true;
    }
}
