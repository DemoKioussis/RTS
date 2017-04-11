using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	private GameObject hudPanel; // contains all UI elements of the game
	private GameObject gameInfoPanel;
	private Text name;
	private Text health;

	private Interactable selectedObject;
	private Stats selectedObjectStats;

	private bool isGroup;

	// Use this for initialization
	void Start () {
		isGroup = false;
		hudPanel = GameObject.FindGameObjectWithTag ("HUDPanel");
		gameInfoPanel = GameObject.FindGameObjectWithTag ("InfoPanel");
	}

	void Update(){
		if (selectedObject != null) {
		}
	}
	 
	// Add interactable object to the info panel
	public void AddToInfoPanel(Interactable selectedGameObj){
		if (selectedGameObj == null) {
			return;
		}

		isGroup = false;
		selectedObject = selectedGameObj;
		selectedObjectInfoPanel = (GameObject)Instantiate (selectedGameObj.GetInfoPanel (), hudPanel.transform);
		selectedObjectStats = selectedGameObj.GetStats();
	}

	public void ClearInfoPanel(){
		Destroy(selectedObjectInfoPanel.gameObject);
		selectedObjectInfoPanel = null;
		selectedObject = null;
		selectedObjectStats = null;
		isGroup = false;
	}
}
