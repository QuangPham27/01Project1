using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class BuildScript : MonoBehaviour
{
    public Canvas canvas;
    public GameObject towerBuildMenuPrefab;
    public GameObject towerUpgradeMenuPrefab;
    public GameObject towerDestroyMenuPrefab;

    public GameObject archerTower;
    public GameObject wizardTower;
    public GameObject barrackTower;

    public GameObject archerTower2;
    public GameObject wizardTower2;
    public GameObject barrackTower2;

    public GameObject archerTower3;
    public GameObject wizardTower3;
    public GameObject barrackTower3;

    public GoldValues goldValues;

    public Camera worldCamera;
    public Player player;

    GameObject towerMenu;
    GameObject currentTower;
    int currentTowerGoldRefund;
    Vector3 towerPosition;

    private BuildTowerFactory towerFactory;
    private TowerUpgradeFactory towerUpgradeFactory;
    private TowerDestroyFactory towerDestroyFactory;
    private int towerID;
    private GameObject[] Knights;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 clickPosition = worldCamera.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D[] hits = Physics2D.RaycastAll(clickPosition, Vector2.zero);
            RaycastHit2D hit = GetHighestHitCollider(hits);
            Destroy(towerMenu);
            if (hit.collider != null)
            {
                switch (hit.collider.tag)
                {
                    case "TowerPlace":
                        SpawnMenu(hit, clickPosition, towerBuildMenuPrefab);
                        break;
                    case "archerTower":
                        towerFactory = new BuildArcherFactory() { archerTowerPrefab = archerTower };
                        BuildTower(towerFactory, towerPosition, goldValues.archer1Price);
                        break;
                    case "wizardTower":
                        towerFactory = new BuildWizardFactory() { wizardTowerPrefab = wizardTower };
                        BuildTower(towerFactory, towerPosition, goldValues.wizard1Price);
                        break;
                    case "barrackTower":
                        towerFactory = new BuildBarrackFactory() { barrackTowerPrefab = barrackTower };
                        BuildTower(towerFactory, towerPosition, goldValues.barrack1Price);
                        break;
                    case "destroy":
                        DestroyTower(currentTower);
                        break;
                    case "upgrade":
                        if (!checkEnoughGoldToUpgrade(currentTower)) break;
                        UpgradeTower(currentTower);
                        break;
                    case "ArcherLv1":
                        currentTowerGoldRefund = goldValues.archer1Price;
                        currentTower = hit.collider.gameObject;
                        SpawnMenu(hit, clickPosition, towerUpgradeMenuPrefab, goldValues.archer1Price, goldValues.towerUpgrade);
                        break;
                    case "BarrackLv1":
                        currentTowerGoldRefund = goldValues.barrack1Price;
                        currentTower = hit.collider.gameObject;
                        SpawnMenu(hit, clickPosition, towerUpgradeMenuPrefab, goldValues.barrack1Price, goldValues.towerUpgrade);
                        break;
                    case "WizardLv1":
                        currentTowerGoldRefund = goldValues.wizard1Price;
                        currentTower = hit.collider.gameObject;
                        SpawnMenu(hit, clickPosition, towerUpgradeMenuPrefab, goldValues.wizard1Price, goldValues.towerUpgrade);
                        break;
                    case "ArcherLv2":
                        currentTowerGoldRefund = goldValues.archer2Price;
                        currentTower = hit.collider.gameObject;
                        SpawnMenu(hit, clickPosition, towerUpgradeMenuPrefab, goldValues.archer2Price, goldValues.archerUpgrade);
                        break;
                    case "BarrackLv2":
                        currentTowerGoldRefund = goldValues.barrack2Price;
                        currentTower = hit.collider.gameObject;
                        SpawnMenu(hit, clickPosition, towerUpgradeMenuPrefab, goldValues.barrack2Price, goldValues.barrackUpgrade);
                        break;
                    case "WizardLv2":
                        currentTowerGoldRefund = goldValues.wizard2Price;
                        currentTower = hit.collider.gameObject;
                        SpawnMenu(hit, clickPosition, towerUpgradeMenuPrefab, goldValues.wizard2Price, goldValues.wizardUpgrade);
                        break;
                    case "ArcherLv3":
                        currentTowerGoldRefund = goldValues.archer3Price;
                        currentTower = hit.collider.gameObject;
                        SpawnMenu(hit, clickPosition, towerDestroyMenuPrefab, goldValues.archer3Price);
                        break;
                    case "BarrackLv3":
                        currentTowerGoldRefund = goldValues.barrack3Price;
                        currentTower = hit.collider.gameObject;
                        SpawnMenu(hit, clickPosition, towerDestroyMenuPrefab, goldValues.barrack3Price);
                        break;
                    case "WizardLv3":
                        currentTowerGoldRefund = goldValues.wizard3Price;
                        currentTower = hit.collider.gameObject;
                        SpawnMenu(hit, clickPosition, towerDestroyMenuPrefab, goldValues.wizard3Price);
                        break;
                }

            }
        }

    }

    private bool checkEnoughGoldToUpgrade(GameObject currentTower)
    {
        int price = 0;
        switch (currentTower.tag)
        {
            //price for upgrading lv1 towers are the same
            case "ArcherLv1":
            case "BarrackLv1":
            case "WizardLv1":
                price = goldValues.towerUpgrade;
                break;
            case "ArcherLv2":
                price = goldValues.archerUpgrade;
                break;
            case "BarrackLv2":
                price = goldValues.barrackUpgrade;
                break;
            case "WizardLv2":
                price = goldValues.wizardUpgrade;
                break;
        }
        if (player.Gold < price) return false;
        player.Gold -= price;
        return true;
    }

    private void DestroyTower(GameObject currentTower)
    {
        player.Gold += currentTowerGoldRefund * 60 / 100;
        string tower = currentTower.name;
        if (tower.ContainsInsensitive("barrack"))
        {
            towerDestroyFactory = new BarrackDestroyFactory();
            towerID = currentTower.GetComponent<BarrackSpawn>().towerID;
            Knights = GameObject.FindGameObjectsWithTag("Knight");
            towerDestroyFactory.DestroyTower(currentTower, Knights, towerID);
            return;
        }
        if (tower.ContainsInsensitive("archer"))
        {
            towerDestroyFactory = new ArcherDestroyFactory();
            towerDestroyFactory.DestroyTower(currentTower, null, 0);
            return;
        }
        if (tower.ContainsInsensitive("wizard"))
        {
            towerDestroyFactory = new WizardDestroyFactory();
            towerDestroyFactory.DestroyTower(currentTower, null, 0);
            return;
        }
    }
    private void BuildTower(BuildTowerFactory factory, Vector3 towerPosition,  int price)
    {
        if (player.Gold < price) return;
        GameObject tower = factory.CreateTower(towerPosition);
        player.Gold -= price;
    }

    private void SpawnMenu(RaycastHit2D hit, Vector2 clickPosition, GameObject menu, int destroyRefund = 0, int UpgradePrice = 0)
    {
        Bounds colliderBounds = hit.collider.bounds;
        Vector3 center = colliderBounds.center;
        Vector3 extents = colliderBounds.extents;

        // Compute the offset from the center based on the click position
        Vector3 offset = new Vector3(
            Mathf.Sign(clickPosition.x - center.x) * extents.x * hit.normal.x,
            Mathf.Sign(clickPosition.y - center.y) * extents.y * hit.normal.y,
            0f);

        // Spawn the tower menu at the center plus the offset
        towerPosition = center + offset;
        towerMenu = Instantiate(menu, towerPosition, Quaternion.identity, canvas.transform);
        towerMenu.transform.localScale = new Vector3((float)0.025, (float)0.025, (float)0.025);
        switch(towerMenu.gameObject.name) 
        {
            case "TowerBuildMenu(Clone)":
                Transform archerChildTransform = towerMenu.transform.Find("archer");
                Transform wizardChildTransform = towerMenu.transform.Find("wizard");
                Transform barrackChildTransform = towerMenu.transform.Find("barrack");
                GameObject archerChild = archerChildTransform.gameObject;
                GameObject wizardChild = wizardChildTransform.gameObject;
                GameObject barrackChild = barrackChildTransform.gameObject;
                archerChild.GetComponentInChildren<TextMeshProUGUI>().text = goldValues.archer1Price.ToString() + " Gold";
                wizardChild.GetComponentInChildren<TextMeshProUGUI>().text = goldValues.wizard1Price.ToString() + " Gold";
                barrackChild.GetComponentInChildren<TextMeshProUGUI>().text = goldValues.barrack1Price.ToString() + " Gold";
                break;
            case "TowerUpgrade(Clone)":
                Transform upgradeChildTransform = towerMenu.transform.Find("Upgrade");
                Transform destroyChildTransform = towerMenu.transform.Find("Destroy");
                GameObject upgradeChild = upgradeChildTransform.gameObject;
                GameObject destroyChild = destroyChildTransform.gameObject;
                upgradeChild.GetComponentInChildren<TextMeshProUGUI>().text = UpgradePrice.ToString() + " Gold";
                destroyChild.GetComponentInChildren<TextMeshProUGUI>().text = (destroyRefund*60/100).ToString() + " Gold";
                break;
            case "TowerDestroy(Clone)":
                Transform _destroyChildTransform = towerMenu.transform.Find("Destroy");
                GameObject _destroyChild = _destroyChildTransform.gameObject;
                _destroyChild.GetComponentInChildren<TextMeshProUGUI>().text = (destroyRefund * 60 / 100).ToString() + " Gold";
                break;
        }
    }

    private void UpgradeTower(GameObject currentTower)
    {
        switch (currentTower.tag)
        {
            case "ArcherLv1":
                towerUpgradeFactory = new ArcherUpgradeFactory() { upgradedArcherTowerPrefab = archerTower2 };
                towerUpgradeFactory.UpgradeTower(currentTower,null,0);
                break;
            case "BarrackLv1":
                towerUpgradeFactory = new BarrackUpgradeFactory() { upgradedBarrackTowerPrefab = barrackTower2 };
                towerID = currentTower.GetComponent<BarrackSpawn>().towerID;
                Knights = GameObject.FindGameObjectsWithTag("Knight");
                towerUpgradeFactory.UpgradeTower(currentTower, Knights, towerID);
                break;
            case "WizardLv1":
                towerUpgradeFactory = new WizardUpgradeFactory() { upgradedWizardTowerPrefab = wizardTower2 };
                towerUpgradeFactory.UpgradeTower(currentTower, null, 0);
                break;
            case "ArcherLv2":
                towerUpgradeFactory = new ArcherUpgradeFactory() { upgradedArcherTowerPrefab = archerTower3 };
                towerUpgradeFactory.UpgradeTower(currentTower, null, 0);
                break;
            case "BarrackLv2":
                towerUpgradeFactory = new BarrackUpgradeFactory() { upgradedBarrackTowerPrefab = barrackTower3 };
                towerID = currentTower.GetComponent<BarrackSpawn>().towerID;
                Knights = GameObject.FindGameObjectsWithTag("Knight");
                towerUpgradeFactory.UpgradeTower(currentTower, Knights, towerID);
                break;
            case "WizardLv2":
                towerUpgradeFactory = new WizardUpgradeFactory() { upgradedWizardTowerPrefab = wizardTower3 };
                towerUpgradeFactory.UpgradeTower(currentTower, null, 0);
                break;
        }
    }

    private RaycastHit2D GetHighestHitCollider(RaycastHit2D[] hits)
    {
        RaycastHit2D highestHit = new RaycastHit2D();
        int highestLayerSortingOrder = int.MinValue;
        foreach (RaycastHit2D hit in hits)
        {
            // Check if the hit collider is on a higher sorting layer than the current highest
            int hitSortingOrder = hit.collider.GetComponent<Renderer>().sortingOrder;
            if (hitSortingOrder > highestLayerSortingOrder)
            {
                highestLayerSortingOrder = hitSortingOrder;
                highestHit = hit;
            }
        }
        return highestHit;
    }

    public void DisableScript()
    {
        GameObject camera = Camera.main.gameObject;
        BuildScript script = Camera.main.GetComponent<BuildScript>();
        script.enabled = false;
    }
    public void EnableScript()
    {
        GameObject camera = Camera.main.gameObject;
        BuildScript script = Camera.main.GetComponent<BuildScript>();
        script.enabled = true;
    }
}
