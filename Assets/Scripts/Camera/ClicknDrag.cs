using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClicknDrag : MonoBehaviour
{
    Vector2 mouseClickPos;
    Vector2 mouseCurrentPos;
    bool panning = false;
    float minX, maxX, minY, maxY;
    float maxOrthoSize = 10f; // Maximum orthographic size allowed by the CameraZoom script

    private void Start()
    {
        // Calculate the minimum and maximum coordinates for the camera based on the maximum orthographic size allowed
        var aspectRatio = Screen.width / Screen.height;
        var cameraSize = maxOrthoSize;
        minX = Camera.main.transform.position.x - cameraSize * aspectRatio;
        maxX = Camera.main.transform.position.x + cameraSize * aspectRatio;
        minY = Camera.main.transform.position.y - cameraSize;
        maxY = Camera.main.transform.position.y + cameraSize;
    }

    private void Update()
    {
        // When LMB clicked get mouse click position and set panning to true
        if (Input.GetKeyDown(KeyCode.Mouse0) && !panning)
        {
            mouseClickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            panning = true;
        }
        // If LMB is already clicked, move the camera following the mouse position update
        if (panning)
        {
            mouseCurrentPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var distance = mouseCurrentPos - mouseClickPos;

            // Calculate the new position of the camera, making sure it stays within the limits
            var newPosition = transform.position + new Vector3(-distance.x, -distance.y, 0);
            newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
            newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);
            transform.position = newPosition;
        }

        // If LMB is released, stop moving the camera
        if (Input.GetKeyUp(KeyCode.Mouse0))
            panning = false;
    }
    public void DisableScript()
    {
        GameObject camera = Camera.main.gameObject;
        ClicknDrag script = Camera.main.GetComponent<ClicknDrag>();
        script.enabled = false;
        panning = false;
    }
    public void EnableScript()
    {
        GameObject camera = Camera.main.gameObject;
        ClicknDrag script = Camera.main.GetComponent<ClicknDrag>();
        script.enabled = true;
    }
}