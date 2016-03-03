using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour {
	public gameManager gManager;
	public int p1Score;
	public int p2Score;
	public socketController2 socketController_2;
	public socketController1 socketController_1;

	// Use this for initialization
	void Start () {
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
		if (PlayerPrefs.GetInt ("esHost") == 0) {
			if (socketController_2.getRecibirScore()) {
				gManager.AddScore(socketController_2.getJugadorScore());
				socketController_2.setRecibirScore(false);
			}
		}
	
	}

	/*void onTriggerEnter2D(Collider2D col){
		print ("choque!");
		if (col.gameObject.tag == "ball") {
			print ("tierra!");
		}
	}*/
	void OnCollisionEnter2D(Collision2D collision) {
		if (PlayerPrefs.GetInt ("esHost") == 1) {
			ContactPoint2D[] contacto = collision.contacts;
			Vector2 puntos = contacto [0].point;
			if (collision.gameObject.name == "ball") {
					if (puntos.x < 0) {
						socketController_1.setJugadorScore(1);
						socketController_1.setMandarScore(true);
						gManager.AddScore (1);
					} else {
						socketController_1.setJugadorScore(0);
						socketController_1.setMandarScore(true);
						gManager.AddScore (0);
					}
			}


		}
	}


}
