using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JarScript : MonoBehaviour {

    private Transform jar_pos;

    // Use this for initialization
    void Start () {
        jar_pos = gameObject.GetComponent<Transform>();
    }
        
    
	
	// Update is called once per frame
	void Update () {

        //print(jar_pos.position.y + " | " + jar_pos.position.x);

        if(jar_pos.position.y > -3.9f && jar_pos.position.x == -1.04f)
            jar_pos.position = new Vector2(jar_pos.position.x, jar_pos.position.y - .01f);

        if (jar_pos.position.y <= -3.9f && jar_pos.position.x < 2f)
            jar_pos.position = new Vector2(jar_pos.position.x + .01f, jar_pos.position.y);

        if (jar_pos.position.y < -2.8f && jar_pos.position.x >= 2f)
            jar_pos.position = new Vector2(jar_pos.position.x, jar_pos.position.y + .01f);

        if (jar_pos.position.y >= -2.8f && jar_pos.position.x >= -2)
            jar_pos.position = new Vector2(-1.04f, -2.8f);

        /*
         *  inicial: x -1.04 y -3

            esquina izq abajo: x -1.04 y -3.9

            esquina der abajo: x 2 y -3.9

            final: x 2 y -3
         */
    }
}
