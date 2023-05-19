using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public void playButton()
    {
        SceneManager.LoadScene((int)ScenesSwitch.LevelSelector);
    }
    public void guideButton() 
    {
        SceneManager.LoadScene((int)ScenesSwitch.GuideScene1);
    }

    public void guide2Button()
    {
        SceneManager.LoadScene((int)ScenesSwitch.GuideScene2);
    }
    public void guide3Button()
    {
        SceneManager.LoadScene((int)ScenesSwitch.GuideScene3);
    }
    public void guide4Button()
    {
        SceneManager.LoadScene((int)ScenesSwitch.GuideScene4);
    }
    public void guide5Button()
    {
        SceneManager.LoadScene((int)ScenesSwitch.GuideScene5);
    }
    public void guide6Button()
    {
        SceneManager.LoadScene((int)ScenesSwitch.GuideScene6);
    }
    public void guide7Button()
    {
        SceneManager.LoadScene((int)ScenesSwitch.GuideScene7);
    }
    public void optionsButton()
    {
        SceneManager.LoadScene((int)ScenesSwitch.Options);
    }

    public void backButton()
    {
        SceneManager.LoadScene((int)ScenesSwitch.MainMenu);
    }

    public void level1Button()
    {
        SceneManager.LoadScene((int)ScenesSwitch.Level1);
    }
    public void level2Button()
    {
        SceneManager.LoadScene((int)ScenesSwitch.Level2);
    }
    public void level3Button()
    {
        SceneManager.LoadScene((int)ScenesSwitch.Level3);
    }
    public void level4Button()
    {
        SceneManager.LoadScene((int)ScenesSwitch.Level4);
    }
}
