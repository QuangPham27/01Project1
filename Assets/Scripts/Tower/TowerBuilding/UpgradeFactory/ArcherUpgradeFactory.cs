using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherUpgradeFactory : TowerUpgradeFactory 
{
    public GameObject upgradedArcherTowerPrefab;
    public override void UpgradeTower(GameObject tower, GameObject[] knights, int towerID)
    {
        GameObject upgradedTower = UnityEngine.GameObject.Instantiate(upgradedArcherTowerPrefab, tower.transform.position, Quaternion.identity);
        upgradedTower.transform.localScale = new Vector3((float)1.25, (float)1.25, (float)1.25);
        UnityEngine.GameObject.Destroy(tower);
    }
}
