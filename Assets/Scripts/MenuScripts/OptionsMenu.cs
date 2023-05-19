using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsMenu : MonoBehaviour
{
    public PauseScript pause;
    public void On()
    {
        gameObject.SetActive(true);
    }
    public void Back()
    {
        pause.On();
        gameObject.SetActive(false);
    }
    
}
