using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSetup : MonoBehaviour {

	public GameObject boxs;
	public GameObject pBOMB;
	public GameObject pSHOE;
	public GameObject pWATER;
	public GameObject pICE;
	public GameObject pFIREGUN;
	public GameObject pHEART;
	//public GameObject a;

	private string[] BoxPosition = new string[300] ;
	private string[] PointXZ = null;
	private string[] NoPut = new string[] {"6.5,-5.5"  , "7.5,-5.5"  , "5.5,-5.5" ,"6.5,-4.5"  ,"6.5,-6.5" ,
										   "6.5,6.5"   , "6.5,7.5"   , "6.5,5.5"  , "7.5,6.5"  , "5.5,6.5" ,
										   "-5.5,-5.5" , "-4.5,-5.5" ,"-5.5,-4.5" ,"-6.5,-5.5" ,"-5.5,-6.5",
										   "-5.5,6.5"  , "-5.5,7.5"  , "-5.5,5.5" , "-4.5,6.5" ,"-6.5,6.5" };

	GameObject mgr;
	WalkableMGR walkableMGR;
	void Awake(){
		mgr = GameObject.FindGameObjectWithTag ("MGR");
		walkableMGR = mgr.GetComponent<WalkableMGR> ();
	}

	void Start () {
		
		//放置每個可放箱子座標
		int count=0;
		for (float j = -9.5f; j <= 9.5f; j += 2) { 
			for (float i = -9.5f; i <= 9.5f; i++) {
				BoxPosition [count] = i.ToString() + "," + j.ToString();
				count++;
			}
			for (float i= -8.5f ;i<=9.5f ;i+=2){
				BoxPosition [count] = j.ToString() + "," + i.ToString();
				count++;
			}
		}
		//亂數交換 i次
		string temp = "";
		int randomNUM, tempNUM;
		for (int i = 0; i < BoxPosition.Length/2; i++) {
			randomNUM = Random.Range (0, BoxPosition.Length);
			temp = BoxPosition [i];
			BoxPosition[i] =BoxPosition [randomNUM];
			BoxPosition [randomNUM] = temp;
		}
		//把300(-)是noput的放到151(+)裡面
		tempNUM = 151;
		foreach (string str in NoPut) {
			for (int i = BoxPosition.Length-1; i >= 170; i--) {
				if (str == BoxPosition [i]) {
					temp = BoxPosition [i];
					BoxPosition [i] = BoxPosition [tempNUM];
					BoxPosition [tempNUM] = temp;
					tempNUM++;
				}
			}
		}
		//把0~150是noput的丟到300(-)交換
		tempNUM = BoxPosition.Length-1;
		foreach (string str in NoPut) {
			for (int i = 0; i < 150; i++) {
				if (str == BoxPosition [i]) {
					temp = BoxPosition [i];
					BoxPosition [i] = BoxPosition [tempNUM];
					BoxPosition [tempNUM] = temp;
					tempNUM--;
				}
			}
		}
		//產生箱子
		Quaternion rot = Quaternion.Euler (0, 0,-90f);
		for (int i = 0; i < 150; i++) {
			PointXZ = BoxPosition[i].Split (',');
			transform.position = new Vector3(System.Convert.ToSingle(PointXZ[0]), 0.5f,System.Convert.ToSingle(PointXZ[1]) );

			walkableMGR.walkpoint( System.Convert.ToInt16 ((transform.position.x) + 9.5f),
				Mathf.Abs (System.Convert.ToInt16 ((transform.position.z) - 9.5f)), 99);
			
			Instantiate (boxs, transform.position,transform.rotation);

			//產生道具
			if (i >= 0 && i < 30) {
				//炸彈
				Instantiate (pBOMB, transform.position,Quaternion.Slerp (transform.rotation,rot,Time.deltaTime*100f));
			} else if (i >= 30 && i < 60) {
				//飛鞋
				Instantiate (pSHOE, transform.position,Quaternion.Slerp (transform.rotation,rot,Time.deltaTime*100f));
			} else if (i >= 60 && i < 80) {
				//藥水
				Instantiate (pWATER, transform.position,Quaternion.Slerp (transform.rotation,rot,Time.deltaTime*100f));
			} else if (i >= 80 && i < 85) {
				//火槍
				Instantiate (pFIREGUN, transform.position,Quaternion.Slerp (transform.rotation,rot,Time.deltaTime*100f));
			} else if (i >= 85 && i < 90) {
				//冰
				Instantiate (pICE, transform.position,Quaternion.Slerp (transform.rotation,rot,Time.deltaTime*100f));
			} else if (i >= 90 && i < 120) {
				//心
				Instantiate (pHEART, transform.position,Quaternion.Slerp (transform.rotation,rot,Time.deltaTime*100f));
			}
				
		}

	}

}
