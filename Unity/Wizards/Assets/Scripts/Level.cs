using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Level : MonoBehaviour {

    [SerializeField]
    private GameObject[] enemiesPrefab;

    [SerializeField]
    private Transform enemyStart;

    [SerializeField]
    private Transform enemyEnd;

    [SerializeField]
    private Camera playerCamera;

	// Use this for initialization
	void Start () 
    {
        StartCoroutine(CreateEnemies(10));
	}
	
	// Update is called once per frame
	void Update() 
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            var ray = playerCamera.ScreenPointToRay(Input.mousePosition);
            LayerMask floorLayer = 1 << LayerMask.NameToLayer("Floor");
            if (Physics.Raycast(ray, out hit, playerCamera.farClipPlane, floorLayer))
            {
                Debug.Log(hit.point);
                var spellPrefab = Resources.Load<GameObject>("Spells/Spell1");
                var origin = Camera.main.transform.position + Vector3.down;//new Vector3(64, 4, 0);
                var spell = Instantiate(spellPrefab, origin, Quaternion.identity);
                spell.transform.LookAt(hit.point);
                spell.GetComponentInChildren<RFX4_TransformMotion>().CollisionEnter += (object sender, RFX4_TransformMotion.RFX4_CollisionInfo e) =>  {
                    var enemyHealth = e.Hit.transform.GetComponent<EnemyHealth>();
                    if (enemyHealth != null)
                        enemyHealth.TakeHit(UnityEngine.Random.Range(150, 250));
                };
            }
        }
	}

    IEnumerator CreateEnemies(int quantity)
    {
        for (int i = 0; i < quantity; i++)
        {
            CreateEnemy(enemiesPrefab[0]);
            yield return new WaitForSeconds(Random.Range(1f, 5f));
//            CreateEnemy(enemiesPrefab[1]);
//            yield return new WaitForSeconds(1f);
        }
    }

    void CreateEnemy(GameObject enemyPrefab)
    {
        var enemy = Instantiate(enemyPrefab, enemyStart.position, enemyStart.rotation);
        var agent = enemy.GetComponent<NavMeshAgent>();
        agent.SetDestination(enemyEnd.position);
    }

}
