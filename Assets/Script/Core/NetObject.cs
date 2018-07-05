using UnityEngine;
using System.Collections.Generic;
namespace Core
{

	public abstract class Player: MonoBehaviour
	{

		protected Vector3 nowPosition;

		protected Queue<Vector3> forceQueue = new Queue<Vector3>(); 

		//设置位置
		public void setPosition(Vector3 position) {
			this.nowPosition = position;
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
}