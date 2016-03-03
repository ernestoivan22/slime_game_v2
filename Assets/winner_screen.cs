using UnityEngine;
using System.Collections;

public class winner_screen : MonoBehaviour {
	public Texture backgroundTexture1;
	public Texture backgroundTexture2;

	void OnGUI(){
		if (PlayerPrefs.GetInt ("p1Won") == 1) {
			GUI.DrawTexture (new Rect(0, 0, Screen.width, Screen.height), backgroundTexture1);
		}
		else{
			GUI.DrawTexture (new Rect(0, 0, Screen.width, Screen.height), backgroundTexture2);
		}
		//botones
		if(GUI.Button(new Rect(Screen.width*0.25f, Screen.height * 0.5f, Screen.width*0.5f, Screen.height*0.1f), "Aceptar")){
			Application.LoadLevel(0);
		}
	}
}
