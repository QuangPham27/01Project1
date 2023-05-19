using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1 : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioClip clip;
    void Start()
    {
        SoundManager.Instance.PlayMusic(clip);
    }

    // Update is called once per frame

}
