using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	private GameObject hudPanel; // contains all UI elements of the game
	private Text infoStats;

	private GameObject selectedObjectInfoPanel;
	private Interactable selectedObject;

	// Use this for initialization
	void Start () {
		hudPanel = GameObject.FindGameObjectWithTag ("HUDPanel");
		infoStats = GameObject.FindGameObjectWithTag ("InfoStats").GetComponent<Text>();
	}
	 
	void Update(){
		if (selectedObject != null) {
			GetInformation (selectedObject);
		}
	}

	// Add interactable object to the info panel
	public void AddToInfoPanel(Interactable selectedGameObj){
		if (selectedGameObj == null) {
			return;
		}

		selectedObjectInfoPanel = (GameObject)Instantiate (selectedGameObj.GetInfoPanel (), hudPanel.transform);
		selectedObject = selectedGameObj; // keep track of the object

		GetInformation (selectedObject);
	}

	public void ClearInfoPanel(){
		// clear everything
		Destroy(selectedObjectInfoPanel.gameObject);
		selectedObject = null;
		selectedObjectInfoPanel = null;
	}

	public void GetInformation(Interactable selectedObj){
		switch (selectedObj.getInteractionType())
		{
		case INTERACTION_TYPE.BUILDING:
			infoStats.text = selectedObj.GetStats ().hitpoints + " hp";
			break;
		case INTERACTION_TYPE.POSITION:
			infoStats.text = "";			
			break;
		case INTERACTION_TYPE.UNIT:
			infoStats.text = selectedObj.GetStats().hitpoints + " hp";
			break;
		case INTERACTION_TYPE.RESOURCE:
			{
				ResourceStats stats = (ResourceStats) selectedObj.GetStats ();
				infoStats.text = stats.quantity + " " + stats.type + " left";
				break;
			}
		}
	}
}
