using UnityEngine;
using System.Collections;

public class gameManager : MonoBehaviour {
	static int p1Score = 0;
	static int p2Score = 0;
	static bool p1Scored = false;
	int p1Won = 0;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void AddScore(int player){
		// Jugador del lado del server
		if (player == 0) {
			p1Score++;
			if(p1Score>=5) {
				p1Score = 0;
				p2Score = 0;
				p1Scored = false;
				p1Won = 1;
				PlayerPrefs.SetInt("p1Won",p1Won);
				//System.Threading.Thread.Sleep(100);
				Application.LoadLevel (2);
			}
			else{
				p1Scored = true;
				Application.LoadLevel(1);
			}
		}
		// Jugador del lado del cliente
		else{
			p2Score++;
			if(p2Score>=5) {
				p1Score = 0;
				p2Score = 0;
				p1Scored = false;
				p1Won = 0;
				PlayerPrefs.SetInt("p1Won",p1Won);
				Application.LoadLevel(2);
			}
			else{
				p1Scored = false;
				Application.LoadLevel(1);
			}
		}
	}

	public bool getPlayer1Scored(){
		return p1Scored;
	}

	public void checkScore() {

	}

	void OnGUI(){
		GUILayout.BeginArea(new Rect (0, 0, Screen.width, 20),"");  
		GUILayout.BeginHorizontal();
		GUILayout.FlexibleSpace();
		GUILayout.Label(p1Score + " - " + p2Score, GUILayout.Width(30)); 
		GUILayout.FlexibleSpace();
		GUILayout.EndHorizontal();
		GUILayout.EndArea();
		//GUI.Label()
	}
}
