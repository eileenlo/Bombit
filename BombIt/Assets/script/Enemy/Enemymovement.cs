using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemymovement : MonoBehaviour {
	public Animator anim;
	public Rigidbody rbody;

	private float inputH;
	private float inputV;

	NavMeshAgent navAgent;
	GameObject target;
	GameObject mgr;
	WalkableMGR walkableMGR;
	WalkableMGRplayer walkableMGRP;
	float px,pz;
	//bool beblock = false;
	float ICEtimer,walktimer;
	//bool isplayerbomb = false;

	void Start () {
		anim = GetComponent<Animator>();
		rbody = GetComponent<Rigidbody>();
		navAgent = GetComponent<NavMeshAgent> ();
		target = GameObject.FindGameObjectWithTag ("Player");
		mgr = GameObject.FindGameObjectWithTag ("MGR");
		walkableMGR = mgr.GetComponent<WalkableMGR> ();
		walkableMGRP = mgr.GetComponent<WalkableMGRplayer> ();
	}

	// Update is called once per frame
	void Update () {
		if (anim.enabled == false) {
			ICEtimer += Time.deltaTime;
			if (ICEtimer >= 5f) {
				anim.enabled = true;
				navAgent.enabled = true;
			}
		} else {
			
			/*px = transform.position.x;
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
			//walkableMGR.checkNearest (transform.position);
			walkableMGR.checkCanWalk(System.Convert.ToInt16 (px + 9.5f),
				Mathf.Abs (System.Convert.ToInt16 (pz - 9.5f)));*/
			if (Vector3.Distance (transform.position, target.transform.position) > 1.0f) {
				if (walkableMGR.indexX != -10000.0f || walkableMGR.indexZ != -10000.0f) {
					//isplayerbomb = false;
					walktimer += Time.deltaTime;
					navAgent.SetDestination (new Vector3 (walkableMGR.indexX, 0.1f, walkableMGR.indexZ));
					if (walktimer >= 6) {
						walkableMGR.indexX = -10000.0f;
						walkableMGR.indexZ = -10000.0f;
						walktimer = 0f;
					}
				} else {
					//isplayerbomb = true;

					if (walkableMGRP.indexX != -10000.0f || walkableMGRP.indexZ != -10000.0f) {
						walktimer += Time.deltaTime;
						navAgent.SetDestination (new Vector3 (walkableMGRP.indexX, 0.1f, walkableMGRP.indexZ));
						if (walktimer >= 6) {
							walkableMGRP.indexX = -10000.0f;
							walkableMGRP.indexZ = -10000.0f;
							walktimer = 0f;
						}
					} else {
						navAgent.SetDestination (target.transform.position);
					}
				}	
				anim.SetBool ("walk", true);
			} else {
				anim.SetBool ("walk", false);
			}

		}
	}

	
	void OnTriggerEnter(Collider other) {
		//吃到鞋子球
		if (other.gameObject.tag == "PickShoe") {
			if (navAgent.speed < 6.0f) {
				navAgent.speed += 0.4f;
			} else {
				navAgent.speed =6.0f;
			}
			Destroy (other.gameObject);
		}
		if (other.gameObject.tag == "Ice") {
			ICEtimer = 0f;
			anim.enabled = false;
			navAgent.enabled = false;
		}
	}
}
