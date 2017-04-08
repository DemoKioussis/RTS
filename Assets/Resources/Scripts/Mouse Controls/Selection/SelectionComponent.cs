using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using System.Text;

public class SelectionComponent : MonoBehaviour {

	public LayerMask layerMask;
	bool isSelecting = false;
	Vector3 mousePosition1;

	public GameObject selectionCirclePrefab;
	List<Unit> selectedUnits = new List<Unit> ();
	List<Building> selectedBuildings = new List<Building> ();
	public UnitGroup selectedUnitGroup;
	public BuildingGroup selectedBuildingGroup;
	public UnitGroup selectedUnitGroupPrefab;
	public BuildingGroup selectedBuildingGroupPrefab;

	bool previousInputLeftClick;
	Vector3 clickPosition;
	
	// Update is called once per frame
	void Update () {

		// If we press the left mouse button, begin selection and remember the location of the mouse
		if( Input.GetMouseButtonDown( 0 ) )
		{
			clickPosition = Input.mousePosition;

			if (previousInputLeftClick && selectedUnitGroup != null)
				Destroy(selectedUnitGroup.gameObject);
			if (previousInputLeftClick && selectedBuildingGroup != null)
				Destroy(selectedBuildingGroup.gameObject);

			previousInputLeftClick = true;
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

			RaycastHit hitInfo = Utils.GetPositionFromMouseClick(layerMask);
			if (hitInfo.collider != null) {
				if (hitInfo.collider.gameObject.GetComponent<RTSObject> () != null) {
					RTSObject selectableObject = hitInfo.collider.gameObject.GetComponent<RTSObject> ();
					if( selectableObject.selectionCircle == null )
					{
						selectableObject.selectionCircle = Instantiate( selectionCirclePrefab , Vector3.zero, Quaternion.identity);
						selectableObject.selectionCircle.GetComponent<SizeBasedOnObject> ().SetSize (selectableObject.GetComponent<MeshRenderer>().bounds);
						selectableObject.selectionCircle.transform.SetParent( selectableObject.transform, false );
						selectableObject.selectionCircle.transform.eulerAngles = new Vector3( 90, 0, 0 );
						if (selectableObject.GetComponent<Building> () != null) {
							selectedBuildings.Add (selectableObject.GetComponent<Building> ());
							selectedBuildingGroup.Add (selectableObject.GetComponent<Building> ());
						}
						else if (selectableObject.GetComponent<Unit> () != null) {
							selectedUnits.Add (selectableObject.GetComponent<Unit> ());
							selectedUnitGroup.Add (selectableObject.GetComponent<Unit> ());
						}
					}
				}
			}

			Debug.Log (selectedBuildingGroup.rtsObjects);

		}
		// If we let go of the left mouse button, end selection
		if( Input.GetMouseButtonUp( 0 ) )
		{
			//Debug.Log (selectedBuildingGroup.IsEmpty());
			if (selectedUnitGroup.IsEmpty())
				Destroy(selectedUnitGroup.gameObject);
			if (selectedBuildingGroup.IsEmpty())
				Destroy(selectedBuildingGroup.gameObject);
			
			if (clickPosition == Input.mousePosition) {
				selectedUnits = new List<Unit>();
				selectedBuildings = new List<Building>();
				foreach( var selectableObject in FindObjectsOfType<RTSObject>() )
				{
					if( IsWithinSelectionBounds( selectableObject.gameObject ) )
					{
						if (selectableObject.GetComponent<Building> () != null) {
							selectedBuildings.Add (selectableObject.GetComponent<Building> ());
							selectedBuildingGroup.Add (selectableObject.GetComponent<Building> ());
						}
						else if (selectableObject.GetComponent<Unit> () != null) {
							selectedUnits.Add (selectableObject.GetComponent<Unit> ());
							selectedUnitGroup.Add (selectableObject.GetComponent<Unit> ());
						}
					}
				}
			}

			/*
			var sb = new StringBuilder();
			sb.AppendLine( string.Format( "Selecting [{0}] Objects", selectedObjects.Count ) );
			foreach( var selectedObject in selectedObjects )
				sb.AppendLine( "-> " + selectedObject.gameObject.name );
			Debug.Log( sb.ToString() );
			*/

			isSelecting = false;
		}

		// Highlight all objects within the selection box
		if( isSelecting )
		{
			foreach( var selectableObject in FindObjectsOfType<RTSObject>() )
			{
				if( IsWithinSelectionBounds( selectableObject.gameObject ) )
				{
					if( selectableObject.selectionCircle == null )
					{
						selectableObject.selectionCircle = Instantiate( selectionCirclePrefab , Vector3.zero, Quaternion.identity);
						selectableObject.selectionCircle.GetComponent<SizeBasedOnObject>().SetSize(selectableObject.getModel().bounds);
						selectableObject.selectionCircle.transform.SetParent( selectableObject.transform, false );
						selectableObject.selectionCircle.transform.eulerAngles = new Vector3( 90, 0, 0 );
						if (selectableObject.GetComponent<Building> () != null) {
							selectedBuildings.Add (selectableObject.GetComponent<Building> ());
							selectedBuildingGroup.Add (selectableObject.GetComponent<Building> ());
						}
						else if (selectableObject.GetComponent<Unit> () != null) {
							selectedUnits.Add (selectableObject.GetComponent<Unit> ());
							selectedUnitGroup.Add (selectableObject.GetComponent<Unit> ());
						}
					}
				}
				else if (clickPosition != Input.mousePosition)
				{
					if( selectableObject.selectionCircle != null )
					{
						Destroy( selectableObject.selectionCircle.gameObject );
						selectableObject.selectionCircle = null;
					}
					if (selectableObject.GetComponent<Building> () != null) {
						selectedBuildings.Remove (selectableObject.GetComponent<Building> ());
					}
					else if (selectableObject.GetComponent<Unit> () != null) {
						selectedUnits.Remove (selectableObject.GetComponent<Unit> ());
					}
				}
			}
		}


		if (Input.GetMouseButtonDown(1))
		{
			previousInputLeftClick = false;
			RaycastHit hitInfo = Utils.GetPositionFromMouseClick(layerMask);

			Interactable interactable;
			if (hitInfo.collider != null)
			{
				interactable = hitInfo.collider.GetComponent<Interactable>();
				if (interactable != null) {
					Debug.Log("Interacted with: " + interactable.name + " type: " + interactable.getInteractionType());
					if (selectedUnitGroup != null) {
						InteractionSetter (interactable, hitInfo.point);
						selectedUnitGroup.InteractWith (interactable);
					}
					else if (selectedBuildingGroup != null) {
						InteractionSetter (interactable, hitInfo.point);
						selectedBuildingGroup.InteractWith (interactable);
					}
				}
			}
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

	void OnGUI()
	{
		if( isSelecting )
		{
			// Create a rect from both mouse positions
			var rect = Utils.GetScreenRect( mousePosition1, Input.mousePosition );
			Utils.DrawScreenRect( rect, new Color( 0.8f, 0.8f, 0.95f, 0.25f ) );
			Utils.DrawScreenRectBorder( rect, 2, new Color( 0.8f, 0.8f, 0.95f ) );
		}
	}
}
