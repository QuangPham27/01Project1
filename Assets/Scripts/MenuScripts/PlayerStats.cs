using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public GameObject gold;
    public GameObject health;
    public GameObject wave;
    public Camera camera;
    public EventSystem eventSystem;
    Player player;
    WaveSpawner waveSpawner;
    // Start is called before the first frame update
    void Start()
    {
        player = camera.GetComponent<Player>();
        waveSpawner = eventSystem.GetComponent<WaveSpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        gold.GetComponent<TextMeshProUGUI>().text = player.Gold.ToString();
        health.GetComponent<TextMeshProUGUI>().text = player.Health.ToString();
        wave.GetComponent<TextMeshProUGUI>().text = (waveSpawner.currentWaveNumber+1).ToString() + "/" + waveSpawner.waves.Count();
    }
}
