using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public int MaxHealth = 5;
    public int Health = 5;
    public int Gold = 100;
    //public GameObject gameOver;
    //public Canvas canvas;
    public GameOver gameOver;

    void Start()
    {
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Health <=0)
        {
            Time.timeScale = 0;
            gameOver.Setup();
        }
    }

    public void LoseHealth()
    {
        Health--;
    }
}
