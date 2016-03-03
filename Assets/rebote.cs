using UnityEngine;
using System.Collections;

public class rebote : MonoBehaviour {
	// Constant speed of the ball
	private float speed = 4f;
	private float maxSpeed = 12f;
	private float vX;
	private float vY;
	public socketController1 socketController_1;
	public socketController2 socketController_2;
	
	// Keep track of the direction in which the ball is moving
	private Vector2 velocity;
	
	// used for velocity calculation
	private Vector2 lastPos;

	// Use this for initialization
	void Start () {
		// Random direction
		rigidbody2D.velocity = new Vector2(0,0) * speed;
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
	
	}

	void FixedUpdate ()
	{
		//if (PlayerPrefs.GetInt ("esHost") == 1) {
			// Get pos 2d of the ball.
			Vector3 pos3D = transform.position;
			Vector2 pos2D = new Vector2(pos3D.x, pos3D.y);
			
			// Velocity calculation. Will be used for the bounce
			velocity = pos2D - lastPos;
			vX = rigidbody2D.velocity.x;
			vY = rigidbody2D.velocity.y;
			if (vY > maxSpeed) {
				vY = maxSpeed;
				rigidbody2D.velocity = new Vector2(vX, vY);
			}
			if (vX > maxSpeed) {
				vX = maxSpeed;
				rigidbody2D.velocity = new Vector2(vX, vY);
			}
			lastPos = pos2D;
			//socketController_1.setBVelocity(rigidbody2D.velocity.x, rigidbody2D.velocity.y);
			//socketController_1.setBPosition(rigidbody2D.transform.position.x, rigidbody2D.transform.position.y);
		//}
		if(PlayerPrefs.GetInt ("esHost") == 0) {
			if(socketController_2.getRecibirB()){
				vX = socketController_2.getBVelocityX();
				vY = socketController_2.getBVelocityY();
				float pX = socketController_2.getBPositionX();
				float pY = socketController_2.getBPositionY();
				socketController_2.setRecibirB(false);
				rigidbody2D.velocity = new Vector2(vX,vY);
				rigidbody2D.transform.position = new Vector3(pX, pY, 0);
			}
		}

	}
	
	private void OnCollisionEnter2D(Collision2D col)
	{
		if (PlayerPrefs.GetInt ("esHost") == 1) {
			// Normal
			Vector3 N = col.contacts[0].normal;
			//Direction
			Vector3 V = velocity.normalized;
			
			// Reflection
			Vector3 R = Vector3.Reflect(V, N).normalized * 2;
			
			// Assign normalized reflection with the constant speed
			vX = R.x;
			vY = R.y;
			if (vX > maxSpeed) {
				vX = maxSpeed;
			}
			if (vY > maxSpeed) {
				vY = maxSpeed;
			}
			rigidbody2D.velocity = new Vector2(vX, vY) * speed;
			socketController_1.setBVelocity (rigidbody2D.velocity.x, rigidbody2D.velocity.y);
			socketController_1.setBPosition(rigidbody2D.transform.position.x, rigidbody2D.transform.position.y);
			socketController_1.setMandarB(true);
			//rigidbody2D.AddForce
		}
	}
}
