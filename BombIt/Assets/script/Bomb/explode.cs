using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explode : MonoBehaviour {
	
	float timer;
	/*bomb BOMBcs;
	GameObject Bombs;
	void Awake(){
		Bombs = GameObject.FindGameObjectWithTag ("Bomb");
		BOMBcs = Bombs.GetComponent<bomb> ();
	}*/
	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		if (timer >= 2f) {
			timer = 0f;
			Destroy (gameObject);
		}
	}
	/*void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "Bomb" ) {
			BOMBcs.putbomb (3f);
		}
	}*/

}
