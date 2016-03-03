using UnityEngine;
using System.Collections;

public class Player1Control : MonoBehaviour {
	public socketController1 socketController_1;
	public socketController2 socketController_2;
	public float Speed = 0f;
	public float MaxJumpTime = 2f;
	public float JumpForce = 3f;
	private float move = 0f;
	private float JumpTime = 0f;
	private bool CanJump;
	private float movex = 0f;
	private float movey = 0f;
	private bool m1 = false, m2 = false, m3 = false, sv = false;

	void Start () {
		JumpTime  = MaxJumpTime;
		if (PlayerPrefs.GetInt ("esHost") == 1) {
			GameObject playerGameObj = GameObject.Find("socketController1");
			if (playerGameObj != null) {
				socketController_1 = playerGameObj.GetComponent<socketController1>();
			}
		}
		else{
			GameObject playerGameObj = GameObject.Find("socketController2");
			if (playerGameObj != null) {
				socketController_2 = playerGameObj.GetComponent<socketController2>();
			}
		}

	}

	// Update is called once per frame
	void Update () {
		if (!CanJump)
			JumpTime  -= Time.deltaTime;
		if (JumpTime <= 0)
		{
			CanJump = true;
			JumpTime  = MaxJumpTime;
		}
		if(PlayerPrefs.GetInt("esHost")==1){
			if (Input.GetKey (KeyCode.RightArrow)) {
				if(movex!=1){
					m1 = true;
				}
				movex = 1;	

			} else if (Input.GetKey (KeyCode.LeftArrow)) {
				if(movex!=-1){
					m2 = true;
				}
				movex = -1;	

			} else {
				if(movex!=0){
					m3 = true;
				}
				movex = 0;
			}
		}
		else{
			if(socketController_2.getRecibirP1()){
				movex = socketController_2.getP1VelocityX();
				socketController_2.setRecibirP1(false);
				sv = true;
			}
		}

	}

	void FixedUpdate () {
		move = Input.GetAxis ("Horizontal");
		// 
		if (PlayerPrefs.GetInt ("esHost") == 1) {
			//Mandar movimiento horizontal
			if(m1){
				m1 = false;
				socketController_1.setP1Velocity (1, 0);
				socketController_1.setP1Position (rigidbody2D.transform.position.x, rigidbody2D.transform.position.y);
				socketController_1.setMandarP1(true);

			}
			if(m2){
				m2 = false;
				socketController_1.setP1Velocity (-1, 0);
				socketController_1.setP1Position (rigidbody2D.transform.position.x, rigidbody2D.transform.position.y);
				socketController_1.setMandarP1(true);
			}
			if(m3){
				m3 = false;
				socketController_1.setP1Velocity (0, 0);
				socketController_1.setP1Position (rigidbody2D.transform.position.x, rigidbody2D.transform.position.y);
				socketController_1.setMandarP1(true);
			}
			if (Input.GetKey (KeyCode.UpArrow)  && CanJump) {
				//Mandar Salto
				socketController_1.setP1F(JumpForce);
				socketController_1.setMandarF1(true);
				rigidbody2D.AddForce (new Vector2 (0,JumpForce));
				CanJump = false;
				JumpTime  = MaxJumpTime;
			}
			//socketController_1.setP1Velocity (rigidbody2D.velocity.x, rigidbody2D.velocity.y);
			//socketController_1.setP1Position (rigidbody2D.transform.position.x, rigidbody2D.transform.position.y);
			rigidbody2D.velocity = new Vector2 (movex * Speed, movey * Speed);

		} else {
			if(sv){
				//float vX = socketController_2.getP1VelocityX();
				//float vY = socketController_2.getP1VelocityY();
				float pX = socketController_2.getP1PositionX();
				float pY = socketController_2.getP1PositionY();
				rigidbody2D.transform.position = new Vector3(pX, pY, 0);
				//rigidbody2D.velocity = new Vector2(vX, vY);
				sv = false;
			}
			if(socketController_2.getRecibirF1()){
				float f1 = socketController_2.getP1F();
				socketController_2.setRecibirF1(false);
				rigidbody2D.AddForce (new Vector2 (0,f1));
				CanJump = false;
				JumpTime  = MaxJumpTime;
			}
			rigidbody2D.velocity = new Vector2 (movex * Speed, movey * Speed);


		}

	}

	void OnLevelWasLoaded(){
		//socketController = (socketController1)GameObject.Find ("socketController1");
	}
}
