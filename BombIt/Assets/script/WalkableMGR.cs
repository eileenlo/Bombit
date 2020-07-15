using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkableMGR : MonoBehaviour {
	public int[,] walkable = new int[20,20] ;
	string[] a = new string[200] ;

	int i = 0;
	public float indexX =-10000.0f,indexZ =-10000.0f;
	// Use this for initialization
	void Awake () {
		for (int i=0; i < walkable.GetLength(0); i++) {
			for (int j=0; j < walkable.GetLength(1); j++) {
				if (i % 2 == 0 || (j + 1) % 2 == 0) {
					walkable [i, j] = 0;

				}else{
					walkable [i, j] = 99;

				}
			}
		}

	}

	public void walkpoint (int x ,int z, int num) {
		walkable [x, z] += num;

		//print ("walkable [" + x + "," + z + "] = " + walkable [x, z]);

	}

	public void walkNorth(int x, int z){

		a[i] = x.ToString () + "," + z.ToString ();
		//print ("walkNorth：a[" + i + "] = " + a [i]);
		z--;

		if (z < 0 || walkable [x, z] >= 99) {
			
			z++;
			i++;
			if ((x - 1) >= 0 && walkable [x - 1, z] < 99) {
				//print ("N>W");
				walkWest (x - 1, z);

			} else if ((x + 1) < 20 && walkable [x + 1, z] < 99) {
				//print ("N>E");
				walkEast (x + 1, z);
			}
				
			return;
		} else {

			if (walkable [x, z] == 1) {
				i++;

				walkNorth (x,z);

			} else {
				i++;
				a[i] = x.ToString () + "," + z.ToString ();
				//print ("walkNorth：a[" + i + "] = " + a [i]);
				indexX = System.Convert.ToSingle (x)-9.5f;
				indexZ = System.Convert.ToSingle (z) * (-1) + 9.5f;
				i = 0;
				return ;

			}
		}
	}
	public void walkEast(int x, int z){

		a[i] = x.ToString () + "," + z.ToString ();
		//print ("walkEast：a[" + i + "] = " + a [i]);
		x++;

		if (x > 19 ||walkable [x, z] >= 99) {
			//i = 0;
			x--;
			i++;
			if ((z - 1) >= 0 && walkable [x , z-1] < 99) {
				//print ("E>N");
				walkNorth (x, z-1);
			} else if ((z + 1) < 20 && walkable [x, z+1] < 99) {
				//print ("E>S");
				walkSouth (x, z + 1);
			}
			return ;
		} else {

			if (walkable [x, z] == 1) {
				i++;

				walkEast (x,z);

			} else {
				i++;
				a[i] = x.ToString () + "," + z.ToString ();
				//print ("walkEast：a[" + i + "] = " + a [i]);
				indexX = System.Convert.ToSingle (x)-9.5f;
				indexZ = System.Convert.ToSingle (z) * (-1) + 9.5f;
				i = 0;
				return ;

			}
		}
	}
	public void walkSouth(int x, int z){

		a[i] = x.ToString () + "," + z.ToString ();
		//print ("walkSouth：a[" + i + "] = " + a [i]);
		z++;

		if (z > 19 || walkable [x, z] >= 99) {
			//i = 0;
			z--;
			i++;
			if ((x - 1) >= 0 && walkable [x - 1, z] < 99) {
				//print ("S>W");
				walkWest (x - 1, z);
			} else if ((x + 1) < 20 && walkable [x + 1, z] < 99) {
				//print ("S>E");
				walkEast (x + 1, z);
			}
			return ;
		} else {

			if (walkable [x, z] == 1) {
				i++;

				walkSouth (x,z);

			} else {
				i++;
				a[i] = x.ToString () + "," + z.ToString ();
				//print ("walkSouth：a[" + i + "] = " + a [i]);
				indexX = System.Convert.ToSingle (x)-9.5f;
				indexZ = System.Convert.ToSingle (z) * (-1) + 9.5f;
				i = 0;
				return ;

			}
		}
	}
	public void walkWest(int x, int z){

		a[i] = x.ToString () + "," + z.ToString ();
		//print ("walkWest：a[" + i + "] = " + a [i]);
		x--;

		if (x < 0 || walkable [x, z] >= 99) {
			//i = 0;
			x++;
			i++;
			if ((z - 1) >= 0 && walkable [x , z-1] < 99) {
				//print ("W>N");
				walkNorth (x, z-1);
			} else if ((z + 1) < 20 && walkable [x, z+1] < 99) {
				//print ("W>S");
				walkSouth (x, z + 1);
			}
			return ;
		} else {

			if ( walkable [x, z] == 1) {
				i++;

				walkWest (x,z);

			} else {
				i++;
				a[i] = x.ToString () + "," + z.ToString ();
				//print ("walkWest：a[" + i + "] = " + a [i]);
				indexX = System.Convert.ToSingle (x)-9.5f;
				indexZ = System.Convert.ToSingle (z) * (-1) + 9.5f;
				i = 0;
				return;

			}
		}
	}


	/*public void checkNearest(Vector3 aiPos)
		{
			
			float tempX, tempZ;
			float dist = 1000000.0f;
			float idxX = -1.0f;
			float idxZ = -1.0f;
			for (int i = 0; i < walkable.GetLength (0); i++) {
				for (int j = 0; j < walkable.GetLength (1); j++) {
					if (maze [i,j] == 0) {
						// coordinates
						tempX = System.Convert.ToSingle (i)-9.5f;
						tempZ = System.Convert.ToSingle (j) * (-1) + 9.5f;
						// distance Vector3.Distance
						float distance =  Vector3.Distance(aiPos,new Vector3 (tempX, 0.0f, tempZ));

						// check distance < dist
						// if yes -> keep idxX, idxZ, dist
						if (distance < dist) {
							//if (Vector3.Dot (aiForward, (transform.position - aiPos)) < -0.2f) {
								idxX = tempX;
								idxZ = tempZ;
								dist = distance;
							//}
						}
					}
				}
			}


		}
		/*void Update () {
			for (int i=0; i < walkable.GetLength(0); i++) {
				for (int j=0; j < walkable.GetLength(1); j++) {
					Debug.Log ("walkable [" + i + "," + j + "] = " + walkable [i, j]);
				}
			}

		}*/
	// Update is called once per frame

}
