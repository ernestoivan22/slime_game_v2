using UnityEngine;
using System.Collections;
using System.Threading;
using System;

public class socketController2 : MonoBehaviour {
	static Client tcpCliente;
	Thread mThread, mThread2;
	bool connected = false;
	string ipObtenida;
	bool running;
	float p1VelocityX = 0, p1VelocityY = 0, bVelocityX = 0, bVelocityY = 0, p2VelocityX, p2VelocityY;
	float p1PositionX = -2, p1PositionY = -2, bPositionX = -2, bPositionY = 0, p2PositionX, p2PositionY;
	float p1F = 0, p2F;
	bool recibirP1 = false, recibirF1 = false, recibirB = false, mandarP2 = false, mandarF2 = false;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if(PlayerPrefs.GetInt("pressed1") == 1){
			PlayerPrefs.SetInt("pressed1", 0);
			ipObtenida = PlayerPrefs.GetString("ipObtenido");
			ThreadStart ts = new ThreadStart(threadCliente);
			ThreadStart ts2 = new ThreadStart(threadCliente2);
			mThread = new Thread(ts);
			mThread2 = new Thread(ts2);
			running = true;
			mThread.Start();
			mThread2.Start();
			print("Thread done...");

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

	void Awake(){
		DontDestroyOnLoad (transform.gameObject);
	}

	void threadCliente(){

		tcpCliente = new Client(ipObtenida);
		connected = tcpCliente.getConnected ();
		if (!connected) {
			print ("Fallido");
			mThread.Abort();
		}

		string data;
		string[] serverResponse, responseSeparation;
		tcpCliente.sendData ("Hello");
		while (running) {
			Debug.Log("Recibiendo por primera vez...");
			data = tcpCliente.receiveData();
			Debug.Log("Recibido: " + data);

			try {
				//Debug.Log (data);
				serverResponse = data.Split(':');
				if(serverResponse[0].Equals("P1")){
					responseSeparation = serverResponse[1].Split('|');
					p1VelocityX = float.Parse(responseSeparation[0]);
					p1VelocityY = float.Parse(responseSeparation[1]);
					p1PositionX = float.Parse(responseSeparation[2]);
					p1PositionY = float.Parse(responseSeparation[3]);
					recibirP1 = true;
				}
				else if(serverResponse[0].Equals("F1")){
					p1F = float.Parse(serverResponse[1]);
					recibirF1 = true;
				}
				else if(serverResponse[0].Equals("B")){
					responseSeparation = serverResponse[1].Split('|');
					bVelocityX = float.Parse(responseSeparation[0]);
					bVelocityY = float.Parse(responseSeparation[1]);
					bPositionX = float.Parse(responseSeparation[2]);
					bPositionY = float.Parse(responseSeparation[3]);
					recibirB = true;
				}
			} catch (Exception e) {
				Debug.Log(e.Message.ToString());
			}

		}

		tcpCliente.closeConnection ();
		mThread.Abort ();
	}

	public void threadCliente2(){
		while (!connected) {
			Debug.Log("No conectado con server aun..");
		}

		string data;
		while (running) {
			try {
				if (mandarP2) {
					data = "P2:" + p2VelocityX + "|" + p2VelocityY + "|" + p2PositionX + "|" + p2PositionY;
					tcpCliente.sendData(data);
					mandarP2 = false;
				}
				if(mandarF2){
					data = "F2:" + p2F;
					tcpCliente.sendData(data);
					mandarF2 = false;
				}
			} catch (Exception e) {
				Debug.Log(e.Message.ToString());
			}
		}
		mThread2.Abort ();
	}

	public bool getConnected(){
		return connected;
	}

	public void setP2Velocity(float x, float y){
		p2VelocityX = x;
		p2VelocityY = y;
	}

	public void setP2Position(float x, float y) {
		p2PositionX = x;
		p2PositionY = y;
	}

	public void setMandarP2(bool mP2){
		mandarP2 = mP2;
	}

	public void setMandarF2(bool mF2){
		mandarF2 = mF2;
	}

	public void setRecibirP1(bool rP1){
		recibirP1 = rP1;
	}

	public void setRecibirF1(bool rF1){
		recibirF1 = rF1;
	}

	public void setRecibirB(bool rB){
		recibirB = rB;
	}

	public void setP2F(float fuerza){
		p2F = fuerza;
	}

	public float getP1VelocityX(){
		return p1VelocityX;
	}
	
	public float getP1VelocityY(){
		return p1VelocityY;
	}

	public float getP1PositionX(){
		return p1PositionX;
	}
	
	public float getP1PositionY(){
		return p1PositionY;
	}

	public float getBVelocityX(){
		return bVelocityX;
	}
	
	public float getBVelocityY(){
		return bVelocityY;
	}

	public float getBPositionX(){
		return bPositionX;
	}
	
	public float getBPositionY(){
		return bPositionY;
	}

	public float getP1F(){
		return p1F;
	}

	public bool getRecibirP1(){
		return recibirP1;
	}

	public bool getRecibirF1(){
		return recibirF1;
	}

	public bool getRecibirB(){
		return recibirB;
	}
}
