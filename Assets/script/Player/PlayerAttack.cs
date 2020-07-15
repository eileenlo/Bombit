using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {
	public Animator anim;

	public int Bombsum,Maxbomb;
	public GameObject bombex;
	public GameObject FireGun ,IceBar;
	public int FireRange = 1;
	public int playerlife = 50;
	float px,pz;
	bool canFire ,canIce;
	int firechance , icechance;
	bool[] bomblist = new bool[34];
	int temp = 0;

	Quaternion rot=Quaternion.Euler(0,0,0);
	void Start () {
		
		anim = GetComponent<Animator>();
		for (int i = 1; i <= 33; i++) {
			bomblist [i] = false;
		}
		Maxbomb = 1;
		Bombsum = Maxbomb;
		canFire = false;
	
		canIce = false;
		PlayerABISnow.snowenabled = false;
		PlayerABIFire.fireenabled = false;
	}
	

	void Update () {
		PlayerBombcount.sum = Bombsum;
		PlayerLifecount.life = playerlife;
		if (Input.GetKeyDown (KeyCode.Space)) {
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
				//--------------------------------------------------
			if (canFire) {
				anim.Play ("POSE30");
				if (firechance > 0) {
					GameObject go = Instantiate (FireGun, new Vector3 (px, 0.5f, pz), transform.rotation) as GameObject;
					Destroy (go, 3f);
					firechance--;
					PlayerABIFire.fireenabled = true;
					PlayerABIFire.FireChance = firechance;
				} 
				if(firechance == 0){
					canFire = false;
					PlayerABIFire.fireenabled = false;
				}
				//--------------------------------------------------
			}else if(canIce){
				anim.Play ("POSE30");
				if (icechance > 0) {
					
					GameObject go = Instantiate (IceBar, new Vector3 (px, 0.5f, pz), transform.rotation) as GameObject;
					Destroy (go, 1.0f); 
					icechance--;
					PlayerABISnow.snowenabled = true;
					PlayerABISnow.SnowChance = icechance;

				} 
				if(icechance == 0){
					canIce = false;
					PlayerABISnow.snowenabled = false;
				}

				//--------------------------------------------------
			} else if (Bombsum > 0) {
				temp++;
				if (temp > 33) 
					temp = 1;
				bomblist [temp] = true;
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
			playerlife -= 1;
			if (playerlife <= 0) {
				//print ("dead");
				anim.SetTrigger("die");
			}
		}
		if (other.gameObject.tag == "FireGun") {
			canFire = true;
			canIce = false;
			firechance = 3;
			PlayerABIFire.FireChance = firechance;
			PlayerABIFire.fireenabled = true;
			PlayerABISnow.snowenabled = false;
			Destroy (other.gameObject);
		}
		if (other.gameObject.tag == "PIce") {
			canIce = true;
			canFire = false;
			icechance = 1;
			PlayerABISnow.SnowChance = icechance;
			PlayerABISnow.snowenabled = true;
			PlayerABIFire.fireenabled = false;
			Destroy (other.gameObject);
		}

		if (other.gameObject.tag == "PHeart") {
			playerlife += 1;
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
