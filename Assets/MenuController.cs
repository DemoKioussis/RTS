using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour {

	// Use this for initialization
	public void Quit () {
		Application.Quit ();
	}
	
	// Update is called once per frame
	public void GoToGame () {
		Application.LoadLevel("Final");
	}
}
