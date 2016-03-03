using UnityEngine;
using System.Collections;

public class Player2Control : MonoBehaviour {
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
		if(PlayerPrefs.GetInt("esHost")==0){
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
			if(socketController_1.getRecibirP2()){

				movex = socketController_1.getP2VelocityX();
				socketController_1.setRecibirP2(false);
				sv = true;
			}
		}
		
	}
	
	void FixedUpdate () {
		move = Input.GetAxis ("Horizontal");
		//rigidbody2D.velocity = new Vector2 (move * Speed, rigidbody2D.velocity.y);
		if (PlayerPrefs.GetInt ("esHost") == 0) {
			if(m1){
				m1 = false;
				socketController_2.setP2Velocity (1, 0);
				socketController_2.setP2Position (rigidbody2D.transform.position.x, rigidbody2D.transform.position.y);
				socketController_2.setMandarP2(true);
				
			}
			if(m2){
				m2 = false;
				socketController_2.setP2Velocity (-1, 0);
				socketController_2.setP2Position (rigidbody2D.transform.position.x, rigidbody2D.transform.position.y);
				socketController_2.setMandarP2(true);
			}
			if(m3){
				m3 = false;
				socketController_2.setP2Velocity (0, 0);
				socketController_2.setP2Position (rigidbody2D.transform.position.x, rigidbody2D.transform.position.y);
				socketController_2.setMandarP2(true);
			}
			if (Input.GetKey (KeyCode.UpArrow)  && CanJump) {
				//Mandar Fuerza
				socketController_2.setP2F(JumpForce);
				socketController_2.setMandarF2(true);
				rigidbody2D.AddForce (new Vector2 (0,JumpForce));
				CanJump = false;
				JumpTime  = MaxJumpTime;
			}
			//socketController_2.setP2Velocity(rigidbody2D.velocity.x, rigidbody2D.velocity.y);
			//socketController_2.setP2Position(rigidbody2D.transform.position.x, rigidbody2D.transform.position.y);
			rigidbody2D.velocity = new Vector2 (movex * Speed, movey * Speed);
		} else{
			if(sv){
				float pX = socketController_1.getP2PositionX();
				float pY = socketController_1.getP2PositionY();
				rigidbody2D.transform.position = new Vector3(pX, pY, 0);
				sv = false;
			}
			if(socketController_1.getRecibirF2()){
				float f2 = socketController_1.getP2F();
				socketController_1.setRecibirF2(false);
				rigidbody2D.AddForce (new Vector2 (0,f2));
				CanJump = false;
				JumpTime  = MaxJumpTime;
			}

			rigidbody2D.velocity = new Vector2 (movex * Speed, movey * Speed);
		}
		

	}
}
