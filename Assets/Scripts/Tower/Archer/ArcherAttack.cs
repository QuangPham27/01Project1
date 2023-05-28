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
        try
        {
            towerBox = GetComponent<TowerDetection>();
            boxCollider = towerBox.Range.GetComponent<CircleCollider2D>();
            FiringPos = towerBox.FiringPos;
            audioSource = GetComponent<AudioSource>();
        }
        catch (Exception e)
        {
            Debug.LogError($"Error in ArcherAttack.Start(): {e}");
            // Handle the exception (e.g., display an error message)
        }
    }


    private void Update()
    {
        try
        {
            Targeting();
        }
        catch (Exception e)
        {
            Debug.LogError($"Error in ArcherAttack.Update(): {e}");
            // Handle the exception (e.g., display an error message)
        }
    }



    public void Targeting()
    {
        try
        {
            GameObject closestEnemy = gameObject.GetComponent<FindClosest>().FindClosestEnemy();
            if (closestEnemy != null)
            {
                Vector3 direction = closestEnemy.transform.position - personTransform.position;
                Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, direction);
                personTransform.rotation = targetRotation;
            }
            else
            {
                personTransform.rotation = Quaternion.Euler(0f, 0f, 90f);
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"Error in ArcherAttack.Targeting(): {e}");
            // Handle the exception (e.g., display an error message)
        }
    }


    public void SpawnArrow()
    {
        try
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
        catch (Exception e)
        {
            Debug.LogError($"Error in ArcherAttack.SpawnArrow(): {e}");
            // Handle the exception (e.g., display an error message)
        }
    }
}
