using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherAttack : MonoBehaviour
{


    public float fireRate = 1.0f;
    private float nextFireTime = 0.0f;
    public float rotationSpeed = 5f; // Adjust this value to control the speed of the rotation

    private TowerDetection towerBox;
    private CircleCollider2D boxCollider;
    private AudioSource audioSource;
    public GameObject DesignatedEnemy;
    public GameObject Arrow;
    private GameObject FiringPos;
    public Transform bowTransform; // Assign the bow asset's Transform component to this field in the Inspector
    public Transform personTransform; // Assign the person's Transform component to this field in the Inspector
    public int hitdmg;
    

    // Start is called before the first frame update

    void Start()
    {
        towerBox = GetComponent<TowerDetection>();
        boxCollider = towerBox.Range.GetComponent<CircleCollider2D>();
        FiringPos = towerBox.FiringPos;
        audioSource=GetComponent<AudioSource>();
    }

    private void Update()
    {
        Targeting();
    }



    public void Targeting()
    {
        GameObject closestEnemy = gameObject.GetComponent<FindClosest>().FindClosestEnemy();
        if (closestEnemy != null)
        {
            // Calculate the direction to the closest enemy
            Vector3 direction = closestEnemy.transform.position - personTransform.position;

            // Calculate the rotation to face the closest enemy
            Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, direction);

            // Apply the rotation to the personTransform
            personTransform.rotation = targetRotation;
        } else
        {
            personTransform.rotation = Quaternion.Euler(0f, 0f, 90f);
        }
    }

    public void SpawnArrow()
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
                    GameObject arrow = Instantiate(Arrow, FiringPos.transform.position, Quaternion.identity);
                    audioSource.Play();
                    arrow.GetComponent<ArrowTargeting>().hitdmg = hitdmg;
                    DesignatedEnemy = closestEnemy;
                    nextFireTime = Time.time + fireRate;
                }
            }
        }
    }
}
