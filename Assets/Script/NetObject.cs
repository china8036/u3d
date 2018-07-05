using System;
using Core;
using UnityEngine;
using System.Collections.Generic;
public class NetObject:MonoBehaviour
{
	protected Vector3 nowPosition;

	protected Queue<Vector3> forceQueue = new Queue<Vector3>(); 

	void Start ()
	{
		this.nowPosition = GetComponent<Transform> ().position;
	}


	//设置位置
	public void setPosition(Vector3 position) {
		this.nowPosition = position;
	}

	public Vector3  getPosition(){
		return this.nowPosition;
	}

	public Vector3 getForce(){
		if (this.forceQueue.Count > 0) {
			return this.forceQueue.Dequeue ();
		}

		return new Vector3 ();
	}
	//设置受力
	public void setForce(Vector3 force)
	{
		this.forceQueue.Enqueue (force);
	}

	//设置name
	public void SetName(string name) {
		this.name = name;
	}

	//获取name
	public string GetName() {
		return this.name;
	}
}
