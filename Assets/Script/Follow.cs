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

		GetComponent<Transform> ().position = player.GetComponent<Transform> ().position - this.relPosition;

		
	}
}
