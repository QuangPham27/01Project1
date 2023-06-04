using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BarrackDestroyFactory : TowerDestroyFactory
{
    public override void DestroyTower(GameObject tower, GameObject[] knights, int towerID)
    {
        if (towerID > 0)
        {
            foreach (GameObject knight in knights)
            {
                if (knight.GetComponent<KnightMovement>().towerID == towerID)
                {
                    if (knight.GetComponent<KnightMovement>().engagedTarget != null)
                    {
                        knight.GetComponent<KnightMovement>().engagedTarget.GetComponent<NavMeshAgent>().isStopped = false;
                        knight.GetComponent<KnightMovement>().engagedTarget.GetComponent<MushroomAttack>().inCombat = false;
                        knight.GetComponent<KnightMovement>().engagedTarget.GetComponent<MushroomAttack>().attacker = null;
                    }
                    UnityEngine.GameObject.Destroy(knight.gameObject);
                }
            }
        }
        UnityEngine.GameObject.Destroy(tower);
    }
}
