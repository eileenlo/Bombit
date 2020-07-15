using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverMGR : MonoBehaviour {

	public PlayerAttack playerHealth;
	public EnemyAttack enemyHealth;
	Animator anim;
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (playerHealth.playerlife <= 0) {
			anim.SetTrigger ("AIWin");

		} else if(enemyHealth.enemylife <= 0){
			anim.SetTrigger ("PlayerWin");

		}
	}
}
