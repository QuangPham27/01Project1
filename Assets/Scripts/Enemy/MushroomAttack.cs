using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class MushroomAttack : MonoBehaviour
{
    private Detection range;
    private HealthSystem healthSystem;
    private CircleCollider2D detectRange;
    private Rigidbody2D rb;
    public float attackSpeed = 2.0f;
    private float nextAttackTime = 0.0f;
    public int attackDmg;
    public int Gold;
    public GameObject attacker;

    public bool inCombat = false;
    // Start is called before the first frame update
    void Start()
    {
        range = GetComponent<Detection>();
        detectRange = range.Range.GetComponent<CircleCollider2D>();
        rb = this.GetComponent<Rigidbody2D>();
        GetComponent<HealthSystem>().Mushroom = gameObject;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (attacker != null)
        {
            inCombat = true;
        } else
        {
            inCombat = false;
        }
        if (inCombat)
        {
            Engaged();
        }

    }
    void Engaged()
    {
        if(attacker != null)
        {
            if (attacker.GetComponent<KnightMovement>().inCombat == true)

            {
                healthSystem = attacker.GetComponent<HealthSystem>();
                attacker.GetComponent<KnightMovement>().engagedTarget = gameObject;
                GetComponent<NavMeshAgent>().isStopped = true;
                if (transform.position == attacker.transform.position)
                {
                    attack();
                }
            }
        }
    }

        void attack()
        {
            if (Time.time >= nextAttackTime)
                if (attacker != null)
                {
                    healthSystem.Hit(attackDmg);
                    nextAttackTime = Time.time + attackSpeed;
                }
        }
}
