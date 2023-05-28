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
        try
        {
            towerID = new System.Random().Next(1, 1001);
            towerBox = GetComponent<TowerDetection>();
            boxCollider = towerBox.Range.GetComponent<CircleCollider2D>();
            Spawnpos = towerBox.FiringPos;
        }
        catch (Exception e)
        {
            Debug.LogError($"Error in BarrackSpawn.Start(): {e}");
            // Handle the exception (e.g., display an error message)
        }
    }

    public void SpawnKnight()
    {
        try
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
        catch (Exception e)
        {
            Debug.LogError($"Error in BarrackSpawn.SpawnKnight(): {e}");
            // Handle the exception (e.g., display an error message)
        }
    }


    public void OnKnightDeath(KnightDeathMessage msg)
    {
        try
        {
            if (msg.towerID == towerID)
            {
                Debug.Log($"Respawning knight for tower {msg.towerID}");
                StartCoroutine(SpawnKnightWithDelay(5f));
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"Error in BarrackSpawn.OnKnightDeath(): {e}");
            // Handle the exception (e.g., display an error message)
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
