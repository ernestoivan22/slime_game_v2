using UnityEngine;
using System.Collections;

public class connect_to_server : MonoBehaviour {

	public Texture backgroundTexture;
	public socketController2 miSocketC;
	int esHost;
	public string stringToEdit = "Ingrese IP a conectarse";

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (miSocketC.getConnected()) {
			Application.LoadLevel(1);
		}
	}

	void Awake(){
		PlayerPrefs.SetInt ("pressed1", 0);
	}

	void OnGUI(){
		GUI.DrawTexture (new Rect(0,0,Screen.width,Screen.height),backgroundTexture);
		//botones
		stringToEdit = GUI.TextField (new Rect(10,10,200,20), stringToEdit, 25);
		if(GUI.Button(new Rect(Screen.width*0.25f, Screen.height * 0.5f, Screen.width*0.5f, Screen.height*0.1f), "Connect")){

			PlayerPrefs.SetInt ("pressed1", 1);
			PlayerPrefs.SetString("ipObtenido",stringToEdit);
			//Application.LoadLevel(1);

			
		}
	}
}
