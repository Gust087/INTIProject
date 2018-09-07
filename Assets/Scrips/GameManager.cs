using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public GameObject box;
    public GameObject jam;

    List<GameObject> empty_box = new List<GameObject>();
    List<GameObject> jam_box = new List<GameObject>();

    float[] stockX1 = new float[] { -3.4f, -2.7f, -0.3f, 0.4f };
    float[] stockX2 = new float[] { };
    float[] stockY = new float[] { 0.9f, 0.1f, -0.7f };

    // Use this for initialization
    void Start () {
        for (int i = 0; i < stockY.Length; i++)
        {
            for (int a = 0; a < stockX1.Length; a++)
            {
                GameObject auxgo = Instantiate(box, new Vector2(a,i), new Quaternion());
                empty_box.Add(auxgo.GetComponent<GameObject>());
            }
            for (int e = 0; e < stockX2.Length; e++)
            {
                GameObject auxgo = Instantiate(jam, new Vector2(e, i), new Quaternion());
                empty_box.Add(auxgo.GetComponent<GameObject>());
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

}
