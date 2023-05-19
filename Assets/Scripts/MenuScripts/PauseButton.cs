using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class PauseButton : MonoBehaviour
{
    public GameObject camera;
    BuildScript buildScript;
    CameraZoom zoomScript;
    ClicknDrag dragScript;
    public void Start()
    {
        buildScript = camera.GetComponent<BuildScript>();
        zoomScript = camera.GetComponent<CameraZoom>();
        dragScript = camera.GetComponent<ClicknDrag>();
    }
    public void Pause()
    {
        DisableAllScripts();
        gameObject.SetActive(false);
    }
    public void Resume()
    {
        EnableAllScripts();
        gameObject.SetActive(true);
    }
    public void DisableAllScripts()
    {
        buildScript.DisableScript();
        zoomScript.DisableScript();
        dragScript.DisableScript();
    }

    public void EnableAllScripts()
    {
        buildScript.EnableScript();
        zoomScript.EnableScript();
        dragScript.EnableScript();
    }

}
