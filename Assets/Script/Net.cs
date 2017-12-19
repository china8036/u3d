using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Net.Sockets;
using System.Linq;
using System;

public class Net : MonoBehaviour
{

	//服务端套字节
	Socket socket;

	const int BUFFER_SIZE = 4;

	byte[] readBuffer = new byte[BUFFER_SIZE];

	public void Connect ()
	{

		socket = new Socket (AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
		string host = "127.0.0.1";
		int port = 8888;
		string str = "Hello im c# socket";
		socket.Connect (host, port);
		byte[] bytes = System.Text.Encoding.Default.GetBytes (str);
		socket.Send (bytes);
		//socket.EndSend (syncresult);
		//socket.Close ();
		//socket.BeginReceive (readBuffer, 0, readBuffer.Length, SocketFlags.None, new AsyncCallback (ReceiveCallback), socket);
		socket.Close ();
	}




	// Use this for initialization
	void Start ()
	{
		//Connect ();
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}


	void ReceiveCallback (IAsyncResult result)
	{
		try {
			Socket sk = (Socket)result.AsyncState;
			sk.EndReceive (result);
			result.AsyncWaitHandle.Close ();
			Debug.Log ("receive:" + System.Text.Encoding.UTF8.GetString (readBuffer));
		} catch (Exception ex) {
			Debug.Log (ex.Message);
		}
	}




}
