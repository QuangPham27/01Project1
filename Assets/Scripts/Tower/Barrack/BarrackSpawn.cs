using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrackSpawn : MonoBehaviour
{


    private TowerDetection towerBox;
    private CircleCollider2D boxCollider;


    public GameObject Knight;
    private GameObject spawnedKnight;
    private GameObject Spawnpos;

    public int towerID;
    private int knightCount = 0;

    private Dictionary<int, GameObject> spawnedKnights = new Dictionary<int, GameObject>();


    void Start()
    {
        towerID = new System.Random().Next(1, 1001);
        towerBox = GetComponent<TowerDetection>();
        boxCollider = towerBox.Range.GetComponent<CircleCollider2D>();
        Spawnpos = towerBox.FiringPos;
    }
    public void SpawnKnight()
    {
            GameObject closestEnemyPath = gameObject.GetComponent<FindClosest>().FindClosestPath();
            if (closestEnemyPath != null)
            {
                Vector3 enemyPos3D = closestEnemyPath.transform.position;
                Vector2 enemyPos2D = new Vector2(enemyPos3D.x, enemyPos3D.y);
                Vector2 rndPointInside = boxCollider.ClosestPoint(new Vector2(enemyPos2D.x, enemyPos2D.y));

                if (rndPointInside.x == enemyPos2D.x && rndPointInside.y == enemyPos2D.y)
                {
                    spawnedKnight = Instantiate(Knight, Spawnpos.transform.position, Quaternion.identity);
                    spawnedKnight.name = "Knight" + knightCount;
                    spawnedKnight.tag = "Knight";
                    spawnedKnight.GetComponent<KnightMovement>().closestEnemyPath = closestEnemyPath;
                    spawnedKnight.GetComponent<KnightMovement>().towerID = towerID;
                    knightCount++;
                }
            }
    }

    public void OnKnightDeath(KnightDeathMessage msg)
    {
        // If the message tower ID matches this tower's ID, respawn the knight
        if (msg.towerID == towerID)
        {
            Debug.Log($"Respawning knight for tower {msg.towerID}");
            StartCoroutine(SpawnKnightWithDelay(5f));
        }
    }

    private IEnumerator SpawnKnightWithDelay(float delay)
    {
        // Wait for the specified delay time
        yield return new WaitForSeconds(delay);

        // Spawn a new knight
        SpawnKnight();
    }
}
