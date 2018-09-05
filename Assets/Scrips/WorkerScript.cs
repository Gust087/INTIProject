using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerScript : MonoBehaviour {

    public Animator animator;
    public int cantMovement;

    int count = 0;
    int numberDir = 0;

    const string
        wRight = "right",
        wLeft = "left",
        wUp = "up",
        wDown = "down"
        ;
    //   int[] directions = new int[] {'E','O','S','N' };

    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {

        CheckAnims(Move(numberDir));

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

    void CheckAnims(Vector2 v)
    {
        if (animator == null)
        {
            return;
        }
        if (v[0] < 0)
        {
            animator.SetBool(wRight, false);
            animator.SetBool(wUp, false);
            animator.SetBool(wDown, false);
            animator.SetBool(wLeft, true);

        }
        else if(v[0] > 0)
        {
            animator.SetBool(wLeft, false);
            animator.SetBool(wUp, false);
            animator.SetBool(wDown, false);
            animator.SetBool(wRight, true);

        }
        else if (v[1] < 0)
        {
            animator.SetBool(wRight, false);
            animator.SetBool(wLeft, false);
            animator.SetBool(wUp, false);
            animator.SetBool(wDown, true);

        }
        else if (v[1] > 0)
        {
            animator.SetBool(wRight, false);
            animator.SetBool(wLeft, false);
            animator.SetBool(wDown, false);
            animator.SetBool(wUp, true);
        }
        else
        {
            animator.SetBool(wRight, false);
            animator.SetBool(wLeft, false);
            animator.SetBool(wUp, false);
            animator.SetBool(wDown, false);

        }

    }

}
