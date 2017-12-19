using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		Vector3 go = new Vector3();
		if (Input.GetKey (KeyCode.UpArrow)) {
			go = new Vector3 (0.0f, 1.0f, 0.0f);
		}
		if (Input.GetKey (KeyCode.DownArrow)) {
			go = new Vector3 (0.0f, -1.0f, 0.0f);
		}
		if (Input.GetKey (KeyCode.LeftArrow)) {

			go = new Vector3 (-1.0f, 0.0f, 0.0f);
		}
		if (Input.GetKey (KeyCode.RightArrow)) {
			go = new Vector3 (1.0f, 0.0f, 0.0f);
		}
		GetComponent<Transform> ().position += go * Time.deltaTime * 2f;
	}
}
