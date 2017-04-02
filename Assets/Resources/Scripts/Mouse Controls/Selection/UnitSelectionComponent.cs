using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Text;

public class UnitSelectionComponent : MonoBehaviour
{
	public LayerMask layerMask;
    bool isSelecting = false;
    Vector3 mousePosition1;

    public GameObject selectionCirclePrefab;
    List<SelectableUnitComponent> selectedUnits = new List<SelectableUnitComponent>();
    UnitGroupController selectedGroup;
    public UnitGroupController unitGroupControllerPrefab;

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
            selectedGroup = Instantiate(unitGroupControllerPrefab, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
            selectedUnits = new List<SelectableUnitComponent>();
            isSelecting = true;
            mousePosition1 = Input.mousePosition;

            foreach( var selectableObject in FindObjectsOfType<SelectableUnitComponent>() )
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

            selectedUnits = new List<SelectableUnitComponent>();
            foreach( var selectableObject in FindObjectsOfType<SelectableUnitComponent>() )
            {
                if( IsWithinSelectionBounds( selectableObject.gameObject ) )
                {
                    selectedUnits.Add( selectableObject );
                }
            }

            var sb = new StringBuilder();
            sb.AppendLine( string.Format( "Selecting [{0}] Units", selectedUnits.Count ) );
            foreach( var selectedUnit in selectedUnits )
                sb.AppendLine( "-> " + selectedUnit.gameObject.name );
            Debug.Log( sb.ToString() );

            isSelecting = false;
        }

        // Highlight all objects within the selection box
        if( isSelecting )
        {
            foreach( var selectableObject in FindObjectsOfType<SelectableUnitComponent>() )
            {
                if( IsWithinSelectionBounds( selectableObject.gameObject ) )
                {
                    if( selectableObject.selectionCircle == null )
                    {
                        selectableObject.selectionCircle = Instantiate( selectionCirclePrefab );
                        selectableObject.selectionCircle.transform.SetParent( selectableObject.transform, false );
                        selectableObject.selectionCircle.transform.eulerAngles = new Vector3( 90, 0, 0 );
                        if (selectableObject.interactable.getInteractionType() == INTERACTION_TYPE.UNIT)
                            selectedGroup.add(selectableObject.GetComponent<UnitController>());
                    }
                }
                else
                {
                    if( selectableObject.selectionCircle != null )
                    {
                        Destroy( selectableObject.selectionCircle.gameObject );
                        selectableObject.selectionCircle = null;
                    }
                    selectedGroup.remove(selectableObject.GetComponent<UnitController>());
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