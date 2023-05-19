using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{

    public static bool isMuted = false;

    private void Start()
    {
        // still play music if the screen name is not "Options"
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "Options")
        {
            SeemlessBackgroundMusic.Instance.gameObject.GetComponent<AudioSource>().Play();
        }
        else
        {
            SeemlessBackgroundMusic.Instance.gameObject.GetComponent<AudioSource>().Pause();
        }

    }

    private void OnDisable()
    {
        SeemlessBackgroundMusic.Instance.gameObject.GetComponent<AudioSource>().Play();
    }

    public void ToggleMusic()
    {
        Debug.Log("Music is muted: " + isMuted);
        isMuted = !isMuted;
        SeemlessBackgroundMusic.Instance.gameObject.GetComponent<AudioSource>().mute = isMuted;

        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name != "Options" && UnityEngine.SceneManagement.SceneManager.GetActiveScene().name != "Main Menu")
        {
            gameObject.GetComponent<AudioSource>().mute = isMuted;
        }

    }

    // mute object when game is paused


}
