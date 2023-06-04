using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BarrackUpgradeFactory : TowerUpgradeFactory
{
    public GameObject upgradedBarrackTowerPrefab;
    public override void UpgradeTower(GameObject tower, GameObject[] Knights, int towerID)
    {
        // Destroy all knights with the matching tower ID
        foreach (GameObject knight in Knights)
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
        GameObject upgradedTower = UnityEngine.GameObject.Instantiate(upgradedBarrackTowerPrefab, tower.transform.position, tower.transform.rotation);
        upgradedTower.transform.localScale = new Vector3((float)1.25, (float)1.25, (float)1.25);
        UnityEngine.GameObject.Destroy(tower);
    }
}
