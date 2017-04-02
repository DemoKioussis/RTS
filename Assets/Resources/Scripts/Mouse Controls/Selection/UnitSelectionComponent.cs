using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Text;

public class UnitSelectionComponent : MonoBehaviour
{
    bool isSelecting = false;
    Vector3 mousePosition1;

    public GameObject selectionCirclePrefab;
    List<SelectableUnitComponent> selectedUnits = new List<SelectableUnitComponent>();

    void Update()
    {
        // If we press the left mouse button, begin selection and remember the location of the mouse
        if( Input.GetMouseButtonDown( 0 ) )
        {
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
                    }
                }
                else
                {
                    if( selectableObject.selectionCircle != null )
                    {
                        Destroy( selectableObject.selectionCircle.gameObject );
                        selectableObject.selectionCircle = null;
                    }
                }
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            if (selectedUnits.Count != 0)
            {
                foreach (SelectableUnitComponent selectedUnit in selectedUnits)
                {
                    //Debug.Log(selectedUnit.name);
                }
            }

            RaycastHit hitInfo = Utils.GetPositionFromMouseClick();

            GameObject o;
            if (hitInfo.collider != null)
            {
                o = hitInfo.collider.gameObject;
                Debug.Log(o.name);
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