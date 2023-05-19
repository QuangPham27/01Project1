using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldValues : MonoBehaviour
{
    public int towerUpgrade = 40;
    public int archer1Price = 40;
    public int wizard1Price = 50;
    public int barrack1Price = 30;
    //do not set by hand
    public int wizardUpgrade;
    public int archerUpgrade;
    public int barrackUpgrade;
    public int archer2Price;
    public int archer3Price;
    public int wizard2Price;
    public int wizard3Price;
    public int barrack2Price;
    public int barrack3Price;
    public void Start()
    {
        //refund value of lv2 towers
        archer2Price = archer1Price + towerUpgrade;
        wizard2Price = wizard1Price + towerUpgrade;
        barrack2Price = barrack1Price + towerUpgrade;

        //update price of lv2 towers
        wizardUpgrade = wizard2Price + towerUpgrade;
        archerUpgrade = archer2Price + towerUpgrade;
        barrackUpgrade = barrack2Price + towerUpgrade;

        //refund value of lv3 towers
        archer3Price = archerUpgrade + archer2Price;
        wizard3Price = wizardUpgrade + wizard2Price;
        barrack3Price = barrackUpgrade + barrack2Price;
    }
}
