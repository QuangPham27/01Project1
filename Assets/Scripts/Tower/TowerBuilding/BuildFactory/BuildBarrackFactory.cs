using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildBarrackFactory : BuildTowerFactory
{
    public GameObject barrackTowerPrefab;
    private Vector3 barrackTowerOffset = new Vector3(0, (float)0.5, 0);
    public override GameObject CreateTower(Vector3 position)
    {
        GameObject tower = UnityEngine.GameObject.Instantiate(barrackTowerPrefab, position + barrackTowerOffset, Quaternion.identity);
        tower.transform.localScale = new Vector3((float)1.25, (float)1.25, (float)1.25);
        return tower;
    }
}
