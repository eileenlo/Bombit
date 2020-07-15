using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBombcount : MonoBehaviour {

	public static int sum;
	Text text;

	// Use this for initialization
	void Start () {
		text = GetComponent<Text> ();
		sum = 1;
	}
	
	// Update is called once per frame
	void Update () {
		text.text = "：" + sum;
	}
}
