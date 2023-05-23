using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DieEffect : MonoBehaviour
{
    Camera camera;
    Player player;
    private SpriteRenderer rend { get; set; }
    private Vector3 objectScale { get; set; }
    private AudioController audioController { get; set; }
    public GameObject Mushrooms;

    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
        player = camera.GetComponent<Player>();
        rend = GetComponent<SpriteRenderer>();
        audioController = GetComponent<AudioController>();
        objectScale = rend.transform.localScale;
    }

    IEnumerator Disappear()
    {
        for (float i = 0.1f; i < 1.5; i += 0.3f)
        {
            rend.transform.localScale = new Vector3(objectScale.x - i, objectScale.y + i, objectScale.z);
            yield return new WaitForSeconds(0.05f);
        }
        audioController.dieAudio.Play();
        player.Gold += gameObject.GetComponent<MushroomAttack>().Gold;
        

            if (Mushrooms.name.ContainsInsensitive("brood"))
            {
                Mushrooms.GetComponent<SpawnChild>().StartCoroutine(Mushrooms.GetComponent<SpawnChild>().SpawnChildren());
            }
        Destroy(gameObject);
    }

    public void startDisappearing(GameObject Mushroom)
    {
        Mushrooms = Mushroom;
        StartCoroutine("Disappear");
    }
}
