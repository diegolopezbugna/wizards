using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyEnd : MonoBehaviour {

    public int MonstersPassed { get; set; }

    [SerializeField]
    public Text monstersPassedText;

	// Use this for initialization
	void Start () {
		
	}
	
    void OnTriggerEnter(Collider other) {
        MonstersPassed += other.GetComponentsInChildren<Animator>().Length;
        monstersPassedText.text = MonstersPassed.ToString();
        Destroy(other.gameObject);
    }
}
