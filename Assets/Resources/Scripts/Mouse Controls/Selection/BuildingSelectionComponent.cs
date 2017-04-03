using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Text;

public class BuildingSelectionComponent : MonoBehaviour
{
	public LayerMask layerMask;
	bool isSelecting = false;
	Vector3 mousePosition1;

	public GameObject selectionCirclePrefab;
	List<SelectableBuildingComponent> selectedBuildings = new List<SelectableBuildingComponent>();
	BuildingGroupController selectedGroup;
	public BuildingGroupController buildingGroupControllerPrefab;

	bool previousInputLeftClick;

	void Update()
	{
		// If we press the left mouse button, begin selection and remember the location of the mouse
		if( Input.GetMouseButtonDown( 0 ) )
		{
			if (previousInputLeftClick && selectedGroup != null)
			{
				Destroy(selectedGroup.gameObject);
			}

			previousInputLeftClick = true;
			selectedGroup = Instantiate(buildingGroupControllerPrefab, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
			selectedBuildings = new List<SelectableBuildingComponent>();
			isSelecting = true;
			mousePosition1 = Input.mousePosition;

			foreach( var selectableObject in FindObjectsOfType<SelectableBuildingComponent>() )
			{
				if( selectableObject.selectionCircle != null )
				{
					Destroy( selectableObject.selectionCircle.gameObject );
					selectableObject.selectionCircle = null;
				}
			}
		}
		// If we let go of the left mouse button, end selection
		if( Input.GetMouseButtonUp( 0 ) )
		{
			if (selectedGroup.isEmpty())
				Destroy(selectedGroup.gameObject);

			selectedBuildings = new List<SelectableBuildingComponent>();
			foreach( var selectableObject in FindObjectsOfType<SelectableBuildingComponent>() )
			{
				if( IsWithinSelectionBounds( selectableObject.gameObject ) )
				{
					selectedBuildings.Add( selectableObject );
				}
			}

			var sb = new StringBuilder();
			sb.AppendLine( string.Format( "Selecting [{0}] Units", selectedBuildings.Count ) );
			foreach( var selectedBuilding in selectedBuildings )
				sb.AppendLine( "-> " + selectedBuilding.gameObject.name );
			Debug.Log( sb.ToString() );

			isSelecting = false;
		}

		// Highlight all objects within the selection box
		if( isSelecting )
		{
			foreach( var selectableObject in FindObjectsOfType<SelectableBuildingComponent>() )
			{
				if( IsWithinSelectionBounds( selectableObject.gameObject ) )
				{
					if( selectableObject.selectionCircle == null )
					{
						selectableObject.selectionCircle = Instantiate( selectionCirclePrefab );
						selectableObject.selectionCircle.transform.SetParent( selectableObject.transform, false );
						selectableObject.selectionCircle.transform.eulerAngles = new Vector3( 90, 0, 0 );
						if (selectableObject.interactable.getInteractionType() == INTERACTION_TYPE.BUILDING)
							selectedGroup.add(selectableObject.GetComponent<BuildingController>());
					}
				}
				else
				{
					if( selectableObject.selectionCircle != null )
					{
						Destroy( selectableObject.selectionCircle.gameObject );
						selectableObject.selectionCircle = null;
					}
					selectedGroup.remove(selectableObject.GetComponent<BuildingController>());
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
					Debug.Log("Interacted with: " + interactable.name);
					Debug.Log (interactable.getInteractionType());
					if (selectedGroup != null) {
						InteractionSetter (interactable, hitInfo.point);
						selectedGroup.interactWith (interactable);
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