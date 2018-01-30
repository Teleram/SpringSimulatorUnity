using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SelectScript : MonoBehaviour {

    public GameObject me;
    public bool selected = false;


    bool isSelecting = false;
    Vector3 mousePosition1;
    private bool toBeSelected = false;
    
    // for debugging
    private bool selectionChanged = false;

	
	// Update is called once per frame
	void Update () 
	{

		// If we press the left mouse button, 
        // save mouse location and begin selection and
        // delete old selection
        if( Input.GetMouseButtonDown( 0 ) )
        {
            isSelecting = true;
            mousePosition1 = Input.mousePosition;
            selected = false;
            selectionChanged = true;
        }
        // If we let go of the left mouse button, end selection
        if( Input.GetMouseButtonUp( 0 ) )
            isSelecting = false;

        
        // If selecting is over and apply selection
        if(toBeSelected && !isSelecting)
        {
        	selected = true;
            selectionChanged = true;
        	toBeSelected = false;
        }


        // While we are selecting, mark if we are within the selection
        if(IsWithinSelectionBounds(me) && isSelecting)
        {
        	toBeSelected = true;
        }
        else
        {
        	toBeSelected = false;
        }


        // just to see, if we are selected or not
        if(selectionChanged && selected)
        {
            selectionChanged = false;
        	Debug.Log("I am selected!");
        }
        if(selectionChanged && !selected)
        {
            selectionChanged = false;
            Debug.Log("I am not selected!");
        }


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

    public bool IsWithinSelectionBounds( GameObject gameObject )
    {
        if( !isSelecting )
            return false;
 
        var camera = Camera.main;
        var viewportBounds =
            Utils.GetViewportBounds( camera, mousePosition1, Input.mousePosition );
            
        return viewportBounds.Contains(
            camera.WorldToViewportPoint( gameObject.transform.position ) );
    }
}
