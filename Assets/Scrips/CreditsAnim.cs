using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsAnim : MonoBehaviour {
    
    private Vector3 destination;
    private RectTransform rt;

    // Use this for initialization
    void Start ()
    {
        rt = GetComponent<RectTransform>();
        destination = new Vector3(0, 320, 0);
    }
	
	// Update is called once per frame
	void Update () {
        if (rt.position.y < destination.y)
        {
            rt.Translate(new Vector3(0, 1, 0), new Space());
        }
        else
        {
            UIScript.actualUI.MainMenu();
        }
    }
}
