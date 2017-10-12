using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour {

    [SerializeField]
    private int health = 100;

    private Animator anim;
    private NavMeshAgent agent;
    private Collider collider;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        collider = GetComponent<Collider>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void TakeHit(int damage)
    {
        Debug.LogFormat("TakeHit: {0}", damage);

        health -= damage;

        if (health > 0)
        {
            anim.SetTrigger("Hurt");
        }
        else
        {
            if (agent.isOnNavMesh)
            {
                agent.isStopped = true;
                agent.enabled = false;
                collider.enabled = false;
                anim.SetTrigger("Die");
                Invoke("DestroyGameObject", 1);
            }
        }

    }

    private void DestroyGameObject()
    {
        Destroy(gameObject);
    }
}
