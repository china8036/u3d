using Core;
using System;

namespace Message.Recv
{



	[System.Serializable]
	public class InitPlayerMsg : RecvMsg
	{
		public InitPlayer data;
	}

	[System.Serializable]
	public class InitPlayer
	{

		public float x;

		public float y;

		public float z;

		public bool isMaster;

		public string initPlayerId;
	}
}
