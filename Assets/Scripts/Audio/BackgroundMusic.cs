using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{

    public static bool isMuted = false;

    private void Start()
    {
        try
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
        } catch (Exception ex)
        {
            Debug.Log(ex.Message);
        }
    }

    private void OnDisable()
    {
        try
        {
            SeemlessBackgroundMusic.Instance.gameObject.GetComponent<AudioSource>().Play();
        } catch (Exception ex)
        {
            Debug.Log(ex.Message);
        }
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
