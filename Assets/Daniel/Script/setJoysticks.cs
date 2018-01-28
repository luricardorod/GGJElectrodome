using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class setJoysticks : MonoBehaviour {
	private string[] controllers = new string[4];
	private int ind = 0;
	private bool playable = false;
	private bool j1 = false, j2 = false, j3 = false, j4 = false;
	private bool p1Set = false, p2Set = false, p3Set = false, p4Set = false;

	Text playersTxt;
	Text startTxt;

	public GameObject playerText;
	public GameObject startT;
	public Image p1Img, p2Img, p3Img, p4Img ;
	public Material p1m, p2m, p3m, p4m;
	public Color p1Color, p2Color, p3Color, p4Color;
	public Texture p1tex, p2Tex, p3Tex, p4Tex;
	// Use this for initialization
	void Start () {
		playersTxt = playerText.gameObject.GetComponent<Text>();
		startTxt = startT.gameObject.GetComponent<Text>();
		//playersTxt.text = "Presiona A para seleccionar";

	}

	void Update () {
		if (Input.GetButton ("FlipMoveSetK")) {
			//controllers[ind] = "keyboard";
			//Debug.Log("Key pressed ");
		}else if (ind < 4) {
			if (Input.GetButton ("FlipMoveSet0") || Input.GetButton ("p1"))
				setControl("0");
			else if (Input.GetButton ("FlipMoveSet1") || Input.GetButton ("p2"))
				setControl("joy2");
			else if (Input.GetButton ("FlipMoveSet2") || Input.GetButton ("p3"))
				setControl("joy3");
			else if (Input.GetButton ("FlipMoveSet3") || Input.GetButton ("p4"))
				setControl("joy4");
		}
		if(playable){
			if (Input.GetButton ("FlipMoveSetK")){
				SceneManager.LoadScene("Game_Scene");
			}
		}
	}

	void setControl(string joy){
		Debug.Log ("setting");
		bool assign = false;
		if (joy == "joy1") {
			if (j1 == true)
				assign = false; //already assigned
			else {
				assign = true;
				j1 = true; //won't be reassigned
				//p1m.SetTexture(0,p1tex);
				//p1m.SetColor("_TintColor",p1Color);
				//p1Img.GetComponent<Image>().color = p1Color;// ("_TintColor", p1Color);

			}
		}
		if (joy == "joy2"){
			if (j2 == true)
				assign = false; //already assigned
			else {
				assign = true;
				j2 = true;

				//p2m.SetColor("_TintColor",p2Color);
				//p1Img.GetComponent<Material>().SetColor ("_TintColor", p2Color);
			}
		}
		if (joy == "joy3"){
			if(j3 == true)
				assign = false; //already assigned
			else{
				assign = true;
				j3 = true;
				//p3m.SetColor("_TintColor",p3Color);
				//p1Img.GetComponent<Renderer>().SetColor ("_TintColor", p3Color);
			}
		}
		if (joy == "joy4"){
			if(j4 == true)
					assign = false; //already assigned
			else{
				assign = true;
				//p4m.SetColor("_TintColor",p4Color);
				//p1Img.GetComponent<Material>().SetColor ("_TintColor", p4Color);
				j4 = true;
			}
		}
		if(assign){
			controllers[ind] = joy;
			ind++;
			playersTxt.text += "\nControl " + joy + " es player : " + ind;
			Debug.Log ("Control " + joy + " es player : " + ind);
			if(ind == 1)
				p1Set = true;
			if(ind == 2)
				p2Set = true;
			if(ind == 3)
				p3Set = true;
			if(ind == 4)
				p4Set = true;
		}
		if(p1Set){
			Debug.Log("Cambiar color");
			if(p1Img.GetComponent<Image>().color != null)
				p1Img.GetComponent<Image>().color = p1Color;// ("_TintColor", p1Color);
			//p1m.SetTexture(0,p1tex);
		}
                                               		if(p2Set){
			p2Img.GetComponent<Image>().color = p2Color;// ("_TintColor", p1Color);
			//p2m.SetTexture(0,p1tex);
		}
		if(p3Set){
			p3Img.GetComponent<Image>().color = p3Color;// ("_TintColor", p1Color);
			//p3m.SetTexture(0,p1tex);
		}
		if(p4Set){
			p4Img.GetComponent<Image>().color = p4Color;// ("_TintColor", p1Color);
			//p4m.SetTexture(0,p1tex);
		}
		if(ind >= 2){
			playable = true;
			startTxt.text = "Presiona start para empezar";
		}
	}
}
