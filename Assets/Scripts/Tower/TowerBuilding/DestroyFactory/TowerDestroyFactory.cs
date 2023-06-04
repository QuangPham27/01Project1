using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TowerDestroyFactory
{
    public abstract void DestroyTower(GameObject tower, GameObject[] knights, int towerID);
}
