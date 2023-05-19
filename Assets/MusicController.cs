using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public static bool isMuted = false;

    private void Start()
    {
        SeemlessBackgroundMusic.Instance.gameObject.GetComponent<AudioSource>().Play();
    }

    public void ToggleMusic()
    {
        isMuted = !isMuted;
        SeemlessBackgroundMusic.Instance.gameObject.GetComponent<AudioSource>().mute = isMuted;

        Debug.Log("Music is muted: " + isMuted);
    }

    // mute object when game is paused

}
