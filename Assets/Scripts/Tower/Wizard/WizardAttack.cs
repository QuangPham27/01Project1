using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardAttack : MonoBehaviour
{
    public float fireRate = 1.0f;
    private float nextFireTime = 0.0f;


    private TowerDetection towerBox;
    private CircleCollider2D boxCollider;
    private AudioSource audioSource;
    public GameObject Magic;
    private GameObject FiringPos;
    public int hitdmg;

    // Start is called before the first frame update

    void Start()
    {
        towerBox = GetComponent<TowerDetection>();
        boxCollider = towerBox.Range.GetComponent<CircleCollider2D>();
        FiringPos = towerBox.FiringPos;
        audioSource = GetComponent<AudioSource>();
    }
    public void SpawnMagic()
    {
        if (Time.time >= nextFireTime)
        {
            GameObject closestEnemy = gameObject.GetComponent<FindClosest>().FindClosestEnemy();
            if (closestEnemy != null)
            {
                Vector3 enemyPos3D = closestEnemy.transform.position;
                Vector2 enemyPos2D = new Vector2(enemyPos3D.x, enemyPos3D.y);
                Vector2 rndPointInside = boxCollider.ClosestPoint(new Vector2(enemyPos2D.x, enemyPos2D.y));

                if (rndPointInside.x == enemyPos2D.x && rndPointInside.y == enemyPos2D.y)
                {
                    GameObject magic = Instantiate(Magic, FiringPos.transform.position, Quaternion.identity);
                    audioSource.Play();
                    magic.GetComponent<ArrowTargeting>().hitdmg = hitdmg;
                    nextFireTime = Time.time + fireRate;
                }
            }
        }
    }
}
