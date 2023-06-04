using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildArcherFactory : BuildTowerFactory
{
    public GameObject archerTowerPrefab;
    private Vector3 archerTowerOffset = new Vector3(0, (float)0.5, 0);
    public override GameObject CreateTower(Vector3 position)
    {
        GameObject tower = UnityEngine.GameObject.Instantiate(archerTowerPrefab, position + archerTowerOffset, Quaternion.identity);
        tower.transform.localScale = new Vector3((float)1.25, (float)1.25, (float)1.25);
        return tower;
    }
}
