using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEditor.Progress;

public class PauseScript : MonoBehaviour
{
    public PauseButton pauseButton;
    public void Setup()
    {
        gameObject.SetActive(true);
        Time.timeScale = 0;
        pauseButton.Pause();
    }
    public void RestartButton()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void ExitButton()
    {
        SceneManager.LoadScene((int)ScenesSwitch.LevelSelector);
    }
    public void ResumeButton()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1;
        pauseButton.Resume();
    }
    public void OptionsButton()
    {
        gameObject.SetActive(false);
    }
    public void On()
    {
        gameObject.SetActive(true);
    }
}
