using UnityEngine;
using System.Collections;

public class wait_connection : MonoBehaviour {
	public Texture backgroundTexture;
	public socketController1 miSocketC;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (miSocketC.getCreado() == true) {
			Application.LoadLevel(1);
		}
	}

	void OnGUI(){
		GUI.DrawTexture (new Rect(0,0,Screen.width,Screen.height),backgroundTexture);
	}
}
