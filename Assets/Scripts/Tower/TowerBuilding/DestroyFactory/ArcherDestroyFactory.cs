using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherDestroyFactory : TowerDestroyFactory
{
    public override void DestroyTower(GameObject tower, GameObject[] knights, int towerID)
    {
        UnityEngine.GameObject.Destroy(tower);
    }
}
