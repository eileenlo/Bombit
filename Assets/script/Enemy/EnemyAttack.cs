using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAttack : MonoBehaviour {
	float dist;

	GameObject mgr,player;
	WalkableMGR walkableMGR;

	public Animator anim;
	public int Bombsum,Maxbomb;
	public GameObject bombex;
	public int FireRange = 1;
	public int enemylife = 250;
	float px,pz;
	bool[] bomblist = new bool[34];
	int temp = 0;
	float CDtime;
	Quaternion rot=Quaternion.Euler(0,0,0);

	// Use this for initialization
	void Awake(){
		player = GameObject.FindGameObjectWithTag ("Player");
		mgr = GameObject.FindGameObjectWithTag ("MGR");
		walkableMGR = mgr.GetComponent<WalkableMGR> ();
		anim = GetComponent<Animator>();

	}

	void Start () {
		for (int i = 1; i <= 33; i++) {
			bomblist [i] = false;
		}
		Maxbomb = 1;
		Bombsum = Maxbomb;

	}
	
	// Update is called once per frame
	void Update () {
		AILifecount.life = enemylife;
		CDtime += Time.deltaTime;
		GameObject[] Boxs;
		Boxs = GameObject.FindGameObjectsWithTag ("Box");
		foreach (GameObject go in Boxs) {
			dist = Vector3.Distance (transform.position, go.transform.position);
			if (dist < 0.95f) {
				
				createbomb ();

			}
		}
		if (Vector3.Distance (transform.position, player.transform.position) < 1.0f) {
			if(CDtime>=3.0f){
				CDtime = 0f;
				createbomb ();
			}
		}
		if (enemylife <= 0) {

			anim.SetTrigger ("die");
			GetComponent <UnityEngine.AI.NavMeshAgent> ().Stop();

		}
	}
	void createbomb()
	{
		px = transform.position.x;
		pz = transform.position.z;

		if (px >= 0.0f) {
			px = Mathf.CeilToInt (px) - 0.5f;
		} else if (px < 0.0f) {
			px = Mathf.FloorToInt (px) + 0.5f;
		}
		if (pz >= 0.0f) {
			pz = Mathf.CeilToInt (pz) - 0.5f;
		} else if (pz < 0.0f) {	
			pz = Mathf.FloorToInt (pz) + 0.5f;
		}
		if (Bombsum > 0) {
			temp++;
			if (temp > 33) 
				temp = 1;
			bomblist [temp] = true;
			if (walkableMGR.walkable [System.Convert.ToInt16 (px + 9.5f), Mathf.Abs (System.Convert.ToInt16 (pz - 9.5f))] == 0) {
				Instantiate (bombex, new Vector3 (px, 0.5f, pz), rot);

				Bombsum -= 1;
			}

		}
	}
	void OnTriggerEnter(Collider other){
		//吃到炸彈球
		if (other.gameObject.tag == "PickBomb") {
			Maxbomb++;
			Bombsum++;
			Destroy (other.gameObject);
		}
		// 吃到藥水
		if (other.gameObject.tag == "PickWater") {
			FireRange++;
			Destroy (other.gameObject);
		}
		if (other.gameObject.tag == "Fire") {
			enemylife -= 1;

		}
		if (other.gameObject.tag == "PHeart") {
			enemylife += 1;
			Destroy (other.gameObject);
		}

	}
	public void recover(int amount)	{
		for (int i = 1; i <= 33; i++) {
			if (bomblist [i] == true) {
				Bombsum += amount;
				bomblist [i] = false;

				break;

			}
		}

	}

}
