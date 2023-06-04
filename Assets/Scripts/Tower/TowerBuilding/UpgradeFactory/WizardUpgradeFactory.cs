using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardUpgradeFactory : TowerUpgradeFactory
{
    public GameObject upgradedWizardTowerPrefab;
    public override void UpgradeTower(GameObject tower, GameObject[] knights, int towerID)
    {
        GameObject upgradedTower = UnityEngine.GameObject.Instantiate(upgradedWizardTowerPrefab, tower.transform.position, tower.transform.rotation);
        upgradedTower.transform.localScale = new Vector3((float)1.25, (float)1.25, (float)1.25);
        UnityEngine.GameObject.Destroy(tower);
    }
}
