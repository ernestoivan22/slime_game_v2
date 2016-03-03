using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour {
	public gameManager gManager;
	public int p1Score;
	public int p2Score;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	/*void onTriggerEnter2D(Collider2D col){
		print ("choque!");
		if (col.gameObject.tag == "ball") {
			print ("tierra!");
		}
	}*/
	void OnCollisionEnter2D(Collision2D collision) {
		ContactPoint2D[] contacto = collision.contacts;
		Vector2 puntos = contacto[0].point;
		if(collision.gameObject.name == "ball"){
			if(puntos.x < 0){
				gManager.AddScore(1);
			}
			else{
				gManager.AddScore(0);
			}
		}
	}


}
