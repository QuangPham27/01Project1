using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Navigate : MonoBehaviour
{
    private NavMeshAgent agent;
    private GameObject home;
    private Camera camera;
    private Player player;

    private float distance;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        home = GameObject.FindGameObjectWithTag("home");
        camera = Camera.main;
        player = camera.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        var target = home.transform.position;
        target.z = 0;
        agent.destination = target;
         distance = Vector3.Distance(transform.position, target);
        if (distance < 1f)
        {
            arrive();
        }
    }

    void arrive()
    {
        player.Health -= 1;
        Destroy(gameObject);
    }
}
