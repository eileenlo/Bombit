using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerABISnow : MonoBehaviour {

	public Image snow;
	public static bool snowenabled;
	Text text;
	public static int SnowChance = 0;
	// Use this for initialization
	void Start () {
		//snow.enabled = false;
		text = GetComponent<Text> ();
		snowenabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (snowenabled) {
			snow.enabled = true;
			text.enabled = true;
			text.text = "x"+SnowChance.ToString();
		} else {
			snow.enabled = false;
			text.enabled = false;
		}
	}
}
