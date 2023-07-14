using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public PauseButton pauseButton;
    public void Setup()
    {
        pauseButton.Pause();
        gameObject.SetActive(true);
    }
    public void RestartButton()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void ExitButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene((int)ScenesSwitch.LevelSelector);
    }

}
