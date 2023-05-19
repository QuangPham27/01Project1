using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelComplete : MonoBehaviour
{
    [SerializeField] Image[] stars;
    public int levelStars;
    public Sprite yellowStar;
    public PauseButton pauseButton;
    // Update is called once per frame
    void Update()
    {
        for (int i=0;i<levelStars;i++)
        {
            stars[i].sprite = yellowStar;
        }
    }
    public void Setup()
    {
        gameObject.SetActive(true);
        pauseButton.Pause();
    }
}
