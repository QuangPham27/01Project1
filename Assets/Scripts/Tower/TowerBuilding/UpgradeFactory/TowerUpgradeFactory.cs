using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TowerUpgradeFactory
{
    public abstract void UpgradeTower(GameObject tower, GameObject[] knights, int towerID);
}
