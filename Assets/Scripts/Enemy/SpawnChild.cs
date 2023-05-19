using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnChild : MonoBehaviour
{
    [SerializeField]
    int spawnQuantity;
    [SerializeField]
    GameObject objectToSpawn;

    HealthSystem healthSystem;

    // Start is called before the first frame update
    void Start()
    {
        healthSystem = GetComponent<HealthSystem>();
    }

    // Update is called once per frame

    public IEnumerator SpawnChildren()
    {
        for (int i = 0; i < spawnQuantity; i++)
        {
            // Find a valid position on the NavMesh near the parent object's position
            NavMeshHit hit;
            if (NavMesh.SamplePosition(transform.position, out hit, 2.0f, NavMesh.AllAreas))
            {
                GameObject mush = Instantiate(objectToSpawn, hit.position, Quaternion.identity) as GameObject;
                mush.name = "Mushy" + i;

                // Set the parent object as the parent of the spawned enemy
                mush.transform.SetParent(transform.parent);

                yield return new WaitForSeconds(1.0f); // wait for 1 second before spawning the next child
            }
        }
    }

}
