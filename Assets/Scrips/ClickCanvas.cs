using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClickCanvas : MonoBehaviour {

    GraphicRaycaster raycaster;

    void Awake()
    {
        // Get both of the components we need to do this
        this.raycaster = GetComponent<GraphicRaycaster>();
    }

    void Update()
    {
        //Check if the left Mouse button is clicked
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            //Set up the new Pointer Event
            PointerEventData pointerData = new PointerEventData(EventSystem.current);
            List<RaycastResult> results = new List<RaycastResult>();

            //Raycast using the Graphics Raycaster and mouse click position
            pointerData.position = Input.mousePosition;
            this.raycaster.Raycast(pointerData, results);

            if (results.Count == 0)
            {
                UIScript.actualUI.DisablePopUps();
            }
        }
    }
}
