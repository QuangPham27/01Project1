using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildWizardFactory : BuildTowerFactory
{
    public GameObject wizardTowerPrefab;
    private Vector3 wizardTowerOffset = new Vector3(0, (float)0.25, 0);
    public override GameObject CreateTower(Vector3 position)
    {
        GameObject tower = UnityEngine.GameObject.Instantiate(wizardTowerPrefab, position + wizardTowerOffset, Quaternion.identity);
        tower.transform.localScale = new Vector3((float)1.25, (float)1.25, (float)1.25);
        return tower;
    }
}
