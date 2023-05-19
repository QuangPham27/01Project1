using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeemlessBackgroundMusic : MonoBehaviour
{
    // Start is called before the first frame update
    private static SeemlessBackgroundMusic instance = null;
    public static SeemlessBackgroundMusic Instance
    {
        get { return instance; }
    }

    void Start()
    {
        // play music
        gameObject.GetComponent<AudioSource>().Play();
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);

    }
}
