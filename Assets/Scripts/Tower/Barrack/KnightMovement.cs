using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;


public class KnightDeathMessage : UnityEngine.Object
{
    public int towerID;
}


public class KnightMovement : MonoBehaviour
{
    public float speed;
    public bool inCombat = false;
    public float attackSpeed = 2.0f;
    private float nextAttackTime = 0.0f;
    public int attackDmg;
    private float chaseTimer = 0f;
    public float moveSpeed = 5f;
    public float guardRadius = 1f; // the radius around the guard point to move within

    private CircleCollider2D detectRange;
    private Rigidbody2D rb;
    private AudioSource audioSource;
    private Detection range;
    private BarrackSpawn BarrackSpawn;
    public int towerID;
    private HealthSystem healthSystem;

    public GameObject closestEnemyPath;
    private GameObject closestEnemy;
    public GameObject engagedTarget;
    private Vector3 guardPoint; // the point to guard around

    // Start is called before the first frame update
    void Start()
    {
        BarrackSpawn = GetComponent<BarrackSpawn>();
        range = GetComponent<Detection>();
        detectRange = range.Range.GetComponent<CircleCollider2D>();

        GetComponent<HealthSystem>().Knight = gameObject;
        rb = this.GetComponent<Rigidbody2D>();
        guardPoint = GetRandomPointInRadius(closestEnemyPath.transform.position, guardRadius);
        audioSource = GetComponent<AudioSource>();
    }

    void attack()
    {
        if (Time.time >= nextAttackTime)
            if (closestEnemy != null)
            {
                healthSystem.Hit(attackDmg);
                audioSource.Play();
                nextAttackTime = Time.time + attackSpeed;
            }
    }

    public void Die()
    {
        // Spawn a particle effect or play a sound effect
        // ...

        // Send a KnightDeathMessage to the BarrackSpawn script
        KnightDeathMessage msg = new KnightDeathMessage();
        msg.towerID = this.towerID; // add the tower ID to the message
        GameObject.FindObjectOfType<BarrackSpawn>().SendMessage("OnKnightDeath", msg);
    }

    void moveToTarget(GameObject target)
    {
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        if (transform.position == target.transform.position)
        {

            if (target.GetComponent<HealthSystem>().health > 0)
            {
                attack();
            }

        }
    }

    private void FixedUpdate()
    {
        if (inCombat == false)
        {
            closestEnemy = gameObject.GetComponent<FindClosest>().FindClosestEnemy();
            if (closestEnemy != null)
            {
                Vector3 enemyPos3D = closestEnemy.transform.position;
                Vector2 enemyPos2D = new Vector2(enemyPos3D.x, enemyPos3D.y);
                Vector2 rndPointInside = detectRange.ClosestPoint(new Vector2(enemyPos2D.x, enemyPos2D.y));
                if (rndPointInside.x == enemyPos2D.x && rndPointInside.y == enemyPos2D.y)
                {
                    if  (closestEnemy.GetComponent<MushroomAttack>().inCombat == false && closestEnemy.GetComponent<MushroomAttack>().attacker == null)
                    {
                        moveToTarget(closestEnemy);
                        healthSystem = closestEnemy.GetComponent<HealthSystem>();
                        inCombat = true;
                        closestEnemy.GetComponent<MushroomAttack>().inCombat = true;
                        closestEnemy.GetComponent<MushroomAttack>().attacker = gameObject;
                    }
                    

                }
                else if (!inCombat)
                {
                    moveToGuard();
                }
            }
            else
            {
                inCombat = false;
                moveToGuard();
            }
        }
        else
        {
            if (closestEnemy != null)
            {
                moveToTarget(closestEnemy);
            }
            else
            {
                moveToGuard();
            }

        }
    }

    void moveToGuard()
    {
        if (Vector3.Distance(closestEnemyPath.transform.position, guardPoint) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, guardPoint, speed * Time.deltaTime);
        }

    }
    private Vector3 GetRandomPointInRadius(Vector3 center, float radius)
    {
        float angle = UnityEngine.Random.Range(0f, 1f) * Mathf.PI * 2f; // get a random angle around the center
        float distance = UnityEngine.Random.Range(0f, 1f) * radius; // get a random distance within the radius
        Vector3 offset = new Vector3(Mathf.Sin(angle), Mathf.Cos(angle), 0f) * distance; // calculate the offset from the center
        return center + offset;
    }
}
