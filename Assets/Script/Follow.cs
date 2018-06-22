using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour {

	public GameObject player;

	private Vector3 relPosition;

	// Use this for initialization
	void Start () {
		relPosition = player.GetComponent<Transform> ().position - GetComponent<Transform> ().position;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.A))
        {
            GetComponent<Transform>().Rotate(new Vector3(0.0f, -0.5f, 0f));
        }

        if (Input.GetKey(KeyCode.D))
        {
            GetComponent<Transform>().Rotate(new Vector3(0.0f, 0.5f, 0f));
        }
        GetComponent<Transform> ().position = player.GetComponent<Transform> ().position - this.relPosition;

		
	}
}
