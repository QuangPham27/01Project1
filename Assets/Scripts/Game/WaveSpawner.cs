using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Wave
{
    public string waveName;
    public int noOfEnemies;
    public GameObject[] typeOfEnemies;
    public float spawnInterval;
    public float waveTimer; // added new variable for wave timer
}

public class WaveSpawner : MonoBehaviour
{
    Queue<Wave> waveQueue;



    public Player player;
    public LevelComplete levelComplete;
    public int currentLevel;
    public Wave[] waves;
    public Transform[] spawnPoints;
    public Text waveName;

    private Wave currentWave;
    public int currentWaveNumber;
    private float nextSpawnTime;

    private bool canSpawn = true;
    private bool canAnimate = false;

    private IEnumerator waveTimerCoroutine; // added new variable for the wave timer coroutine

    private void Start()
    {
        waveQueue = new Queue<Wave>();
        for (int i = 0; i < waves.Length; i++)
        {
            waveQueue.Enqueue(waves[i]);
        }



        waveTimerCoroutine = WaveTimer(); // create the wave timer coroutine

        currentWave = waveQueue.Dequeue();


    }

    private void Update()
    {




        SpawnWave();
        GameObject[] totalEnemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (totalEnemies.Length == 0 && canAnimate)
        {
            if (currentWaveNumber + 1 != waves.Length)
            // if (currentWaveNumber + 1 != waveQueue.Count())
            {
                //waveName.text = waves[currentWaveNumber + 1].waveName;
                //animator.SetTrigger("WaveComplete");
                canAnimate = false;
                waveTimerCoroutine = WaveTimer();///Use queue to wait next wave
                StartCoroutine(waveTimerCoroutine); // start the wave timer coroutine
            }
            else
            {
                CompleteLevel();
            }
        }


    }

    void SpawnWave()
    {
        if (canSpawn && nextSpawnTime < Time.time)
        {
            GameObject randomEnemy = currentWave.typeOfEnemies[Random.Range(0, currentWave.typeOfEnemies.Length)];
            Transform randomPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Instantiate(randomEnemy, randomPoint.position, Quaternion.identity);
            currentWave.noOfEnemies--;
            nextSpawnTime = Time.time + currentWave.spawnInterval;
            if (currentWave.noOfEnemies == 0)
            {
                canSpawn = false;
                canAnimate = true;
            }
        }
    }

    IEnumerator WaveTimer()
    {
        yield return new WaitForSeconds(currentWave.waveTimer);
        if (waveQueue.Count() > 0)
        {
            currentWave = waveQueue.Dequeue();
            currentWaveNumber++;
            canSpawn = true;
            canAnimate = false;
        }
        else
        {

            CompleteLevel();

        }




    }
    void CompleteLevel()
    {
        canSpawn = false;
        canAnimate = false;
        levelComplete.Setup();
        if (player.MaxHealth == player.Health)
        {
            levelComplete.levelStars = 3;
        }
        else if (player.MaxHealth > player.Health && player.Health > player.MaxHealth / 2)
        {
            levelComplete.levelStars = 2;
        }
        else levelComplete.levelStars = 1;
        int clearedLevels = PlayerPrefs.GetInt("levelsUnlocked");//1
        int levelStarClear = levelComplete.levelStars;
        PlayerPrefs.SetInt("starClear", levelStarClear);//3
        PlayerPrefs.SetInt("currentLevel", currentLevel);//1
    }
}
