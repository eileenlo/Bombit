using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bombByEnemy : MonoBehaviour {
	public GameObject exfire;

	EnemyAttack enemyattack;
	GameObject enemy, mgr;
	WalkableMGR walkableMGR;

	float timer,dist;
	int Range;
	int[,] firepoint = new int[20,20];

	void Awake(){
		enemy = GameObject.FindGameObjectWithTag ("Enemy");
		enemyattack = enemy.GetComponent<EnemyAttack> ();
		mgr = GameObject.FindGameObjectWithTag ("MGR");
		walkableMGR = mgr.GetComponent<WalkableMGR> ();
	}

	// Use this for initialization
	void Start () {
		Range = enemyattack.FireRange ;
		explosion ();
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		putbomb (timer);


	}
	void OnTriggerExit(Collider other){
		GetComponent<Collider>().isTrigger = false;
	}
	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "Fire" ) {
			this.putbomb (3f);
		}
	}


	public void putbomb(float timer){
		transform.rotation = Quaternion.Euler (0, 0, 0);
		if (timer >= 3f) {
			enemyattack.recover(1);
			destroyBox ();
			timer = 0f;
			for (int i = 0; i < firepoint.GetLength (0); i++) {
				for (int j = 0; j < firepoint.GetLength (1); j++) {
					if (firepoint [i, j] >= 1) {
						firepoint [i, j] -= 1;
						Instantiate (exfire, new Vector3(System.Convert.ToSingle (i)-9.5f,0.5f,
														System.Convert.ToSingle(j)*(-1)+9.5f), transform.rotation);
						walkableMGR.walkpoint (i, j, -1);
					}
				}
			}
			Destroy (gameObject);

		}

	}

	void explosion(){ //1.兩顆炸彈互相碰到(火) 2.精簡程式or其他方法

		walkableMGR.walkpoint (System.Convert.ToInt16 ((transform.position.x) + 9.5f),
			Mathf.Abs (System.Convert.ToInt16 ((transform.position.z) - 9.5f)),1);
		firepoint [System.Convert.ToInt16 ((transform.position.x) + 9.5f),
			Mathf.Abs (System.Convert.ToInt16 ((transform.position.z) - 9.5f))] += 1;

		//上----------------------------------------------------------------------------------------------------
		for (float i = 1.0f; i <= Range; i++) {	
			float j = i;
			GameObject[] obst;
			Vector3 UP = transform.position + transform.forward * i;
			obst = GameObject.FindGameObjectsWithTag("Obstacle");
			foreach (GameObject go in obst) {
				dist = Vector3.Distance (UP, go.transform.position);
				if (dist < 1.0f  || UP.z >9.5f) {
					j = Range + 1;
				}
			}
			if (j == i) {
				walkableMGR.walkpoint( System.Convert.ToInt16 ((UP.x) + 9.5f),
					Mathf.Abs (System.Convert.ToInt16 ((UP.z) - 9.5f)), 1);
				firepoint [System.Convert.ToInt16 ((UP.x) + 9.5f),
					Mathf.Abs (System.Convert.ToInt16 ((UP.z) - 9.5f))] += 1;

				GameObject[] bos;
				bos = GameObject.FindGameObjectsWithTag ("Box");
				foreach (GameObject go in bos) {
					dist = Vector3.Distance (UP, go.transform.position);
					if (dist < 1.0f ) {
						i = Range + 1;
					}
				}
			} else
				i = j;
		}
		//右----------------------------------------------------------------------------------------------------
		for (float i = 1.0f; i <= Range; i++) {
			float j = i;
			Vector3 RIGHT = transform.position + transform.right * i;
			GameObject[] obst;
			obst = GameObject.FindGameObjectsWithTag ("Obstacle");
			foreach (GameObject go in obst) {
				dist = Vector3.Distance (RIGHT, go.transform.position);
				if (dist < 1.0f || RIGHT.x > 9.5f) {
					j = Range + 1;
				}
			}
			if (j == i) {
				walkableMGR.walkpoint(System.Convert.ToInt16 ((RIGHT.x) + 9.5f),
					Mathf.Abs (System.Convert.ToInt16 ((RIGHT.z) - 9.5f)), 1);
				firepoint[System.Convert.ToInt16 ((RIGHT.x) + 9.5f),
					Mathf.Abs (System.Convert.ToInt16 ((RIGHT.z) - 9.5f))] += 1;

				GameObject[] bos;
				bos = GameObject.FindGameObjectsWithTag ("Box");
				foreach (GameObject go in bos) {
					dist = Vector3.Distance (RIGHT, go.transform.position);
					if (dist < 1.0f) {
						i = Range + 1;
					}
				}
			}else
				i = j;
		}
		//下----------------------------------------------------------------------------------------------------
		for (float i = -1.0f; i >= -Range; i--) {
			float j = i;
			Vector3 DOWN = transform.position + transform.forward * i;
			GameObject[] obst;
			obst = GameObject.FindGameObjectsWithTag ("Obstacle");
			foreach (GameObject go in obst) {
				dist = Vector3.Distance (DOWN, go.transform.position);
				if (dist < 1.0f || DOWN.z < -9.5f) {
					j = -Range - 1;
				}
			}
			if (j == i) {
				walkableMGR.walkpoint(System.Convert.ToInt16 ((DOWN.x) + 9.5f),
					Mathf.Abs (System.Convert.ToInt16 ((DOWN.z) - 9.5f)), 1);
				firepoint [System.Convert.ToInt16 ((DOWN.x) + 9.5f),
					Mathf.Abs (System.Convert.ToInt16 ((DOWN.z) - 9.5f))] += 1;

				GameObject[] bos;
				bos = GameObject.FindGameObjectsWithTag ("Box");
				foreach (GameObject go in bos) {
					dist = Vector3.Distance (DOWN, go.transform.position);
					if (dist < 1.0f) {
						i = -Range - 1;
					}
				}
			}else
				i = j;
		}
		//左----------------------------------------------------------------------------------------------------
		for (float i = -1.0f; i >= -Range; i--) {
			float j = i;
			Vector3 LEFT = transform.position + transform.right * i;
			GameObject[] obst;
			obst = GameObject.FindGameObjectsWithTag ("Obstacle");
			foreach (GameObject go in obst) {
				dist = Vector3.Distance (LEFT, go.transform.position);
				if (dist < 1.0f || LEFT.x < -9.5f) {
					j = -Range - 1;
				}
			}
			if (j == i) {
				walkableMGR.walkpoint(System.Convert.ToInt16 ((LEFT.x) + 9.5f),
					Mathf.Abs (System.Convert.ToInt16 ((LEFT.z) - 9.5f)), 1);
				firepoint[System.Convert.ToInt16 ((LEFT.x) + 9.5f),
					Mathf.Abs (System.Convert.ToInt16 ((LEFT.z) - 9.5f))] += 1;

				GameObject[] bos;
				bos = GameObject.FindGameObjectsWithTag ("Box");
				foreach (GameObject go in bos) {
					dist = Vector3.Distance (LEFT, go.transform.position);
					if (dist < 1.0f) {
						i = -Range - 1;
					}
				}
			}else
				i = j;
		}
		walkableMGR.walkNorth (System.Convert.ToInt16 (transform.position.x + 9.5f),
			Mathf.Abs (System.Convert.ToInt16 (transform.position.z - 9.5f)));
		walkableMGR.walkEast (System.Convert.ToInt16 (transform.position.x + 9.5f),
			Mathf.Abs (System.Convert.ToInt16 (transform.position.z - 9.5f)));
		walkableMGR.walkSouth (System.Convert.ToInt16 (transform.position.x + 9.5f),
			Mathf.Abs (System.Convert.ToInt16 (transform.position.z - 9.5f)));
		walkableMGR.walkWest (System.Convert.ToInt16 (transform.position.x + 9.5f),
			Mathf.Abs (System.Convert.ToInt16 (transform.position.z - 9.5f)));
	}
	void destroyBox(){
		//上----------------------------------------------------------------------------------------------------
		for (float i = 1.0f; i <= Range; i++) {	
			float j = i;
			GameObject[] obst;
			Vector3 UP = transform.position + transform.forward * i;
			obst = GameObject.FindGameObjectsWithTag("Obstacle");
			foreach (GameObject go in obst) {
				dist = Vector3.Distance (UP, go.transform.position);
				if (dist < 1.0f  || UP.z >9.5f) {
					j = Range + 1;
				}
			}
			if (j == i) {
				GameObject[] bos;
				bos = GameObject.FindGameObjectsWithTag ("Box");
				foreach (GameObject go in bos) {
					dist = Vector3.Distance (UP, go.transform.position);
					if (dist < 1.0f ) {
						walkableMGR.walkpoint( System.Convert.ToInt16 ((UP.x) + 9.5f),
							Mathf.Abs (System.Convert.ToInt16 ((UP.z) - 9.5f)), -99);
						Destroy (go);
						i = Range + 1;
					}
				}
			} else
				i = j;
		}
		//右----------------------------------------------------------------------------------------------------
		for (float i = 1.0f; i <= Range; i++) {
			float j = i;
			Vector3 RIGHT = transform.position + transform.right * i;
			GameObject[] obst;
			obst = GameObject.FindGameObjectsWithTag ("Obstacle");
			foreach (GameObject go in obst) {
				dist = Vector3.Distance (RIGHT, go.transform.position);
				if (dist < 1.0f || RIGHT.x > 9.5f) {
					j = Range + 1;
				}
			}
			if (j == i) {
				GameObject[] bos;
				bos = GameObject.FindGameObjectsWithTag ("Box");
				foreach (GameObject go in bos) {
					dist = Vector3.Distance (RIGHT, go.transform.position);
					if (dist < 1.0f) {
						walkableMGR.walkpoint(System.Convert.ToInt16 ((RIGHT.x) + 9.5f),
							Mathf.Abs (System.Convert.ToInt16 ((RIGHT.z) - 9.5f)), -99);
						Destroy (go);
						i = Range + 1;
					}
				}
			}else
				i = j;
		}
		//下----------------------------------------------------------------------------------------------------
		for (float i = -1.0f; i >= -Range; i--) {
			float j = i;
			Vector3 DOWN = transform.position + transform.forward * i;
			GameObject[] obst;
			obst = GameObject.FindGameObjectsWithTag ("Obstacle");
			foreach (GameObject go in obst) {
				dist = Vector3.Distance (DOWN, go.transform.position);
				if (dist < 1.0f || DOWN.z < -9.5f) {
					j = -Range - 1;
				}
			}
			if (j == i) {
				GameObject[] bos;
				bos = GameObject.FindGameObjectsWithTag ("Box");
				foreach (GameObject go in bos) {
					dist = Vector3.Distance (DOWN, go.transform.position);
					if (dist < 1.0f) {
						walkableMGR.walkpoint(System.Convert.ToInt16 ((DOWN.x) + 9.5f),
							Mathf.Abs (System.Convert.ToInt16 ((DOWN.z) - 9.5f)), -99);
						Destroy (go);
						i = -Range - 1;
					}
				}
			}else
				i = j;
		}
		//左----------------------------------------------------------------------------------------------------
		for (float i = -1.0f; i >= -Range; i--) {
			float j = i;
			Vector3 LEFT = transform.position + transform.right * i;
			GameObject[] obst;
			obst = GameObject.FindGameObjectsWithTag ("Obstacle");
			foreach (GameObject go in obst) {
				dist = Vector3.Distance (LEFT, go.transform.position);
				if (dist < 1.0f || LEFT.x < -9.5f) {
					j = -Range - 1;
				}
			}
			if (j == i) {
				GameObject[] bos;
				bos = GameObject.FindGameObjectsWithTag ("Box");
				foreach (GameObject go in bos) {
					dist = Vector3.Distance (LEFT, go.transform.position);
					if (dist < 1.0f) {
						walkableMGR.walkpoint(System.Convert.ToInt16 ((LEFT.x) + 9.5f),
							Mathf.Abs (System.Convert.ToInt16 ((LEFT.z) - 9.5f)), -99);
						Destroy (go);
						i = -Range - 1;
					}
				}
			}else
				i = j;
		}

	}
}
