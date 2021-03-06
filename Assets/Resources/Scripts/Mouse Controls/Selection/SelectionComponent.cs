﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using System.Text;

public class SelectionComponent : MonoBehaviour {

	public LayerMask layerMask;
	public bool isSelecting = false;
	Vector3 mousePosition1;

	public GameObject selectionCirclePrefab;
	List<Unit> selectedUnits = new List<Unit> ();
	List<Building> selectedBuildings = new List<Building> ();
	Resource selectedResource;
	public UnitGroup selectedUnitGroup;
	public BuildingGroup selectedBuildingGroup;
	public UnitGroup selectedUnitGroupPrefab;
	public BuildingGroup selectedBuildingGroupPrefab;
    PlayerContext player;

	bool previousInputLeftClick;
	Vector3 clickPosition;

	UIManager ui;
	Collider2D uiPanel;

    void Awake()
    {
        player = GetComponentInParent<PlayerContext>();
    }

	void Start(){
		ui = GameObject.FindGameObjectWithTag ("UIManager").GetComponent<UIManager> ();
		uiPanel = GameObject.FindGameObjectWithTag ("UIBoundary").GetComponent<Collider2D> ();
	}
	
	// Update is called once per frame
	void Update () {

		// If we press the left mouse button, begin selection and remember the location of the mouse
		if( Input.GetMouseButtonDown( 0 ) && !MouseInUI())
		{
			if (selectedUnitGroup != null && !selectedUnitGroup.isActivated())
				Destroy(selectedUnitGroup.gameObject);

			clickPosition = Input.mousePosition;

			if (selectedBuildingGroup != null)
				Destroy (selectedBuildingGroup.gameObject);

			selectedUnits = new List<Unit>();
			selectedBuildings = new List<Building>();
			selectedUnitGroup = Instantiate (selectedUnitGroupPrefab);
			selectedBuildingGroup = Instantiate (selectedBuildingGroupPrefab);
			isSelecting = true;
			mousePosition1 = Input.mousePosition;

			foreach( var selectableObject in FindObjectsOfType<RTSObject>() )
			{
				if( selectableObject.selectionCircle != null )
				{
					Destroy( selectableObject.selectionCircle.gameObject );
					selectableObject.selectionCircle = null;
				}
			}

			if (selectedResource != null) {
				Destroy(selectedResource.selectionCircle);
				selectedResource.selectionCircle = null;
			}

			ClearUI ();
		}

		// If we let go of the left mouse button, end selection
		if( Input.GetMouseButtonUp( 0 ) )
		{
			if (clickPosition != Input.mousePosition) {
				foreach( var selectableObject in FindObjectsOfType<RTSObject>() )
				{
					if( IsWithinSelectionBounds( selectableObject.gameObject ) )
					{
						if( selectableObject.selectionCircle == null )
						{
							selectableObject.selectionCircle = Instantiate( selectionCirclePrefab , Vector3.zero, Quaternion.identity);
							//selectableObject.selectionCircle.GetComponent<SizeBasedOnObject>().SetSize(selectableObject.getModel().bounds);
							selectableObject.selectionCircle.transform.SetParent( selectableObject.transform, false );
							selectableObject.selectionCircle.transform.eulerAngles = new Vector3( 0, 0, 0 );
							if (selectableObject.GetComponent<Building> () != null && selectableObject.GetComponent<Building>().player == player) {
								selectedBuildings.Add (selectableObject.GetComponent<Building> ());
								selectedBuildingGroup.Add (selectableObject.GetComponent<Building> ());
							}
							else if (selectableObject.GetComponent<Unit> () != null && selectableObject.GetComponent<Unit>().player == player) {
								selectedUnits.Add (selectableObject.GetComponent<Unit> ());
								selectedUnitGroup.Add (selectableObject.GetComponent<Unit> ());
							}
						}
					}
				}
				if (selectedUnitGroup != null && !selectedUnitGroup.IsEmpty () && (!MouseInUI() || isSelecting)) 
				{
					if (selectedUnitGroup.Count() > 1) {
						ui.AddToInfoPanel (selectedUnitGroup);
					} else {
						ui.AddToInfoPanel(selectedUnitGroup.rtsObjects[0]);
					}
				} 
				else if(selectedBuildingGroup != null && !selectedBuildingGroup.IsEmpty() && (!MouseInUI() || isSelecting))
				{
					if (selectedBuildingGroup.Count() > 1) {
						ui.AddToInfoPanel (selectedBuildingGroup);
					} else {
						ui.AddToInfoPanel(selectedBuildingGroup.rtsObjects[0]);
					}
				}
			}
			else {
				RaycastHit hitInfo = Utils.GetPositionFromMouseClick(layerMask);
				if (hitInfo.collider != null) {
					if (hitInfo.collider.gameObject.GetComponentInParent<RTSObject> () != null) {
						// structure of units have been changed, collider is now located in the selection target of the model
						RTSObject selectableObject = hitInfo.collider.gameObject.GetComponentInParent<RTSObject> ();
						if (selectableObject.selectionCircle == null) {
							selectableObject.selectionCircle = Instantiate (selectionCirclePrefab, Vector3.zero, Quaternion.identity);
							// selectableObject.selectionCircle.GetComponent<SizeBasedOnObject> ().SetSize (selectableObject.getModel ().bounds);
							selectableObject.selectionCircle.transform.SetParent (selectableObject.transform, false);
							selectableObject.selectionCircle.transform.eulerAngles = new Vector3 (0, 0, 0);
							if (selectableObject.GetComponent<Building> () != null && selectableObject.GetComponent<Building> ().player == player) {
								selectedBuildings.Add (selectableObject.GetComponent<Building> ());
								selectedBuildingGroup.Add (selectableObject.GetComponent<Building> ());

								ui.AddToInfoPanel (selectableObject); 

							} 
							else if (selectableObject.GetComponent<Unit> () != null && selectableObject.GetComponent<Unit> ().player == player) {
								selectedUnits.Add (selectableObject.GetComponent<Unit> ());
								selectedUnitGroup.Add (selectableObject.GetComponent<Unit> ());

								ui.AddToInfoPanel (selectableObject);
							}
						}
					} else if (hitInfo.collider.gameObject.GetComponent<Resource> () != null) {
						// clicked on resource
						Resource selectableObject = hitInfo.collider.gameObject.GetComponent<Resource> ();
						if (selectableObject.selectionCircle == null) {
							selectableObject.selectionCircle = Instantiate (selectionCirclePrefab, Vector3.zero, Quaternion.identity);
							// selectableObject.selectionCircle.GetComponent<SizeBasedOnObject> ().SetSize (selectableObject.getModel ().bounds);
							selectableObject.selectionCircle.transform.SetParent (selectableObject.transform, false);
							selectableObject.selectionCircle.transform.eulerAngles = new Vector3 (0, 0, 0);

							selectedResource = selectableObject;
							ui.AddToInfoPanel (selectableObject);
						}
					}
				}

			}
			if (selectedUnitGroup != null && selectedUnitGroup.IsEmpty())
				Destroy(selectedUnitGroup.gameObject);

			if (selectedBuildingGroup != null && selectedBuildingGroup.IsEmpty())
				Destroy(selectedBuildingGroup.gameObject);

			/*
		var sb = new StringBuilder();
		sb.AppendLine( string.Format( "Selecting [{0}] Objects", selectedObjects.Count ) );
		foreach( var selectedObject in selectedObjects )
			sb.AppendLine( "-> " + selectedObject.gameObject.name );
		Debug.Log( sb.ToString() );
		*/

			isSelecting = false;
		}

		if (Input.GetMouseButtonDown(1))
		{
			RaycastHit hitInfo = Utils.GetPositionFromMouseClick(layerMask);

			Interactable interactable;
			if (hitInfo.collider != null)
			{
				interactable = hitInfo.collider.GetComponent<InteractableLink>().getInteractable();
				if (interactable != null) {
					if (selectedUnitGroup != null) {
						InteractionSetter (interactable, hitInfo.point);
						selectedUnitGroup.InteractWith (interactable);
					}
                    else if (selectedBuildingGroup != null) {
						InteractionSetter (interactable, hitInfo.point);
						selectedBuildingGroup.InteractWith (interactable);
					}
				//	Debug.Log("Interacted with: " + interactable.name + " type: " + interactable.getInteractionType());
				}
			}

			isSelecting = false;
		}

	}

	void InteractionSetter(Interactable interaction, Vector3 position)
	{
		switch (interaction.getInteractionType ()) {

		case INTERACTION_TYPE.POSITION:
			{
				((MapPos)interaction).setPosition (position);
				break;
			}
		}
	}

	public bool IsWithinSelectionBounds( GameObject gameObject )
	{
		if( !isSelecting )
			return false;

		var camera = Camera.main;
		var viewportBounds = Utils.GetViewportBounds( camera, mousePosition1, Input.mousePosition );
		return viewportBounds.Contains( camera.WorldToViewportPoint( gameObject.transform.position ) );
	}

	public void ClearUI(){
		ui.ClearInfoPanel ();
	}

	void OnGUI()
	{
		if( isSelecting )
		{
			// Create a rect from both mouse positions
			var rect = Utils.GetScreenRect( mousePosition1, Input.mousePosition, uiPanel );
			Utils.DrawScreenRect( rect, new Color( 0.8f, 0.8f, 0.95f, 0.25f ) );
			Utils.DrawScreenRectBorder( rect, 2, new Color( 0.8f, 0.8f, 0.95f ) );
		}
	}

	bool MouseInUI()
	{
		if (uiPanel.bounds.Contains(Input.mousePosition))
			return true;

		return false;
	}
}
