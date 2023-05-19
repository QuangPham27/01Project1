using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTargeting : MonoBehaviour
{
    private HealthSystem healthSystem;
    private static int health;
    public float speed;
    public GameObject target;
    private Rigidbody2D rb;
    private Transform lastposition;
    public int hitdmg;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        target = gameObject.GetComponent<FindClosest>().FindClosestEnemy();
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -2f);
        healthSystem = target.GetComponent<HealthSystem>();
        lastposition = target.transform;
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(gameObject);
            if (target != null)
            {
            healthSystem.Hit(hitdmg);
            }
        }
    }

    void moveToTarget()
    {
        
        if (target != null)
        {
            
            Vector2 direction = (target.transform.position - transform.position).normalized;
            transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
            rb.velocity = direction * speed;
        }
        else
        {
            Destroy (gameObject);
        }
    }

    private void FixedUpdate()
    {
        moveToTarget();

    }
}
