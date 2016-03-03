using UnityEngine;
using System.Collections;
using System.Threading;
using System.Net.Sockets;
using System.IO;
using System;

public class socketController1 : MonoBehaviour {
	static bool creado = false;
	static Server tcpServer;
	float p1VelocityX, p1VelocityY, bVelocityX, bVelocityY, p2VelocityX = 0, p2VelocityY = 0;
	float p1PositionX, p1PositionY, bPositionX, bPositionY, p2PositionX = 2, p2PositionY = -2;
	float p1F, p2F = 0;
	bool mandarP1 = false, mandarF1 = false, mandarB = false, recibirP2 = false, recibirF2 = false;

	private bool running;
	Thread mThread, mThread2;

	//TcpListener tcp_Listener = null;
	// Use this for initialization
	void Start () {
		if (creado == false) {
			/*miServer = new Server();
			Application.LoadLevel(1);
			creado = true;		*/
			running = true;
			ThreadStart ts = new ThreadStart(threadServer);
			ThreadStart ts2 = new ThreadStart(threadServer2); 

			mThread = new Thread(ts);
			mThread2 = new Thread(ts2);
			mThread.Start();
			mThread2.Start();
			print("Threads done...");
		}
	}

	void OnApplicationQuit() {
		Debug.Log("Application exit");
		running = false;
	}

	void OnDestroy() {
		Debug.Log("Socket controller 1 destroyed");
		running = false;
	}

	void OnDisable() {
		Debug.Log("Socket controller 1 destroyed");
		running = false;
	}

	// Update is called once per frame
	void Update () {

	}

	void Awake(){
		DontDestroyOnLoad (transform.gameObject);
	}

	void threadServer()
	{
		tcpServer = new Server();
		creado = true;
		string data, instruccion;
		string[] clientResponse;
		string[] responseSeparation;

		while (running) {
			data = tcpServer.receiveData();
			//Debug.Log (data);
			clientResponse = data.Split(':');
			instruccion = clientResponse[0];
			if(instruccion.Equals("P2")){
				responseSeparation = clientResponse[1].Split('|');
				p2VelocityX = float.Parse(responseSeparation[0]);
				p2VelocityY = float.Parse(responseSeparation[1]);
				p2PositionX = float.Parse(responseSeparation[2]);
				p2PositionY = float.Parse(responseSeparation[3]);
				recibirP2 = true;
			}
			else if(instruccion.Equals("F2")){
				p2F = float.Parse(clientResponse[1]);
				recibirP2 = true;
			}

			//Debug.Log  ("p2VelocityX: " + p2VelocityX);
			//Debug.Log ("p2VelocityY: " + p2VelocityY);
			//Debug.Log ("asdfHoli");
			//Thread.Sleep(500);

		}

		tcpServer.closeConnection ();
		mThread.Abort ();
	}

	void threadServer2(){
		while (!creado) {
			Debug.Log("Thread server aun no creado");
		}

		string data;
		while (running) {
			if (mandarP1) {
				data = "P1:" + p1VelocityX + "|" + p1VelocityY + "|" + p1PositionX + "|" + p1PositionY;
				tcpServer.sendData(data);
				mandarP1 = false;
			}
			if(mandarF1){
				data = "F1:" + p1F;
				tcpServer.sendData(data);
				mandarF1 = false;
			}
			if(mandarB){
				data = "B:" + bVelocityX + "|" + bVelocityY + "|" + bPositionX + "|" + bPositionY;
				tcpServer.sendData(data);
				mandarB = false;
			}
		}

		mThread2.Abort ();
	}

	public bool getCreado(){
		return creado;
	}

	public void setP1Velocity(float x, float y) {
		p1VelocityX = x;
		p1VelocityY = y;
	}

	public void setP1Position(float x, float y) {
		p1PositionX = x;
		p1PositionY = y;
	}

	public void setBVelocity(float x, float y) {
		bVelocityX = x;
		bVelocityY = y;
	}

	public void setBPosition(float x, float y) {
		bPositionX = x;
		bPositionY = y;
	}

	public void setP1F(float force){
		p1F = force;
	}

	public void setMandarP1(bool mP1){
		mandarP1 = mP1;
	}

	public void setMandarF1(bool mF1){
		mandarF1 = mF1;
	}

	public void setMandarB(bool mB){
		mandarB = mB;
	}

	public void setRecibirP2(bool rP2){
		recibirP2 = rP2;
	}

	public void setRecibirF2(bool rF){
		recibirF2 = rF;
	}

	public float getP2VelocityX(){
		return p2VelocityX;
	}

	public float getP2VelocityY(){
		return p2VelocityY;
	}

	public float getP2PositionX() {
		return p2PositionX;
	}

	public float getP2PositionY() {
		return p2PositionY;
	}

	public float getP2F(){
		return p2F;
	}

	public bool getRecibirP2(){
		return recibirP2;
	}

	public bool getRecibirF2(){
		return recibirF2;
	}

}
