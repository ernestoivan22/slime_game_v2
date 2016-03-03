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
				movex = 1;	
			} else if (Input.GetKey (KeyCode.LeftArrow)) {
				movex = -1;	
			} else {
				movex = 0;
			}
		}

	}

	void FixedUpdate () {
		move = Input.GetAxis ("Horizontal");
		// 
		if (PlayerPrefs.GetInt ("esHost") == 1) {
			if (Input.GetKey (KeyCode.UpArrow)  && CanJump) {
				rigidbody2D.AddForce (new Vector2 (0,JumpForce));
				CanJump = false;
				JumpTime  = MaxJumpTime;
			}
			socketController_1.setP1Velocity (rigidbody2D.velocity.x, rigidbody2D.velocity.y);
			socketController_1.setP1Position (rigidbody2D.transform.position.x, rigidbody2D.transform.position.y);
			rigidbody2D.velocity = new Vector2 (movex * Speed, movey * Speed);

		} else {
			float vX = socketController_2.getP1VelocityX();
			float vY = socketController_2.getP1VelocityY();
			float pX = socketController_2.getP1PositionX();
			float pY = socketController_2.getP1PositionY();

			rigidbody2D.transform.position = new Vector3(pX, pY, 0);
			rigidbody2D.velocity = new Vector2(vX, vY);
		}

	}

	void OnLevelWasLoaded(){
		//socketController = (socketController1)GameObject.Find ("socketController1");
	}
}
