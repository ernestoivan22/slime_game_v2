using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Net.Sockets;

public class Client {
	TcpClient clientSocket;
	NetworkStream networkStream;
	ASCIIEncoding encoder;
	bool connected = false;

	public Client(string server_adress) {
		try {
			clientSocket = new TcpClient ();

			Debug.Log ("Connecting.....");
			clientSocket.Connect (server_adress, 1024);
			connected = true;
			Debug.Log ("Connected");

			encoder = new ASCIIEncoding();
		} catch (Exception e) {
			Debug.Log("Error..... " + e.StackTrace);
		}
	}

	public void sendData(String data) {
		if (!connected) {
			Debug.Log("Tratando de enviar y no esta conectado");
			return "";
		}
		try{
			networkStream = clientSocket.GetStream();
			//Debug.Log("Transmitting: " + data);
			byte[] outStream = encoder.GetBytes(data);

			networkStream.Write(outStream, 0, outStream.Length);
			networkStream.Flush();
			
			// Respuesta del servidor
			/**
			byte[] inStream = new byte[1024];
			serverStream.Read(inStream, 0, (int)clientSocket.ReceiveBufferSize);
			String response = encoder.GetString (inStream);
			Console.WriteLine("Server response: " + response);
			**/

		}catch (Exception e) {
			Debug.Log("Error..... " + e.StackTrace);
		}

	}


	public String receiveData() {
		if (!connected) {
			Debug.Log("Tratando de recibir y no esta conectado");
			return "";
		}
		try{
			// Respuesta del servidor
			byte[] inStream = new byte[1024];
			networkStream.Read(inStream, 0, inStream.Length);
			String response = encoder.GetString (inStream);
			
			//Console.WriteLine("Server response: " + response);
			
			// Ack
			/**
			serverStream = clientSocket.GetStream();
			byte[] outStream = encoder.GetBytes("ok$");;
			serverStream.Write(outStream, 0, outStream.Length);
			serverStream.Flush();
			**/
			return response;
		}catch (Exception e) {
			Debug.Log("Error..... " + e.StackTrace);
			return ("Error..... " + e.StackTrace);
		}

	}

	public bool getConnected(){
		return connected;
	}

	public void closeConnection() {
		/* clean up */
		clientSocket.Close();
	}

}
