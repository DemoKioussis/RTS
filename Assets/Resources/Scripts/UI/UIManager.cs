﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	public GameObject hudPanel; // contains all UI elements of the game
	private Text infoStats;
	private GameObject progressBar;

	private PlayerContext player;
	private Text[] playerStats;

	private GameObject selectedObjectInfoPanel;
	private Interactable selectedObject;

	// Use this for initialization
	void Awake () {
		hudPanel = GameObject.FindGameObjectWithTag ("HUDPanel");
		infoStats = GameObject.FindGameObjectWithTag ("InfoStats").GetComponent<Text>();
		player = GetComponentInParent<PlayerContext> ();
		playerStats = GameObject.FindGameObjectWithTag ("PlayerStats").GetComponentsInChildren<Text>();
		progressBar = GameObject.FindGameObjectWithTag ("UIProgress");
		progressBar.SetActive (false);
	}
	 
	void FixedUpdate(){
		if (selectedObject != null) {
			GetInformation (selectedObject);
		}

		GetPlayerInformation ();
	}

	// Add interactable object to the info panel
	public void AddToInfoPanel(Interactable selectedGameObj){
		if (selectedGameObj == null) {
			return;
		}

		GameObject infoPanel = selectedGameObj.GetInfoPanel ();
		selectedObjectInfoPanel = (GameObject)Instantiate (infoPanel, Vector3.zero, infoPanel.transform.rotation, hudPanel.transform);

		// I had to hard code this value...
		selectedObjectInfoPanel.GetComponent<RectTransform>().anchoredPosition = new Vector3 (100, 0, 0);

		selectedObject = selectedGameObj; // keep track of the object

		GetInformation (selectedObject);
	}

	public void ClearInfoPanel(){
		// clear everything
		if (selectedObject != null && selectedObjectInfoPanel != null) {
			Destroy (selectedObjectInfoPanel.gameObject);
		}
		selectedObject = null;
		selectedObjectInfoPanel = null;
		infoStats.text = "";
		progressBar.SetActive (false);

	}

	public void GetInformation(Interactable selectedObj){
		switch (selectedObj.getInteractionType())
		{
		case INTERACTION_TYPE.BUILDING:
			infoStats.text = selectedObj.GetStats ().hitpoints + "/" + selectedObj.GetStats ().maxHitpoints + " hp";
			if (selectedObj.GetComponent<TrainingBuilding> () != null) {
				progressBar.SetActive (true);
				progressBar.GetComponent<Image>().fillAmount = selectedObj.GetComponent<TrainingBuilding> ().progress / 100.0f;
			}

			break;
		case INTERACTION_TYPE.POSITION:
			infoStats.text = "";			
			break;
		case INTERACTION_TYPE.UNIT:
			infoStats.text = selectedObj.GetStats().hitpoints + "/" + selectedObj.GetStats ().maxHitpoints + " hp";
			break;
		case INTERACTION_TYPE.RESOURCE:
			{
				ResourceStats stats = (ResourceStats) selectedObj.GetStats ();
				infoStats.text = stats.quantity + " " + stats.type + " left";
				break;
			}
		}
	}

	private void GetPlayerInformation(){
		// 0 is paper
		// 1 is glue
		// 2 is popuplation
		playerStats [0].text = "Paper: " + player.paperQuantity;
		playerStats [1].text = "Glue: " + player.glueQuantity;
		playerStats [2].text = "Pop: " + player.population + "/" + player.populationLimit;
	}

}
