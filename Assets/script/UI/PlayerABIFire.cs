using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerABIFire : MonoBehaviour {

	public Image fire ;
	public static bool fireenabled;
	Text text;
	public static int FireChance = 0;
	// Use this for initialization
	void Start () {
		//fire.enabled = false;
		text = GetComponent<Text> ();
		fireenabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (fireenabled) {
			fire.enabled = true;
			text.enabled = true;
			text.text = "x"+FireChance.ToString();
		} else {
			fire.enabled = false;
			text.enabled = false;
		}
	}
}
