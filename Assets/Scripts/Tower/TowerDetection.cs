using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;
using static UnityEngine.GraphicsBuffer;

public class TowerDetection : MonoBehaviour
{
    
    private bool isRangeVisible = false;
    private string tower;
    private bool firstspawn = false;
    private int count = 0;
    private float delay = 5.0f;

    private Renderer rend;
    
    public GameObject Range;
    public GameObject FiringPos;



    void Start()
    {
        try
        {
            rend = Range.GetComponent<Renderer>();
            Color color = rend.material.color;
            color.a = 0.0f;
            rend.material.color = color;
            tower = gameObject.name;
        }
        catch (Exception e)
        {
            Debug.LogError($"Error in TowerDetection.Start(): {e}");
            // Handle the exception (e.g., display an error message)
        }
    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            if (tower.ContainsInsensitive("archer"))
            {
                gameObject.GetComponent<ArcherAttack>().SpawnArrow();
            }
            else if (tower.ContainsInsensitive("wizard"))
            {
                gameObject.GetComponent<WizardAttack>().SpawnMagic();
            }
            else if (tower.ContainsInsensitive("barrack") && count < 3)
            {
                if (firstspawn == false)
                {
                    gameObject.GetComponent<BarrackSpawn>().SpawnKnight();
                    firstspawn = true;
                    count++;
                }
                else
                {
                    StartCoroutine(SpawnDelay(delay));
                    count++;
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"Error in TowerDetection.Update(): {e}");
            // Handle the exception (e.g., display an error message)
        }
    }

    private IEnumerator SpawnDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        gameObject.GetComponent<BarrackSpawn>().SpawnKnight();

    }


    public class KnightSpawnData
    {
        public int towerID;
        public GameObject knightObject;
        public float respawnTime;
        public bool isDead;
    }
}
