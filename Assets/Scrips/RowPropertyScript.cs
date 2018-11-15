using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RowPropertyScript : MonoBehaviour {

    public Vector3 destination;
    private RectTransform rt;
    int mov = 0;

    // Use this for initialization
    void Start ()
    {
        rt = GetComponent<RectTransform>();
        destination = rt.position;
    }
	
	// Update is called once per frame
	void Update () {

        mov = rt.position.y - destination.y > 0 ? -1 : 1;

        if (rt.position.y != destination.y)
        {
            rt.anchoredPosition = new Vector3(0, rt.position.y + mov);
        }
    }
}
