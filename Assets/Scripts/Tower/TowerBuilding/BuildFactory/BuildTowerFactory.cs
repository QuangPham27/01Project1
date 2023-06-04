using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BuildTowerFactory
{
    public abstract GameObject CreateTower(Vector3 position);
}
