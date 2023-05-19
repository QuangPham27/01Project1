using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    [SerializeField]
    Image healthBar;
    public float health;
    public float healthMax;
    private float idleTime = 0;
    Camera camera;
    Player player;
    private bool repspawned = false;

    public GameObject Knight;
    public GameObject Mushroom;
    private bool dead = false;
    Animator animator;
    const string HIT_ANIM = "hit";

    DieEffect dieEffect;

    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
        player = camera.GetComponent<Player>();
        animator = GetComponent<Animator>();
        dieEffect = GetComponent<DieEffect>();
        if (gameObject.CompareTag("Enemy"))
        {
            animator = GetComponent<Animator>();
            animator.SetBool("isShaking", true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.CompareTag("Knight"))
        {
            healthRegen();
            Mushroom = gameObject.GetComponent<KnightMovement>().engagedTarget;
        }
        else if (gameObject.CompareTag("Enemy"))
        {
            //Debug.Log(gameObject.name + " Called");
            Knight = gameObject.GetComponent<MushroomAttack>().attacker;
        }

        if (health <= 0 && dead == false)
        {
            dead = true;
            if (CompareTag("Knight"))
            {
                if (!repspawned) 
                {
                    gameObject.GetComponent<KnightMovement>().Die();
                    repspawned = true;
                }
                Mushroom.GetComponent<MushroomAttack>().inCombat = false;
                Mushroom.GetComponent<NavMeshAgent>().isStopped = false;
                Mushroom.GetComponent<MushroomAttack>().attacker = null;
                Destroy(gameObject);
            }
            if (CompareTag("Enemy"))
            {
                if (Mushroom.GetComponent<MushroomAttack>().attacker != null)
                {
                    Mushroom.GetComponent<MushroomAttack>().attacker.GetComponent<KnightMovement>().engagedTarget = null;
                    Mushroom.GetComponent<MushroomAttack>().attacker.GetComponent<KnightMovement>().inCombat = false;
                }
                dieEffect.startDisappearing(Mushroom);
            }
        }
    }

    void healthRegen()
    {

        if (Knight.GetComponent<KnightMovement>().inCombat == false)
        {
            idleTime += Time.fixedDeltaTime;
            if (idleTime >= 10.0)
            {
                health += Time.fixedDeltaTime * 4 / 10;
                healthBar.fillAmount = health / healthMax;
                if (health > healthMax)
                {
                    health = healthMax;
                    healthBar.fillAmount = health / healthMax;
                }
            }

        }
        else
        {
            idleTime = 0;
        }
    }

    public void Hit(int damage)
    {
        animator.SetTrigger(HIT_ANIM);
        health -= damage;
        healthBar.fillAmount = (float)health / healthMax;
    }
}
