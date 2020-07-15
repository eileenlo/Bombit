using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour {
	public Animator anim;
	public Rigidbody rbody;
	public float speed=1.0f;

	private float inputH;
	private float inputV;

	float timer;
	void Start () {
		anim = GetComponent<Animator>();
		rbody = GetComponent<Rigidbody>();

	}
	
	// Update is called once per frame
	void Update () {
		


		inputH = Input.GetAxis ("Horizontal");
		inputV = Input.GetAxis ("Vertical");

		/*anim.SetFloat ("inputH", Mathf.Abs(inputH));
		anim.SetFloat ("inputV",Mathf.Abs(inputV));*/
		if (anim.enabled == false) {
			timer += Time.deltaTime;
			if (timer >= 5f)
				anim.enabled = true;
		} else {

	
			if (Mathf.Abs (inputH) >= 0.1 || Mathf.Abs (inputV) >= 0.1) {
				anim.SetBool ("walk", true);
			} else {
				anim.SetBool ("walk", false);
			}


			if (Input.GetKey ("up") || Input.GetKey (KeyCode.W)) {
				move (0, 0, 1);
			} else if (Input.GetKey ("down") || Input.GetKey (KeyCode.S)) {
				move (-180, 0, -1);
			} else if (Input.GetKey ("left") || Input.GetKey (KeyCode.A)) {
				move (-90, 1, -1);
			} else if (Input.GetKey ("right") || Input.GetKey (KeyCode.D)) {
				move (90, 1, 1);
			}
		}
	}
	void move(int angel , int aswd ,int sa){	//sa=是否為下左(-1)
		Quaternion rot = Quaternion.Euler (0, angel, 0);
		transform.rotation = Quaternion.Slerp (transform.rotation,rot,Time.deltaTime*60f);

		float moveX = inputH *  Time.deltaTime*2f;
		float moveZ = inputV * Time.deltaTime*2f;
	
		if (aswd == 0) {
			//上下
			transform.Translate (0, 0, moveZ*sa*speed);
			//rbody.velocity = new Vector3 ( 0f, 0f, moveZ);	//問如何停止?
		} else if (aswd == 1) {
			//左右
			transform.Translate (0, 0,moveX*sa*speed);
			//rbody.velocity = new Vector3 (moveX, 0f, 0f);
		}


	}


	void OnTriggerEnter(Collider other) {
		//吃到鞋子球			
		if (other.gameObject.tag == "PickShoe") {
			if (speed < 3.0f) {
				speed += 0.2f;
			} else {
				speed = 3.0f;
			}
			Destroy (other.gameObject);
		}
		if (other.gameObject.tag == "Ice") {
			timer = 0f;
			anim.enabled = false;
		}

	}

}
