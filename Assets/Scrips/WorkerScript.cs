using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerScript : MonoBehaviour {

    public int cantMovement;
    int count = 0;
    int numberDir = 0;
 //   int[] directions = new int[] {'E','O','S','N' };

    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        if (count > cantMovement) {
            numberDir++;
            count = 0;
        }
        if (numberDir == 4)
        {
            numberDir = 0;
        }
        transform.Translate(Move(numberDir));
        count++;
    }

    Vector2 Move(int a)
    {
        switch (a)
        {
            case 0:
                return new Vector2(0f,0.025f);
            case 1:
                return new Vector2(0f,-0.025f);
            case 2:
                return new Vector2(0.025f,0f);
            case 3:
                return new Vector2(-0.025f,0f);
            default:
                return new Vector2(0f,0f);
        }
    }
}
