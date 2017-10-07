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
        StartCoroutine(CreateEnemies(1000));
	}
	
	// Update is called once per frame
	void Update() 
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            var ray = playerCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log(hit.point);
                var spellPrefab = Resources.Load<GameObject>("Spells/Spell1");
                var origin = new Vector3(0, 2, 0);
                var spell = Instantiate(spellPrefab, origin, Quaternion.identity);
                spell.transform.LookAt(hit.point);
            }
        }
	}

    IEnumerator CreateEnemies(int quantity)
    {
        for (int i = 0; i < quantity; i++)
        {
            CreateEnemy(enemiesPrefab[0]);
            yield return new WaitForSeconds(1f);
            CreateEnemy(enemiesPrefab[1]);
            yield return new WaitForSeconds(1f);
        }
    }

    void CreateEnemy(GameObject enemyPrefab)
    {
        var enemy = Instantiate(enemyPrefab, enemyStart.position, enemyStart.rotation);
        var agent = enemy.GetComponent<NavMeshAgent>();
        agent.SetDestination(enemyEnd.position);
    }

}
